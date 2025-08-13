using AutoMapper;
using System.Reflection;

namespace ITHSystems.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(x => x.GetTypes())
        .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes<Attributes.AutoMapAttribute>().Any());

        foreach (var type in types)
        {
            var attributes = type.GetCustomAttributes<Attributes.AutoMapAttribute>();
            foreach (var attr in attributes)
            {
                var map = CreateMap(type, attr.TargetType);
                if (attr.ReverseMap)
                    map.ReverseMap();
            }
        }
    }
}
