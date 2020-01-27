using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;
using Sync.Business;
using Sync.Common;
using Sync.Model;

namespace Sync.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",true,true);

            var configuration = builder.Build();
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICustomDateFormat, CustomDateFormat>(provider => new CustomDateFormat("tr-TR"))
                .AddSingleton<WebDocument>()
                .AddTransient<ISyncService, SyncService>()
                .AddScoped(arg =>
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
                .AddSingleton(typeof(ProjectSettings), delegate
                {
                    return configuration.GetSection("ProjectSetting").Get<ProjectSettings>();
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
    }
}