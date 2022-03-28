using PAS.API.Infrastructure.Contracts;
using PAS.API.Infrastructure.Entities;
using PAS.API.Services.Contract;

namespace PAS.API.Services.Core
{
    /// <summary>
    /// RepositoryService class
    /// </summary>
    public class RepositoryService : IRepositoryService
    {
        /// <summary>
        /// _codeListRepository variable
        /// </summary>
        private readonly IRepository<CodeListEntity> _codeListRepository;

        /// <summary>
        /// CodeListRepository
        /// </summary>
        public IRepository<CodeListEntity> CodeListRepository { get => this._codeListRepository; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeListRepository"></param>
        public RepositoryService(IRepository<CodeListEntity> codeListRepository)
        {
            _codeListRepository = codeListRepository;
        }
    }
}
