using PAS.API.DTO.Base;

namespace PAS.API.DTO
{
    /// <summary>
    /// ImportCodeList request model
    /// </summary>
    public class ImportCodeListRequest : BaseRequest
    {
        /// <summary>
        /// Code-list References
        /// </summary>
        public IEnumerable<string> CodeListReferences { get; set; }
    }
}
