using FleetErp.Application.Accounting.Interfaces;
using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Lookups.Interfaces;
using FleetErp.Application.Rentals.Dtos;
using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Domain.Entities.Rentals;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IRentalPaymentRepository _paymentRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ILookupRepository _lookupRepository;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionEngine _transactionEngine;

    public RentalService(
        IRentalRepository rentalRepository,
        IRentalPaymentRepository paymentRepository,
        IVehicleRepository vehicleRepository,
        ILookupRepository lookupRepository,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork,
        ITransactionEngine transactionEngine)
    {
        _rentalRepository = rentalRepository;
        _paymentRepository = paymentRepository;
        _vehicleRepository = vehicleRepository;
        _lookupRepository = lookupRepository;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
        _transactionEngine = transactionEngine;
    }

    public async Task<Result<RentalDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var rental = await _rentalRepository.GetByIdWithPaymentsAsync(id, ct);
        if (rental is null)
        {
            return Result<RentalDto>.NotFound("Rental not found");
        }

        return Result<RentalDto>.Success(MapToDto(rental));
    }

    public async Task<Result<PagedResult<RentalListDto>>> GetPagedAsync(int page, int pageSize, string? search, int? customerId, int? vehicleId, int? statusId, CancellationToken ct = default)
    {
        var result = await _rentalRepository.GetPagedAsync(page, pageSize, search, customerId, vehicleId, statusId, ct);

        var dtos = result.Items.Select(MapToListDto).ToList();
        return Result<PagedResult<RentalListDto>>.Success(
            new PagedResult<RentalListDto> { Items = dtos, Page = result.Page, PageSize = result.PageSize, TotalCount = result.TotalCount });
    }

    public async Task<Result<IReadOnlyList<RentalListDto>>> GetActiveRentalsAsync(CancellationToken ct = default)
    {
        var rentals = await _rentalRepository.GetActiveAsync(ct);
        var dtos = rentals.Select(MapToListDto).ToList();
        return Result<IReadOnlyList<RentalListDto>>.Success(dtos);
    }

    public async Task<Result<RentalDto>> CreateAsync(CreateRentalRequest request, CancellationToken ct = default)
    {
        // Check if vehicle has active rental
        if (await _rentalRepository.VehicleHasActiveRentalAsync(request.VehicleId, null, ct))
        {
            return Result<RentalDto>.Conflict("Vehicle already has an active rental");
        }

        // Generate rental number
        var sequence = await _rentalRepository.GetNextRentalSequenceAsync(DateTime.UtcNow, ct);
        var rentalNumber = $"RNT-{DateTime.UtcNow:yyyyMMdd}-{sequence:D4}";

        // Calculate total
        var totalDays = (request.EndDate.Date - request.StartDate.Date).Days + 1;
        var subTotal = request.DailyRate * totalDays;
        var totalAmount = subTotal - request.Discount;

        // Get initial payment status (Unpaid)
        var unpaidStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.PaymentStatus, "UNPAID", ct);
        if (unpaidStatus is null)
        {
            return Result<RentalDto>.Invalid("Payment status 'UNPAID' not found");
        }

        var rental = new Rental
        {
            RentalNumber = rentalNumber,
            VehicleId = request.VehicleId,
            CustomerId = request.CustomerId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            OdometerStart = request.OdometerStart,
            DailyRate = request.DailyRate,
            Discount = request.Discount,
            TotalAmount = totalAmount,
            PaidAmount = 0,
            StatusId = request.StatusId,
            PaymentStatusId = unpaidStatus.Id,
            Notes = request.Notes
        };

        await _rentalRepository.AddAsync(rental, ct);

        // Update vehicle status to RENTED
        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, ct);
        if (vehicle != null)
        {
            var rentedStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.VehicleStatus, "RENTED", ct);
            if (rentedStatus != null)
            {
                vehicle.StatusId = rentedStatus.Id;
                _vehicleRepository.Update(vehicle);
            }
        }

        _auditLogger.Log(AuditActions.RentalCreated, nameof(Rental), rental.Id,
            userId: null, $"Rental '{rentalNumber}' created for vehicle ID {request.VehicleId}");

        await _unitOfWork.SaveChangesAsync(ct);

        // Post accounting transaction for rental creation (customer now owes us)
        await _transactionEngine.PostRentalCreatedAsync(rental.Id, request.CustomerId, totalAmount, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _rentalRepository.GetByIdAsync(rental.Id, ct);
        return Result<RentalDto>.Success(MapToDto(created!));
    }

    public async Task<Result<RentalDto>> UpdateAsync(int id, UpdateRentalRequest request, CancellationToken ct = default)
    {
        var rental = await _rentalRepository.GetByIdAsync(id, ct);
        if (rental is null)
        {
            return Result<RentalDto>.NotFound("Rental not found");
        }

        // Check if rental is completed or cancelled
        if (rental.Status.Code == "COMPLETED" || rental.Status.Code == "CANCELLED")
        {
            return Result<RentalDto>.Invalid("Cannot update a completed or cancelled rental");
        }

        // If vehicle changed, check new vehicle availability
        if (rental.VehicleId != request.VehicleId)
        {
            if (await _rentalRepository.VehicleHasActiveRentalAsync(request.VehicleId, id, ct))
            {
                return Result<RentalDto>.Conflict("New vehicle already has an active rental");
            }

            // Set old vehicle to available
            var oldVehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId, ct);
            if (oldVehicle != null)
            {
                var availableStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.VehicleStatus, "AVAILABLE", ct);
                if (availableStatus != null)
                {
                    oldVehicle.StatusId = availableStatus.Id;
                    _vehicleRepository.Update(oldVehicle);
                }
            }

            // Set new vehicle to rented
            var newVehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, ct);
            if (newVehicle != null)
            {
                var rentedStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.VehicleStatus, "RENTED", ct);
                if (rentedStatus != null)
                {
                    newVehicle.StatusId = rentedStatus.Id;
                    _vehicleRepository.Update(newVehicle);
                }
            }
        }

        // Recalculate amounts
        var totalDays = (request.EndDate.Date - request.StartDate.Date).Days + 1;
        var subTotal = request.DailyRate * totalDays;
        var totalAmount = subTotal - request.Discount;

        rental.VehicleId = request.VehicleId;
        rental.CustomerId = request.CustomerId;
        rental.StartDate = request.StartDate;
        rental.EndDate = request.EndDate;
        rental.OdometerStart = request.OdometerStart;
        rental.DailyRate = request.DailyRate;
        rental.Discount = request.Discount;
        rental.TotalAmount = totalAmount;
        rental.StatusId = request.StatusId;
        rental.Notes = request.Notes;

        _rentalRepository.Update(rental);
        _auditLogger.Log(AuditActions.RentalUpdated, nameof(Rental), rental.Id,
            userId: null, $"Rental '{rental.RentalNumber}' updated");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _rentalRepository.GetByIdAsync(rental.Id, ct);
        return Result<RentalDto>.Success(MapToDto(updated!));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var rental = await _rentalRepository.GetByIdAsync(id, ct);
        if (rental is null)
        {
            return Result.NotFound("Rental not found");
        }

        // Set vehicle back to available if rental was active
        if (rental.Status.Code == "ACTIVE" || rental.Status.Code == "RESERVED")
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId, ct);
            if (vehicle != null)
            {
                var availableStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.VehicleStatus, "AVAILABLE", ct);
                if (availableStatus != null)
                {
                    vehicle.StatusId = availableStatus.Id;
                    _vehicleRepository.Update(vehicle);
                }
            }
        }

        _rentalRepository.Delete(rental);
        _auditLogger.Log(AuditActions.RentalDeleted, nameof(Rental), rental.Id,
            userId: null, $"Rental '{rental.RentalNumber}' deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<RentalDto>> CompleteAsync(int id, CompleteRentalRequest request, CancellationToken ct = default)
    {
        var rental = await _rentalRepository.GetByIdAsync(id, ct);
        if (rental is null)
        {
            return Result<RentalDto>.NotFound("Rental not found");
        }

        if (rental.Status.Code != "ACTIVE")
        {
            return Result<RentalDto>.Invalid("Only active rentals can be completed");
        }

        // Get completed status
        var completedStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.RentalStatus, "COMPLETED", ct);
        if (completedStatus is null)
        {
            return Result<RentalDto>.Invalid("Rental status 'COMPLETED' not found");
        }

        rental.ActualReturnDate = request.ActualReturnDate;
        rental.OdometerEnd = request.OdometerEnd;
        rental.StatusId = completedStatus.Id;

        // Update vehicle status to available and odometer
        var vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId, ct);
        if (vehicle != null)
        {
            var availableStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.VehicleStatus, "AVAILABLE", ct);
            if (availableStatus != null)
            {
                vehicle.StatusId = availableStatus.Id;
                vehicle.Odometer = request.OdometerEnd;
                _vehicleRepository.Update(vehicle);
            }
        }

        _rentalRepository.Update(rental);
        _auditLogger.Log(AuditActions.RentalCompleted, nameof(Rental), rental.Id,
            userId: null, $"Rental '{rental.RentalNumber}' completed");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _rentalRepository.GetByIdAsync(rental.Id, ct);
        return Result<RentalDto>.Success(MapToDto(updated!));
    }

    public async Task<Result<RentalDto>> CancelAsync(int id, CancellationToken ct = default)
    {
        var rental = await _rentalRepository.GetByIdAsync(id, ct);
        if (rental is null)
        {
            return Result<RentalDto>.NotFound("Rental not found");
        }

        if (rental.Status.Code == "COMPLETED" || rental.Status.Code == "CANCELLED")
        {
            return Result<RentalDto>.Invalid("Rental is already completed or cancelled");
        }

        // Get cancelled status
        var cancelledStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.RentalStatus, "CANCELLED", ct);
        if (cancelledStatus is null)
        {
            return Result<RentalDto>.Invalid("Rental status 'CANCELLED' not found");
        }

        rental.StatusId = cancelledStatus.Id;

        // Update vehicle status to available
        var vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId, ct);
        if (vehicle != null)
        {
            var availableStatus = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.VehicleStatus, "AVAILABLE", ct);
            if (availableStatus != null)
            {
                vehicle.StatusId = availableStatus.Id;
                _vehicleRepository.Update(vehicle);
            }
        }

        _rentalRepository.Update(rental);
        _auditLogger.Log(AuditActions.RentalCancelled, nameof(Rental), rental.Id,
            userId: null, $"Rental '{rental.RentalNumber}' cancelled");

        await _unitOfWork.SaveChangesAsync(ct);

        var updated = await _rentalRepository.GetByIdAsync(rental.Id, ct);
        return Result<RentalDto>.Success(MapToDto(updated!));
    }

    public async Task<Result<IReadOnlyList<RentalPaymentDto>>> GetPaymentsAsync(int rentalId, CancellationToken ct = default)
    {
        var rental = await _rentalRepository.GetByIdAsync(rentalId, ct);
        if (rental is null)
        {
            return Result<IReadOnlyList<RentalPaymentDto>>.NotFound("Rental not found");
        }

        var payments = await _paymentRepository.GetByRentalIdAsync(rentalId, ct);
        var dtos = payments.Select(MapPaymentToDto).ToList();

        return Result<IReadOnlyList<RentalPaymentDto>>.Success(dtos);
    }

    public async Task<Result<RentalPaymentDto>> AddPaymentAsync(int rentalId, CreateRentalPaymentRequest request, CancellationToken ct = default)
    {
        var rental = await _rentalRepository.GetByIdAsync(rentalId, ct);
        if (rental is null)
        {
            return Result<RentalPaymentDto>.NotFound("Rental not found");
        }

        if (rental.Status.Code == "CANCELLED")
        {
            return Result<RentalPaymentDto>.Invalid("Cannot add payment to a cancelled rental");
        }

        var payment = new RentalPayment
        {
            RentalId = rentalId,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            PaymentMethodId = request.PaymentMethodId,
            TransactionReference = request.TransactionReference,
            Notes = request.Notes
        };

        await _paymentRepository.AddAsync(payment, ct);

        // Update rental paid amount
        rental.PaidAmount += request.Amount;

        // Update payment status
        await UpdatePaymentStatusAsync(rental, ct);

        _rentalRepository.Update(rental);
        _auditLogger.Log(AuditActions.RentalPaymentAdded, nameof(RentalPayment), payment.Id,
            userId: null, $"Payment of {request.Amount:C} added to rental '{rental.RentalNumber}'");

        await _unitOfWork.SaveChangesAsync(ct);

        // Post accounting transaction for payment received
        await _transactionEngine.PostRentalPaymentReceivedAsync(payment.Id, rental.CustomerId, request.Amount, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _paymentRepository.GetByIdAsync(payment.Id, ct);
        return Result<RentalPaymentDto>.Success(MapPaymentToDto(created!));
    }

    public async Task<Result> DeletePaymentAsync(int rentalId, int paymentId, CancellationToken ct = default)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId, ct);
        if (payment is null || payment.RentalId != rentalId)
        {
            return Result.NotFound("Payment not found");
        }

        var rental = await _rentalRepository.GetByIdAsync(rentalId, ct);
        if (rental is null)
        {
            return Result.NotFound("Rental not found");
        }

        var paymentAmount = payment.Amount;

        // Update rental paid amount
        rental.PaidAmount -= payment.Amount;
        if (rental.PaidAmount < 0) rental.PaidAmount = 0;

        // Update payment status
        await UpdatePaymentStatusAsync(rental, ct);

        _paymentRepository.Delete(payment);
        _rentalRepository.Update(rental);
        _auditLogger.Log(AuditActions.RentalPaymentDeleted, nameof(RentalPayment), paymentId,
            userId: null, $"Payment deleted from rental '{rental.RentalNumber}'");

        await _unitOfWork.SaveChangesAsync(ct);

        // Post accounting transaction for payment voided (reverse the original)
        await _transactionEngine.PostRentalPaymentVoidedAsync(paymentId, rental.CustomerId, paymentAmount, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    private async Task UpdatePaymentStatusAsync(Rental rental, CancellationToken ct)
    {
        string statusCode;
        if (rental.PaidAmount >= rental.TotalAmount)
        {
            statusCode = "PAID";
        }
        else if (rental.PaidAmount > 0)
        {
            statusCode = "PARTIAL";
        }
        else
        {
            statusCode = "UNPAID";
        }

        var status = await _lookupRepository.GetByTypeAndCodeAsync(LookupTypes.PaymentStatus, statusCode, ct);
        if (status != null)
        {
            rental.PaymentStatusId = status.Id;
        }
    }

    private static RentalDto MapToDto(Rental rental)
    {
        return new RentalDto(
            rental.Id,
            rental.RentalNumber,
            rental.VehicleId,
            rental.Vehicle.PlateNumber,
            $"{rental.Vehicle.Make} {rental.Vehicle.Model} ({rental.Vehicle.Year})",
            rental.CustomerId,
            rental.Customer.FullName,
            rental.StartDate,
            rental.EndDate,
            rental.ActualReturnDate,
            rental.OdometerStart,
            rental.OdometerEnd,
            rental.DailyRate,
            rental.TotalDays,
            rental.SubTotal,
            rental.Discount,
            rental.TotalAmount,
            rental.PaidAmount,
            rental.RemainingAmount,
            rental.StatusId,
            rental.Status.Name,
            rental.PaymentStatusId,
            rental.PaymentStatus.Name,
            rental.Notes,
            rental.CreatedAt,
            rental.UpdatedAt,
            rental.Payments.Count
        );
    }

    private static RentalListDto MapToListDto(Rental rental)
    {
        return new RentalListDto(
            rental.Id,
            rental.RentalNumber,
            rental.Vehicle.PlateNumber,
            $"{rental.Vehicle.Make} {rental.Vehicle.Model}",
            rental.Customer.FullName,
            rental.StartDate,
            rental.EndDate,
            rental.TotalAmount,
            rental.PaidAmount,
            rental.Status.Name,
            rental.PaymentStatus.Name
        );
    }

    private static RentalPaymentDto MapPaymentToDto(RentalPayment payment)
    {
        return new RentalPaymentDto(
            payment.Id,
            payment.RentalId,
            payment.Amount,
            payment.PaymentDate,
            payment.PaymentMethodId,
            payment.PaymentMethod.Name,
            payment.TransactionReference,
            payment.Notes,
            payment.CreatedAt
        );
    }
}
