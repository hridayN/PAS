using PAS.API.DTO.Base;
using PAS.API.Models;

namespace PAS.API.DTO
{
    /// <summary>
    /// SearchCodeLists Response model
    /// </summary>
    public class SearchCodeListsResponse : BaseResponse
    {
        /// <summary>
        /// Matching CodeLists
        /// </summary>
        public IEnumerable<CodeList> CodeLists { get; set; }
    }
}
