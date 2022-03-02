using AutoMapper;
using PAS.API.Infrastructure.Entities;
using PAS.API.Models;

namespace PAS.API.Mapper
{
    /// <summary>
    /// Mapper class to map entity to objects and vice a versa
    /// </summary>
    public class PasDtoMapper : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PasDtoMapper()
        {
            CreateMap<CodeListEntity, CodeList>();
            CreateMap<CodeList, CodeListEntity>();
        }
    }
}
