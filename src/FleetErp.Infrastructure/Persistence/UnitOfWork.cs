using FleetErp.Application.Common.Interfaces;

namespace FleetErp.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly FleetErpDbContext _context;

    public UnitOfWork(FleetErpDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
