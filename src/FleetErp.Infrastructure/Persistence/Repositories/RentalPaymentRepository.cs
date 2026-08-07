using FleetErp.Application.Rentals.Interfaces;
using FleetErp.Domain.Entities.Rentals;
using Microsoft.EntityFrameworkCore;

namespace FleetErp.Infrastructure.Persistence.Repositories;

public class RentalPaymentRepository : IRentalPaymentRepository
{
    private readonly FleetErpDbContext _context;

    public RentalPaymentRepository(FleetErpDbContext context)
    {
        _context = context;
    }

    public async Task<RentalPayment?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.RentalPayments
            .Include(p => p.PaymentMethod)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<IReadOnlyList<RentalPayment>> GetByRentalIdAsync(int rentalId, CancellationToken ct = default)
    {
        return await _context.RentalPayments
            .Include(p => p.PaymentMethod)
            .Where(p => p.RentalId == rentalId)
            .OrderByDescending(p => p.PaymentDate)
            .ToListAsync(ct);
    }

    public async Task AddAsync(RentalPayment payment, CancellationToken ct = default)
    {
        await _context.RentalPayments.AddAsync(payment, ct);
    }

    public void Delete(RentalPayment payment)
    {
        payment.IsDeleted = true;
        _context.RentalPayments.Update(payment);
    }
}
