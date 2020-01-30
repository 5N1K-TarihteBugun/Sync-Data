using System;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;
using Sync.Common;
using Sync.Console.Business;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Sync.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true);

                var configuration = builder.Build();

                var serviceProvider = new ServiceCollection()
                    .AddSingleton<ICustomDateFormat, CustomDateFormat>(provider => new CustomDateFormat("tr-TR"))
                    .AddSingleton<WebDocument>()
                    .AddTransient<ISyncService, SyncService>()
                    .AddSingleton(arg =>
                    {
                        DefaultTypeMap.MatchNamesWithUnderscores = true;

                        var connectionString = configuration.GetConnectionString("Database");

                        var connection = new NpgsqlConnection(connectionString);

                        var compiler = new PostgresCompiler();

                        var queryFactory = new QueryFactory(connection, compiler)
                        {
                            Logger = compiled => { System.Console.WriteLine(compiled.ToString()); }
                        };

                        return queryFactory;
                    })
                    .AddSingleton(typeof(ProjectSettings),
                        delegate { return configuration.GetSection("ProjectSetting").Get<ProjectSettings>(); })
                    .AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                        loggingBuilder.AddNLog(configuration);
                    })
                    .BuildServiceProvider();

                var syncService = serviceProvider.GetService<ISyncService>();

                var datesOfYear = syncService.GetDaysOfYears(new[]
                {
                    2012,
                    2013,
                    2014,
                    2015,
                    2016,
                    2017,
                    2018,
                    2019,
                    2020
                });

                foreach (var date in datesOfYear)
                {
                    await syncService.SyncData(date);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}