using FleetErp.Application.Audit.Interfaces;
using FleetErp.Application.Common;
using FleetErp.Application.Common.Interfaces;
using FleetErp.Application.Vehicles.Dtos;
using FleetErp.Application.Vehicles.Interfaces;
using FleetErp.Domain.Entities.Vehicles;
using FleetErp.Shared.Constants;

namespace FleetErp.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IVehicleDocumentRepository _documentRepository;
    private readonly IAuditLogger _auditLogger;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleService(
        IVehicleRepository vehicleRepository,
        IVehicleDocumentRepository documentRepository,
        IAuditLogger auditLogger,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _documentRepository = documentRepository;
        _auditLogger = auditLogger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<VehicleDto>> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var vehicle = await _vehicleRepository.GetByIdWithDocumentsAsync(id, ct);
        if (vehicle == null)
        {
            return Result<VehicleDto>.NotFound("Vehicle not found");
        }

        return Result<VehicleDto>.Success(MapToDto(vehicle));
    }

    public async Task<Result<PagedResult<VehicleListDto>>> GetPagedAsync(
        int page,
        int pageSize,
        string? search = null,
        int? investorId = null,
        int? statusId = null,
        int? vehicleTypeId = null,
        CancellationToken ct = default)
    {
        var pagedResult = await _vehicleRepository.GetPagedAsync(page, pageSize, search, investorId, statusId, ct);

        var dtos = pagedResult.Items.Select(MapToListDto).ToList();

        return Result<PagedResult<VehicleListDto>>.Success(new PagedResult<VehicleListDto>
        {
            Items = dtos,
            Page = pagedResult.Page,
            PageSize = pagedResult.PageSize,
            TotalCount = pagedResult.TotalCount
        });
    }

    public async Task<Result<IReadOnlyList<VehicleListDto>>> GetAvailableAsync(CancellationToken ct = default)
    {
        var vehicles = await _vehicleRepository.GetAvailableAsync(ct);
        var dtos = vehicles.Select(MapToListDto).ToList();
        return Result<IReadOnlyList<VehicleListDto>>.Success(dtos);
    }

    public async Task<Result<VehicleDto>> CreateAsync(CreateVehicleRequest request, CancellationToken ct = default)
    {
        // Check for duplicate plate number
        if (await _vehicleRepository.PlateNumberExistsAsync(request.PlateNumber, null, ct))
        {
            return Result<VehicleDto>.Conflict("A vehicle with this plate number already exists");
        }

        // Check for duplicate VIN if provided
        if (!string.IsNullOrEmpty(request.Vin))
        {
            if (await _vehicleRepository.VinExistsAsync(request.Vin, null, ct))
            {
                return Result<VehicleDto>.Conflict("A vehicle with this VIN already exists");
            }
        }

        var vehicle = new Vehicle
        {
            PlateNumber = request.PlateNumber.ToUpperInvariant(),
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            Color = request.Color,
            Vin = request.Vin?.ToUpperInvariant(),
            EngineNumber = request.EngineNumber,
            ChassisNumber = request.ChassisNumber,
            Odometer = request.Odometer,
            DailyRate = request.DailyRate,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            Notes = request.Notes,
            InvestorId = request.InvestorId,
            StatusId = request.StatusId,
            VehicleTypeId = request.VehicleTypeId,
            FuelTypeId = request.FuelTypeId,
            TransmissionTypeId = request.TransmissionTypeId
        };

        await _vehicleRepository.AddAsync(vehicle, ct);

        _auditLogger.Log(AuditActions.VehicleCreated, nameof(Vehicle), vehicle.Id, vehicle.Id.ToString(), $"Vehicle {vehicle.PlateNumber} created");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with all navigation properties
        var createdVehicle = await _vehicleRepository.GetByIdWithDocumentsAsync(vehicle.Id, ct);
        return Result<VehicleDto>.Success(MapToDto(createdVehicle!));
    }

    public async Task<Result<VehicleDto>> UpdateAsync(int id, UpdateVehicleRequest request, CancellationToken ct = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id, ct);
        if (vehicle == null)
        {
            return Result<VehicleDto>.NotFound("Vehicle not found");
        }

        // Check for duplicate plate number if being changed
        if (!string.Equals(vehicle.PlateNumber, request.PlateNumber, StringComparison.OrdinalIgnoreCase))
        {
            if (await _vehicleRepository.PlateNumberExistsAsync(request.PlateNumber, id, ct))
            {
                return Result<VehicleDto>.Conflict("A vehicle with this plate number already exists");
            }
        }

        // Check for duplicate VIN if being changed
        if (!string.IsNullOrEmpty(request.Vin))
        {
            if (!string.Equals(vehicle.Vin, request.Vin, StringComparison.OrdinalIgnoreCase))
            {
                if (await _vehicleRepository.VinExistsAsync(request.Vin, id, ct))
                {
                    return Result<VehicleDto>.Conflict("A vehicle with this VIN already exists");
                }
            }
        }

        vehicle.PlateNumber = request.PlateNumber.ToUpperInvariant();
        vehicle.Make = request.Make;
        vehicle.Model = request.Model;
        vehicle.Year = request.Year;
        vehicle.Color = request.Color;
        vehicle.Vin = request.Vin?.ToUpperInvariant();
        vehicle.EngineNumber = request.EngineNumber;
        vehicle.ChassisNumber = request.ChassisNumber;
        vehicle.Odometer = request.Odometer;
        vehicle.DailyRate = request.DailyRate;
        vehicle.PurchaseDate = request.PurchaseDate;
        vehicle.PurchasePrice = request.PurchasePrice;
        vehicle.Notes = request.Notes;
        vehicle.InvestorId = request.InvestorId;
        vehicle.StatusId = request.StatusId;
        vehicle.VehicleTypeId = request.VehicleTypeId;
        vehicle.FuelTypeId = request.FuelTypeId;
        vehicle.TransmissionTypeId = request.TransmissionTypeId;

        _vehicleRepository.Update(vehicle);

        _auditLogger.Log(AuditActions.VehicleUpdated, nameof(Vehicle), vehicle.Id, vehicle.Id.ToString(), $"Vehicle {vehicle.PlateNumber} updated");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with all navigation properties
        var updatedVehicle = await _vehicleRepository.GetByIdWithDocumentsAsync(vehicle.Id, ct);
        return Result<VehicleDto>.Success(MapToDto(updatedVehicle!));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id, ct);
        if (vehicle == null)
        {
            return Result.NotFound("Vehicle not found");
        }

        _vehicleRepository.Delete(vehicle);

        _auditLogger.Log(AuditActions.VehicleDeleted, nameof(Vehicle), vehicle.Id, vehicle.Id.ToString(), $"Vehicle {vehicle.PlateNumber} deleted");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<IReadOnlyList<VehicleDocumentDto>>> GetDocumentsAsync(int vehicleId, CancellationToken ct = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, ct);
        if (vehicle == null)
        {
            return Result<IReadOnlyList<VehicleDocumentDto>>.NotFound("Vehicle not found");
        }

        var documents = await _documentRepository.GetByVehicleIdAsync(vehicleId, ct);
        var dtos = documents.Select(MapDocumentToDto).ToList();

        return Result<IReadOnlyList<VehicleDocumentDto>>.Success(dtos);
    }

    public async Task<Result<VehicleDocumentDto>> AddDocumentAsync(int vehicleId, CreateVehicleDocumentRequest request, CancellationToken ct = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, ct);
        if (vehicle == null)
        {
            return Result<VehicleDocumentDto>.NotFound("Vehicle not found");
        }

        var document = new VehicleDocument
        {
            VehicleId = vehicleId,
            DocumentTypeId = request.DocumentTypeId,
            FilePath = request.FilePath,
            ExpiresAt = request.ExpiresAt
        };

        await _documentRepository.AddAsync(document, ct);

        _auditLogger.Log(AuditActions.VehicleDocumentAdded, nameof(VehicleDocument), document.Id, vehicleId.ToString(), $"Document added to vehicle {vehicle.PlateNumber}");

        await _unitOfWork.SaveChangesAsync(ct);

        // Reload with document type
        var createdDocument = await _documentRepository.GetByIdAsync(document.Id, ct);
        return Result<VehicleDocumentDto>.Success(MapDocumentToDto(createdDocument!));
    }

    public async Task<Result> DeleteDocumentAsync(int vehicleId, int documentId, CancellationToken ct = default)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, ct);
        if (vehicle == null)
        {
            return Result.NotFound("Vehicle not found");
        }

        var document = await _documentRepository.GetByIdAsync(documentId, ct);
        if (document == null || document.VehicleId != vehicleId)
        {
            return Result.NotFound("Document not found");
        }

        _documentRepository.Delete(document);

        _auditLogger.Log(AuditActions.VehicleDocumentDeleted, nameof(VehicleDocument), document.Id, vehicleId.ToString(), $"Document removed from vehicle {vehicle.PlateNumber}");

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }

    private static VehicleDto MapToDto(Vehicle vehicle)
    {
        return new VehicleDto(
            vehicle.Id,
            vehicle.PlateNumber,
            vehicle.Make,
            vehicle.Model,
            vehicle.Year,
            vehicle.Color,
            vehicle.Vin,
            vehicle.EngineNumber,
            vehicle.ChassisNumber,
            vehicle.Odometer,
            vehicle.DailyRate,
            vehicle.PurchaseDate,
            vehicle.PurchasePrice,
            vehicle.Notes,
            vehicle.InvestorId,
            vehicle.Investor.FullName,
            vehicle.StatusId,
            vehicle.Status.Name,
            vehicle.VehicleTypeId,
            vehicle.VehicleType.Name,
            vehicle.FuelTypeId,
            vehicle.FuelType.Name,
            vehicle.TransmissionTypeId,
            vehicle.TransmissionType.Name,
            vehicle.CreatedAt,
            vehicle.UpdatedAt,
            vehicle.Documents.Count
        );
    }

    private static VehicleListDto MapToListDto(Vehicle vehicle)
    {
        return new VehicleListDto(
            vehicle.Id,
            vehicle.PlateNumber,
            vehicle.Make,
            vehicle.Model,
            vehicle.Year,
            vehicle.Color,
            vehicle.DailyRate,
            vehicle.Investor.FullName,
            vehicle.Status.Name,
            vehicle.VehicleType.Name,
            vehicle.Documents.Count
        );
    }

    private static VehicleDocumentDto MapDocumentToDto(VehicleDocument document)
    {
        return new VehicleDocumentDto(
            document.Id,
            document.VehicleId,
            document.DocumentTypeId,
            document.DocumentType.Name,
            document.FilePath,
            document.ExpiresAt,
            document.IsExpired,
            document.CreatedAt
        );
    }
}
