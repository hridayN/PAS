using AutoMapper;
using System;

namespace PAS.API.Mapper
{
    // The best implementation of AutoMapper for class libraries -> https://www.abhith.net/blog/using-automapper-in-a-net-core-class-library/
    /// <summary>
    /// Object Mapper
    /// </summary>
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<PasDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper => Lazy.Value;
    }
}
