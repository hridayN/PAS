using Microsoft.AspNetCore.Http;
using PAS.API.DTO;
using PAS.API.Infrastructure.Contracts;
using PAS.API.Infrastructure.Entities;
using PAS.API.Mapper;
using PAS.API.Models;
using PAS.API.Services.Contract;
using System.Linq;
using System.Threading.Tasks;

namespace PAS.API.Services.Core
{
    /// <summary>
    /// CodeList Service
    /// </summary>
    public class CodeListService : ICodeListService
    {
        /// <summary>
        /// Codelist repository variable
        /// </summary>
        private readonly IRepository<CodeListEntity> _codeListRepository;

        public CodeListService(IRepository<CodeListEntity> codeListRepository)
        {
            _codeListRepository = codeListRepository;
        }

        public Task<ImportCodeListResponse> ImportCodeList(IFormFile file, ImportCodeListRequest importCodeListRequest)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Save reference data in DB
        /// </summary>
        /// <param name="loadCodeListRequest"></param>
        /// <returns></returns>
        public async Task<LoadCodeListResponse> LoadCodeList(LoadCodeListRequest loadCodeListRequest)
        {
            return null;
        }

        private async Task<bool> SaveCodeList(CodeList codeList)
        {
            bool savedRecord = false;
            var dbLists = await _codeListRepository.GetOneAsyncWithOrder(x => x.CodeListReference.ToLower() == codeList.CodeListReference.ToLower(), "CodeListVersion", false);
            if (dbLists != null && dbLists.CodeListDescription == codeList.CodeListDescription && dbLists.CodeListTitle == codeList.CodeListTitle)
            {
                savedRecord = codeList.EnumerationCodeList.All(x => 
                dbLists.EnumerationCodeList.Any(y => x.Description == y.Description && x.DisplayValue == y.DisplayValue && x.CodeValue == y.CodeValue && 
                (x.SubCodeValue?.All(z => y.SubCodeValue?.Any(m => m == z) ?? true) ?? true))) &&
                     dbLists.EnumerationCodeList.All(x => codeList.EnumerationCodeList.Any(y => x.Description == y.Description && x.DisplayValue == y.DisplayValue && x.CodeValue == y.CodeValue && (x.SubCodeValue?.All(z => y.SubCodeValue?.Any(m => m == z) ?? true) ?? true)));
            }
            if (!savedRecord)
            {
                codeList.CodeListVersion = dbLists?.CodeListVersion == null ? codeList.CodeListVersion : dbLists.CodeListVersion + 1;
                var codeListEntity = ObjectMapper.Mapper.Map<CodeListEntity>(codeList);
                await _codeListRepository.AddAsync(codeListEntity);
            }
            return savedRecord;
        }
    }
}
