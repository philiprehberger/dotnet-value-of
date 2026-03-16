namespace Philiprehberger.ValueOf;

/// <summary>
/// Exception thrown when a value object fails validation.
/// </summary>
public class ValueOfValidationException : Exception
{
    /// <summary>
    /// Gets the type of the value object that failed validation.
    /// </summary>
    public Type ValueObjectType { get; }

    /// <summary>
    /// Creates a new validation exception with a message.
    /// </summary>
    /// <param name="valueObjectType">The type of the value object that failed validation.</param>
    /// <param name="message">A description of the validation failure.</param>
    public ValueOfValidationException(Type valueObjectType, string message)
        : base($"{valueObjectType.Name}: {message}")
    {
        ValueObjectType = valueObjectType;
    }

    /// <summary>
    /// Creates a new validation exception with a message and inner exception.
    /// </summary>
    /// <param name="valueObjectType">The type of the value object that failed validation.</param>
    /// <param name="message">A description of the validation failure.</param>
    /// <param name="innerException">The inner exception that caused this failure.</param>
    public ValueOfValidationException(Type valueObjectType, string message, Exception innerException)
        : base($"{valueObjectType.Name}: {message}", innerException)
    {
        ValueObjectType = valueObjectType;
    }
}
