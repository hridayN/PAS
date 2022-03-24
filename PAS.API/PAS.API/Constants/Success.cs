using PAS.API.Models;

namespace PAS.API.Constants
{
    /// <summary>
    /// Class to define all sucess code for success message
    /// </summary>
    public static class Success
    {
        /// <summary>
        /// Message Comes when data is saved
        /// </summary>
        public static readonly MessageStatus DataSaved = new() { StatusCode = 200, StatusDescription = Messages.DataSaved };

        /// <summary>
        /// Message Comes when data is retrieved
        /// </summary>
        public static readonly MessageStatus CodelistImported = new() { StatusCode = 200, StatusDescription = Messages.CodelistImported };
    }
}
