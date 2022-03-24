using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PAS.API.Infrastructure.Entities;
using PAS.API.Models;
using System.Collections.Generic;

namespace PAS.API.Infrastructure
{
    /// <summary>
    /// PASDbContext class
    /// </summary>
    public class PASDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PASDbContext() : base()
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
