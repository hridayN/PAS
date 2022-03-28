namespace PAS.API.Utilites.DBConnector
{
    /// <summary>
    /// DB Options class
    /// </summary>
    public class DataBaseOptions
    {
        /// <summary>
        /// Database string
        /// </summary>
        public const string DataBase = "DataBase";

        /// <summary>
        /// DB Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// DBConnectionString
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
