using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PAS.API.Infrastructure.Entities;
using PAS.API.Models;

namespace PAS.API.Infrastructure
{
    /// <summary>
    /// BaseDbContext
    /// </summary>
    public class BaseDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseDbContext() : base()
        {
        }

        /// <summary>
        /// Parameterised constructor
        /// </summary>
        /// <param name="options"></param>
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// CodeList table
        /// </summary>
        public DbSet<CodeListEntity> CodeList { get; set; }

        /// <summary>
        /// Model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeListEntity>(cl =>
            {
                cl.Property(p => p.EnumerationCodeList).HasConversion(c => JsonConvert.SerializeObject(c), x => JsonConvert.DeserializeObject<List<EnumerationCode>>(x));
            });
        }
    }
}
