using Appets.DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appets.DataAccess
{
    public class AppetsDbContext : AppDbContext
    {
        public static string ConnectionString { get; set; }


        public AppetsDbContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            if (!OptionsBuilder.IsConfigured)
            {
                OptionsBuilder.UseSqlServer(ConnectionString);
                
            }

            base.OnConfiguring(OptionsBuilder);
        }


        public static void BuildConnectionString(string connectionString)
        {
            var connString = new SqlConnectionStringBuilder
            {
                ConnectionString = connectionString
            };

            ConnectionString = connString.ConnectionString;
        }
    }
}
