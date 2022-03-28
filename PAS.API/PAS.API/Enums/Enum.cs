namespace PAS.API.Enums
{
    /// <summary>
    /// Enum class
    /// </summary>
    public class Enum
    {
        /// <summary>
        /// OperatorType values
        /// </summary>
        public enum OperatorType
        {
            Or,
            And
        }

        /// <summary>
        /// FilterType values
        /// </summary>
        public enum FilterType
        {
            Equal,
            NotEqual,
            GreaterThan,
            LessThan,
            GreaterThanOrEqual,
            LessThanOrEqual,
            Contains,
            StartsWith,
            EndsWith,
            Range
        }
    }
}
