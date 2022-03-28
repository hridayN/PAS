using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PAS.API.Utilites.DBConnector;

namespace PAS.API.Utilites
{
    public static class DBServiceExtension
    {
        public static IServiceCollection SetUpDatabase<T>(this IServiceCollection services, IConfiguration configuration, string migrationTableName, string migrationTableSchemaName) where T : DbContext
        {
            DataBaseOptions dataBaseOptions = new DataBaseOptions();
            configuration.Bind("DataBase", dataBaseOptions);
            services.AddSingleton(dataBaseOptions);
            services.AddDbContext<T>(delegate (DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(dataBaseOptions.ConnectionString))
                {
                    string type = dataBaseOptions.Type;
                    if (type == "SQLServer")
                    {
                        optionsBuilder.UseSqlServer(dataBaseOptions.ConnectionString, delegate (SqlServerDbContextOptionsBuilder a)
                        {
                            a.MigrationsHistoryTable(migrationTableName, migrationTableSchemaName);
                        });
                    }
                }
            });
            SetUpDBHealtChecks(services, dataBaseOptions);
            return services;
        }

        private static void SetUpDBHealtChecks(IServiceCollection services, DataBaseOptions dataBaseOptions)
        {
            IHealthChecksBuilder builder = services.AddHealthChecks();
            string type = dataBaseOptions.Type;
            if (type == "SQLServer")
            {
                builder.AddSqlServer(dataBaseOptions.ConnectionString);
            }
        }
    }
}
