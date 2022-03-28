using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PAS.API.Infrastructure
{
    /// <summary>
    /// PASDbContext class
    /// </summary>
    public class PASDbContext : BaseDbContext
    {
        // private DbConnection _connection;
        private readonly string _dbType;
        private readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        public PASDbContext() : base()
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="configuration"></param>
        public PASDbContext(DbContextOptions<PASDbContext> options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(options)
        {
            _dbType = configuration.GetValue<string>("Database:Type");
            _connectionString = configuration.GetValue<string>("Database:ConnectionString");
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="connectionString"></param>
        public PASDbContext(DbContextOptions<PASDbContext> options, IHttpContextAccessor httpContextAccessor, string connectionString) : base(options)
        {
            _dbType = "SQLServer";
            _connectionString = connectionString;
        }


        /// <summary>
        /// OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
            {
                switch (_dbType)
                {
                    case "SQLServer":
                        optionsBuilder.UseSqlServer(_connectionString);
                        break;
                    default:
                        optionsBuilder.UseSqlServer(_connectionString);
                        break;
                }
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
