using ITHSystems.AutoMapper;
using ITHSystems.Repositories;
using ITHSystems.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        var types = assembly.GetTypes();

        foreach (var implType in types.Where(t => t.IsClass && !t.IsAbstract))
        {
            var interfaceType = implType.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{implType.Name}");

            if (implType.Name.EndsWith("Repository"))
            {
                if (interfaceType is null)
                    services.AddTransient(implType);
                else
                    services.AddTransient(interfaceType, implType);

                continue;
            }

            if (implType.Name.EndsWith("Service"))
            {
                if (interfaceType is null)
                    services.AddTransient(implType);
                else
                    services.AddTransient(interfaceType, implType);

                continue;
            }

            if (implType.Name.EndsWith("ViewModel"))
            {
                services.AddTransient(implType);
                continue;
            }
        }
    }
}
