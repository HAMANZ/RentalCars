namespace FleetErp.Domain.Exceptions;

/// <summary>
/// Exception thrown when a domain invariant is violated.
/// Use for situations like invalid state transitions that shouldn't happen
/// if the calling code is correct.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception innerException)
        : base(message, innerException) { }
}
