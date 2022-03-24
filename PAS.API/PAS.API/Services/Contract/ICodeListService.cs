using Microsoft.AspNetCore.Http;
using PAS.API.DTO;
using System.Threading.Tasks;

namespace PAS.API.Services.Contract
{
    /// <summary>
    /// Interface for reference data 
    /// </summary>
    public interface ICodeListService
    {
        /// <summary>
        /// Save reference data in DB
        /// </summary>
        /// <param name="loadCodeListRequest"></param>
        /// <returns></returns>
        public Task<LoadCodeListResponse> LoadCodeList(LoadCodeListRequest loadCodeListRequest);

        /// <summary>
        /// Imports Code List From Excel File
        /// </summary>
        /// <param name="file">Form File</param>
        /// <param name="importCodeListRequest">importCodeListRequest</param>
        /// <returns></returns>
        public Task<ImportCodeListResponse> ImportCodeList(IFormFile file, ImportCodeListRequest importCodeListRequest);
    }
}
