namespace ITHSystems.Attributes;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterAsRouteAttribute : Attribute
{
    public string? RouteName { get; }

    public RegisterAsRouteAttribute(string? routeName = null)
    {
        RouteName = routeName;
    }
}
