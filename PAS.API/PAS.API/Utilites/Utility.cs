using PAS.API.Models;
using System.Collections.Generic;

namespace PAS.API.Utilites
{
    /// <summary>
    /// Static Utility class for common utility functions
    /// </summary>
    public static class Utility
    {
        public static void AddErrorMessage(MessageInformation messageInformation, MessageStatusError error, string errorMessage = null)
        {
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                error = new MessageStatusError 
                {
                    ErrorCode = error.ErrorCode,
                    ErrorDescription = error.ErrorDescription
                };
            }

            messageInformation.MessageStatus.Errors = messageInformation.MessageStatus.Errors ?? new List<MessageStatusError>();
            messageInformation.MessageStatus.Errors.Add(error);
        }

        /// <summary>
        /// Sets the statusCode of message
        /// </summary>
        /// <param name="status">statusCode code</param>
        /// <param name="statusDescription">statusCode description</param>
        /// <param name="messageInformation">messageInformation object</param>
        public static void SetStatus(int statusCode, string statusDescription, MessageInformation messageInformation)
        {
            messageInformation.MessageStatus.StatusCode = statusCode;
            messageInformation.MessageStatus.StatusDescription = statusDescription;
        }

        /// <summary>
        /// Sets the Status of response message
        /// </summary>
        /// <param name="messageStatus">message Status</param>
        /// <param name="messageInformation">Message Information</param>
        /// <param name="statusMessage">error message needs to be added</param>
        public static void SetOkStatus(MessageStatus messageStatus, MessageInformation messageInformation, string statusMessage = null)
        {

            messageInformation.MessageStatus.StatusDescription = messageStatus.StatusDescription;
            messageInformation.MessageStatus.StatusCode = 200;
            messageInformation.MessageStatus.Errors = null;
            if (!string.IsNullOrWhiteSpace(statusMessage))
            {
                messageInformation.MessageStatus.StatusDescription = statusMessage;
            }
        }
    }
}
