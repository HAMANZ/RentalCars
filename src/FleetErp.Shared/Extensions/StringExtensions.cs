namespace FleetErp.Shared.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Convert a string to snake_case (for database column names).
    /// </summary>
    public static string ToSnakeCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return string.Concat(str.Select((c, i) =>
            i > 0 && char.IsUpper(c) ? "_" + c.ToString() : c.ToString()))
            .ToLowerInvariant();
    }

    /// <summary>
    /// Truncate a string to a maximum length.
    /// </summary>
    public static string Truncate(this string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return str.Length <= maxLength ? str : str[..maxLength];
    }

    /// <summary>
    /// Check if a string is null, empty, or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// Returns null if the string is empty or whitespace, otherwise returns the string.
    /// </summary>
    public static string? NullIfEmpty(this string? str)
    {
        return string.IsNullOrWhiteSpace(str) ? null : str;
    }
}
