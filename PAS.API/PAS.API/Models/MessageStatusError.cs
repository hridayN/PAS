namespace PAS.API.Models
{
    /// <summary>
    /// Stores error code and description
    /// </summary>
    public class MessageStatusError
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}
