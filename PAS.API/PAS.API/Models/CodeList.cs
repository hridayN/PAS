using System.Collections.Generic;

namespace PAS.API.Models
{
    /// <summary>
    /// CodeList model
    /// </summary>
    public class CodeList : BaseCodeList
    {
        /// <summary>
        /// List of EnumerationCodes against a reference
        /// </summary>
        public IEnumerable<EnumerationCode> EnumerationCodeList { get; set; }
    }
}
