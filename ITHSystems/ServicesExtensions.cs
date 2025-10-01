using ITHSystems.Attributes;
using ITHSystems.AutoMapper;
using ITHSystems.Repositories.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ITHSystems;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<ISQLiteManager, SQLiteManager>()
                .AddAutoMapper(cfg =>
                {
                    cfg.AddProfile<MapperProfile>();
                })
                .RegisterAll();
        return services;
    }

    private static void RegisterAll(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            RegisterTypesInAssembly(services, assembly);
        }
    }

    private static void RegisterTypesInAssembly(IServiceCollection services, Assembly assembly)
    {
        Type[]? types = assembly.GetTypes();
        //attributesArray.Any(attr => t.IsDefined(attr, inherit: true)
        Func<Type, bool> allAttributes = t => (t.Namespace != null && t.Namespace == "Attributes") && 
                                              (t.IsDefined(t,inherit: true));

        foreach (var implType in types.Where(allAttributes) )
        {
            var interfaceType = implType.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{implType.Name}");

                if (interfaceType is null)
                    services.AddTransient(implType);
                else
                    services.AddTransient(interfaceType, implType);
           
        }
    }
}
