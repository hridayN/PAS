namespace PAS.API.Models
{
    /// <summary>
    /// MessageInformation model to be returned for every response
    /// </summary>
    public class MessageInformation
    {
        /// <summary> This captures the details of success/error status-code, description and details </summary>
        public MessageStatus MessageStatus { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MessageInformation()
        {
            MessageStatus = new MessageStatus();
        }
    }
}
