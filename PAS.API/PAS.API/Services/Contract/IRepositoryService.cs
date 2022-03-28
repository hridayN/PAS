using PAS.API.Infrastructure.Contracts;
using PAS.API.Infrastructure.Entities;

namespace PAS.API.Services.Contract
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepositoryService
    {
        /// <summary>
        /// CodeList Repository
        /// </summary>
        IRepository<CodeListEntity> CodeListRepository { get; }
    }
}
