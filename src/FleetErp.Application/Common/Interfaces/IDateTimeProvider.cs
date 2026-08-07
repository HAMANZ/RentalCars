namespace FleetErp.Application.Common.Interfaces;

/// <summary>
/// Abstraction for getting current time. Makes testing easier.
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
