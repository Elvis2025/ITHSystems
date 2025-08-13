namespace ITHSystems.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AutoMapAttribute : Attribute
{
    public Type TargetType { get; }
    public bool ReverseMap { get; set; } = true;

    public AutoMapAttribute(Type targetType)
    {
        TargetType = targetType;
    }
}
