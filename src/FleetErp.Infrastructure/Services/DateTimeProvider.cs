using FleetErp.Application.Common.Interfaces;

namespace FleetErp.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
