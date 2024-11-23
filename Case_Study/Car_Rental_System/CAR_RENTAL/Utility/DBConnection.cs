using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CAR_RENTAL_SYSTEM.Utility
{
    internal static class DBConnection
    {
        private static IConfiguration _iconfiguration;

        static DBConnection()
        {
            GetAppSettingsFile();
        }


        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsetting.json");
            _iconfiguration = builder.Build();
        }

        public static string GetConnection()
        {
            return _iconfiguration.GetConnectionString("LocalConnectionString");
        }

        //private static string connection = "Server=DESKTOP-BAHKPDL;Database=CRSDB;Trusted_Connection=True";

        //public static SqlConnection GetConnection()
        //{
        //    return new SqlConnection(connection);
        //}

    }
}

