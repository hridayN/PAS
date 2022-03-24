using PAS.API.Models;

namespace PAS.API.Constants
{
    /// <summary>
    /// Class to define all the constant variables for error codes
    /// </summary>
    public class Errors
    {
        /// <summary>
        /// Error constant for no file uploaded for import code list
        /// </summary>
        public static readonly MessageStatusError CodeListFileMissing = new MessageStatusError { ErrorCode = "001", ErrorDescription = Messages.CodeListFileMissing };

        /// <summary>
        /// Error constant for no file uploaded for import code list
        /// </summary>
        public static readonly MessageStatusError InvalidExcelFile = new MessageStatusError { ErrorCode = "002", ErrorDescription = Messages.InvalidExcelFile };

        /// <summary>
        /// Error constant for duplicate code list
        /// </summary>
        public static readonly MessageStatusError CodeListExists = new MessageStatusError { ErrorCode = "003", ErrorDescription = Messages.CodeListDuplicated };

        /// <summary>
        /// Error constant for no file uploaded for import code list
        /// </summary>
        public static readonly MessageStatusError ExcelDataMissing = new MessageStatusError { ErrorCode = "004", ErrorDescription = Messages.ExcelDataMissing };

        /// <summary>
        /// Constant for NotFoundError Code
        /// </summary>
        public static readonly MessageStatusError RequestNotValid = new MessageStatusError { ErrorCode = "005", ErrorDescription = Messages.RequestNotValid };

        /// <summary>
        /// Constant for BadRequest Code
        /// </summary>
        public static readonly MessageStatusError BadRequest = new MessageStatusError { ErrorCode = "006", ErrorDescription = Messages.BadRequest };
    }
}
