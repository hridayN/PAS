using PAS.API.DTO.Base;
using PAS.API.Models;
using System.Collections.Generic;

namespace PAS.API.DTO
{
    /// <summary>
    /// ImportCodeList response model
    /// </summary>
    public class ImportCodeListResponse : BaseResponse
    {
        /// <summary>
        /// Imported Code list Status
        /// </summary>
        public List<ImportCodeList> ImportStatus { get; set; }
    }
}
