
using AutoMapper;
using System.Reflection;

namespace CaseTecnico.MRA.Application.Mappings;

public  class MapToProfile<TSource, TDestination> : Profile
{
    public MapToProfile()
    {
        CreateMap<TSource, TDestination>()
            .AfterMap((src, dest) =>
            {
                var sourceType = typeof(TSource);
                var destinationType = typeof(TDestination);

                foreach (var sourceProp in sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var destProp = destinationType.GetProperty(sourceProp.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (destProp != null && destProp.CanWrite)
                    {
                        var value = sourceProp.GetValue(src);
                        destProp.SetValue(dest, value);
                    }
                }
            });
    }
}
