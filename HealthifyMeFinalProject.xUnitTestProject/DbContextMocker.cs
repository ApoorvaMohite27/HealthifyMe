using HealthifyMeFinalProject.Data;
using HealthifyMeFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthifyMeFinalProject.xUnitTestProject
{
    // static = singleton
    public static class DbContextMocker
    {
        public static ApplicationDbContext GetApplicationDbContext(string dbName)
        {
            // Create a fresh service provider for the InMemory Database instance
            var serviceProvider = new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider();

            // Create a new options instance telling the context
            // to use InMemory database with the new Service provider
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(dbName)
                            .UseInternalServiceProvider(serviceProvider)
                            .Options;

            // Create the instance of the DbContext
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the InMemory Database to seed the test data
            dbContext.SeedData();

            return dbContext;
        }

        // NOTE: InMemory Databases do not support Relationships, and Database Constraints properly
        // So, seed the Identity Column Values also.
        internal static readonly FeedBackForm[] TestData_FeedBackForms =
        {
            new FeedBackForm
            {
                FormId = 1,
                CustomerName = "First Customer",
                Rating = "Good",
                Comments = "Nice"
            },
            new FeedBackForm
            {
                FormId = 2,
                CustomerName = "Second Customer",
                Rating = "Worst",
                Comments = "Nice"
            },
        };

        /// <summary>
        /// An extension Method for the DbContext object to Seed the Test Data.
        /// </summary>
        /// <param name="context"></param>

        private static void SeedData(this ApplicationDbContext context)
        {
            context.FeedBackForms.AddRange(TestData_FeedBackForms);

            // Commit the changes to the database
            context.SaveChanges();
        }
    }
}
