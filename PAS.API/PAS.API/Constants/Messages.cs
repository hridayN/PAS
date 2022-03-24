using PAS.API.Models;

namespace PAS.API.Constants
{
    /// <summary>
    /// Class used to store constant values in project
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// This message shows when import code list api is called without Excel file
        /// </summary>
        public const string CodeListFileMissing = "Please upload excel file for importing code list.";

        /// <summary>
        /// This message shows when import code list api is called with any other file 
        /// </summary>
        public const string InvalidExcelFile = "Only excel file can be imported.";

        /// <summary>
        /// This message shows when code list is duplicated
        /// </summary>
        public const string CodeListDuplicated = "Duplicate Code List.";
        
        /// <summary>
        /// This message shows when code list is duplicated
        /// </summary>
        public const string CodeListExists = "Code List already exists with same data.";

        /// <summary>
        /// This message shows when sheet does not have mandatory column
        /// </summary>
        public const string ExcelDataMissing = "Sheet {0} does not have mandatory column {1}.";

        /// <summary>
        /// This message shows when data saved successfully
        /// </summary>
        public const string DataSaved = "Data saved successfully.";

        /// <summary>
        /// This message shows when import code list imported successfully 
        /// </summary>
        public const string CodelistImported = "CodeList imported successfully.";

        /// <summary>
        /// This message shows when sheet is not in correct format 
        /// </summary>
        public const string ExcelIsNotValid = "This excel is not in valid format.";

        /// <summary>
        /// This message shows when request is not valid
        /// </summary>
        public const string RequestNotValid = "Request Not valid.";

        /// <summary>
        /// this message shows when bad request 
        /// </summary>
        public const string BadRequest = "Bad Request.";
    }
}
