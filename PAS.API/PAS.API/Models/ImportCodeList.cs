namespace PAS.API.Models
{
    /// <summary>
    /// Model for import code list response
    /// </summary>
    public class ImportCodeList
    {
        /// <summary>
        /// Code List Reference of imported code list
        /// </summary>
        public string CodeListReference { get; set; }

        /// <summary>
        /// Code list version of imported code list
        /// </summary>
        public int? CodeListVersion { get; set; }

        /// <summary>
        /// Status of code list imported
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// No of records processed
        /// </summary>
        public int Records { get; set; }
    }
}
