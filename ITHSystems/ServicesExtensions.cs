using ITHSystems.Attributes;
using ITHSystems.AutoMapper;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Views;
using System.Diagnostics;
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
                .AddTransient(typeof(IRepository<>), typeof(SQLiteRepository<>))
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
        try
        {
            var types = assembly.GetTypes();

            var toRegister = types
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t =>
                  {
                      var attrTypes = t.GetCustomAttributes(inherit: true)
                                       .Select(a => a.GetType());

                      return attrTypes.Any(at =>
                          at.Namespace == "ITHSystems.Attributes" &&
                          at != typeof(SQLiteEntityAttribute));
                  })
          
                .ToList();

            foreach (var implType in toRegister)
            {
                var interfaceType = implType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{implType.Name}");

                if (interfaceType is null)
                    services.AddTransient(implType);
                else
                    services.AddTransient(interfaceType, implType);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error registering types from assembly {assembly.FullName}: {e.Message}");
            MainThread.BeginInvokeOnMainThread(async () =>
                await BaseViewModel.ErrorAlert("Error App", $"Error registering types from assembly {assembly.FullName}: {e.Message}"));
        }
    }
}
