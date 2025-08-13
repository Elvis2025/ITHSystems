namespace ITHSystems.Attributes;

public class RegisterAsRouteAttribute : Attribute
{
    public string? RouteName { get; }

    public RegisterAsRouteAttribute(string? routeName = null)
    {
        RouteName = routeName;
    }
}
