using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF;
using Microsoft.EntityFrameworkCore;
using ChatApi.Services.Database.SetupHelpers;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ChatApi.HelperExtensions
{
    public static class DatabaseSetupExtension
    {
        /// <summary>
        /// Migrates and seeds database.
        /// 
        /// In case of exeption deletes whole database.
        /// </summary>
        /// <param name="webHost"></param>
        /// <returns></returns>
        public static async Task<IHost> SetupDatabaseAsync(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())                                                      //#A
            {
                var services = scope.ServiceProvider;                                                               //#B

                var config = services.GetRequiredService<IConfiguration>();                                         //#B
                var context = services.GetRequiredService<ChatDbContext>();                                         //#B

                try
                {
                    var arePendingMigrations = context.Database.GetPendingMigrations().Any();                       //#C
                    await context.Database.MigrateAsync();

                    if (arePendingMigrations)                                                                       //#C
                    {
                        var seedSettings = config.GetSection("SeedSettings");                                       //#D

                        var seedDataSubDirectory = seedSettings["DataSubDirectory"];                                //#D
                        var seedDataDirectory = Path.Combine(Environment.CurrentDirectory, seedDataSubDirectory);   //#D

                        var seedUsersFileName = seedSettings["UsersSeedFile"];                                      //#D

                        var seedGroupsFileName = seedSettings["GroupsSeedFile"];                                    //#D

                        await context.SeedUsersWithGroups(seedDataDirectory, seedUsersFileName, seedGroupsFileName);//#E
                    }
                }
                catch (Exception ex)                                                                                //#F
                {
                    Console.WriteLine("An error occurred while creating/migrating or seeding the database.\n"
                                      + ex.Message);

                    await context.Database.EnsureDeletedAsync();

                }
            }

            return webHost;
        }
    }
    /*====================================================
    #A: Create scope where all required services will be encapsulated.
    #B: Get config with seed files names and DB context.
    #C: If there are penging migrations, try to fill the database.
    #D: Get seed files names from config.
    #E: Seed users with groups from json files, only if database is empty.
    #F: If exeption occured, send message and detete database. 
        If database if fully migarted and error occured on the seeding stage, the seeding script would not be executed next time.
    ====================================================*/
}
