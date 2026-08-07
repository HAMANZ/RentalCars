namespace FleetErp.Application.Common;

/// <summary>
/// Result pattern for expected failures (validation, not found, business rule conflicts).
/// Exceptions should only be used for truly exceptional/programmer errors.
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public ResultErrorType ErrorType { get; }

    protected Result(bool isSuccess, string? error, ResultErrorType errorType)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorType = errorType;
    }

    public static Result Success() => new(true, null, ResultErrorType.None);
    public static Result NotFound(string error) => new(false, error, ResultErrorType.NotFound);
    public static Result Invalid(string error) => new(false, error, ResultErrorType.Validation);
    public static Result Conflict(string error) => new(false, error, ResultErrorType.Conflict);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(true, null, ResultErrorType.None)
    {
        Value = value;
    }

    private Result(string error, ResultErrorType errorType) : base(false, error, errorType)
    {
        Value = default;
    }

    public static Result<T> Success(T value) => new(value);
    public static new Result<T> NotFound(string error) => new(error, ResultErrorType.NotFound);
    public static new Result<T> Invalid(string error) => new(error, ResultErrorType.Validation);
    public static new Result<T> Conflict(string error) => new(error, ResultErrorType.Conflict);
}

public enum ResultErrorType
{
    None,
    NotFound,
    Validation,
    Conflict
}
