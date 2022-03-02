using System.ComponentModel.DataAnnotations;

namespace PAS.API.Models
{
    public class BaseCodeList
    {
        /// <summary>
        /// Code List Title
        /// </summary>
        [MinLength(1), MaxLength(50)]
        public string CodeListTitle { get; set; }

        /// <summary>
        /// Code List Reference
        /// </summary>
        [MinLength(1), MaxLength(50)]
        public string CodeListReference { get; set; }

        /// <summary>
        /// Code List Version
        /// </summary>
        public int? CodeListVersion { get; set; }

        /// <summary>
        /// Code List Description
        /// </summary>
        public string CodeListDescription { get; set; }
    }
}
