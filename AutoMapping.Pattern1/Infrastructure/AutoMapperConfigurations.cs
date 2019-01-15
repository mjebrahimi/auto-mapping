using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapping.Pattern1.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(configuration =>
            {
                configuration.ConfigureAutoMapperForDto();
            });

            //Compile mapping after configuration to boost map speed
            Mapper.Configuration.CompileMappings();
        }

        public static void ConfigureAutoMapperForDto(this IMapperConfigurationExpression config)
        {
            config.ConfigureAutoMapperForDto(Assembly.GetEntryAssembly());
        }

        public static void ConfigureAutoMapperForDto(this IMapperConfigurationExpression config, params Assembly[] assemblies)
        {
            var dtoTypes = GetDtoTypes(assemblies);

            var mappingTypes = dtoTypes
                .Select(type =>
                {
                    var arguments = type.BaseType.GetGenericArguments();
                    return new
                    {
                        DtoType = arguments[0],
                        EntityType = arguments[1]
                    };
                }).ToList();

            foreach (var mappingType in mappingTypes)
                config.CreateMappingAndIgnoreUnmappedProperties(mappingType.EntityType, mappingType.DtoType);
        }

        public static void CreateMappingAndIgnoreUnmappedProperties(this IMapperConfigurationExpression config, Type entityType, Type dtoType)
        {
            var mappingExpression = config.CreateMap(entityType, dtoType).ReverseMap();

            //Ignore mapping to any property of source (like Post.Categroy) that dose not contains in destination (like PostDto)
            //To prevent from wrong mapping. for example in mapping of "PostDto -> Post", automapper create a new instance for Category (with null catgeoryName) because we have CategoryName property that has null value
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }
        }

        public static IEnumerable<Type> GetDtoTypes(params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

            var dtoTypes = allTypes.Where(type =>
                    type.IsClass && !type.IsAbstract && type.BaseType != null && type.BaseType.IsGenericType &&
                    (type.BaseType.GetGenericTypeDefinition() == typeof(BaseDto<,>) ||
                    type.BaseType.GetGenericTypeDefinition() == typeof(BaseDto<,,>)));

            return dtoTypes;
        }
    }
}
