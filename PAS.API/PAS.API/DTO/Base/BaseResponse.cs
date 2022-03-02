namespace PAS.API.DTO.Base
{
    /// <summary>
    /// Base Response class
    /// </summary>
    public class BaseResponse : BaseDto
    {
        /// <summary>
        /// Response code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; }
    }
}
