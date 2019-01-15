using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace AutoMapping.Pattern2.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                config.AddCustomMappingProfile();
            });

            //Compile mapping after configuration to boost map speed
            Mapper.Configuration.CompileMappings();
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
        {
            config.AddCustomMappingProfile(Assembly.GetEntryAssembly());
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

            //Find all classes that implement IHaveCustomMapping inteface, like Dto because BaseDto implement this interface 
            var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
                type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
                .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

            //Add new automapper Profile for this class to create mapping
            config.AddProfile(new CustomMappingProfile(list));
        }
    }
}
