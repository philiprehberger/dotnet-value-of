namespace Philiprehberger.ValueOf;

/// <summary>
/// Marker attribute for value object types.
/// Reserved for future source generator support.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ValueOfAttribute : Attribute
{
}
