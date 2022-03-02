using PAS.API.DTO.Base;
using PAS.API.Models;

namespace PAS.API.DTO
{
    /// <summary>
    /// LoadCodeList Request model
    /// </summary>
    public class LoadCodeListRequest : BaseRequest
    {
        /// <summary>
        /// CodeList object
        /// </summary>
        public CodeList CodeList { get; set; }
    }
}
