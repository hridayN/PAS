using System.ComponentModel.DataAnnotations;
using PAS.API.DTO.Base;

namespace PAS.API.DTO
{
    /// <summary>
    /// SearchCodeLists Request Model
    /// </summary>
    public class SearchCodeListsRequest : BaseRequest
    {
        /// <summary>
        /// Code List Reference
        /// </summary>
        [MinLength(1), MaxLength(50)]
        public string CodeListReference { get; set; }
    }
}
