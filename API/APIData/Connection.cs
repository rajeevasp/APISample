using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data
{
    /// <summary>
    /// Helper to create open connections
    /// </summary>
    public static class Connection
    {
        /// <summary>
        /// Default connection string
        /// </summary>
        private static string defaultConnectionName = "UV_APIs";

        /// <summary>
        /// Gets a open connection to the database specified by the connection string
        /// name, or falls back to the default connection string
        /// </summary>
        /// <param name="connectionStringName">Optional connection string</param>
        /// <returns>Open IDbConnection</returns>
        internal static IDbConnection GetConnection(string connectionStringName = "") 
        {
            if (!string.IsNullOrEmpty(connectionStringName))
            {
                defaultConnectionName = connectionStringName;
            }

            var setting = ConfigurationManager.ConnectionStrings[defaultConnectionName];
            var factory = DbProviderFactories.GetFactory(setting.ProviderName);
            var connection = factory.CreateConnection();

            if (connection == null)
            {
                throw new DataException(
                    string.Format("The provider {0} cannot create a new connection", setting.ProviderName)
                );
            }

            connection.ConnectionString = setting.ConnectionString;
            connection.Open();
            return connection;
        }
    }
}
