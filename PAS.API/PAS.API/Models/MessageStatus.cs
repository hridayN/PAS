using System.Collections.Generic;

namespace PAS.API.Models
{
    /// <summary>
    /// MessageStatus model
    /// </summary>
    public class MessageStatus
    {
        /// <summary>
        /// Status code for the response
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Message status description. General explanatory text associated with the overall message status.
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Response errors
        /// </summary>
        public List<MessageStatusError> Errors { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MessageStatus()
        {
            StatusCode = 200;
            Errors = new List<MessageStatusError>();
        }
    }
}
