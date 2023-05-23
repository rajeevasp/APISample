using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Infrastructure;
using API.Domain.Repository;
using Dapper;
using System.Data;



namespace API.Data.Repository
{
    public class ApiUserRecords : IApiUserRecords
    {
        /// <summary>
        /// Gets a API user by their api key. Returns null if they aren't found
        /// </summary>
        /// <param name="guid">Unique Guid Key</param>
        /// <returns><see cref="ApiUser"/></returns>
        public ApiUser Get(Guid guid)
        {
            using (IDbConnection connection = Connection.GetConnection("UV_APIs"))
            {
                string sql = @"SELECT [Id]
                                     ,[Name]
                                     ,[PrivateKey]
                                     ,[AuthorizedAPIs]
                                     ,[SignupDate]
                                 FROM [dbo].[User]
                                WHERE [Id] = @key";
                return connection.Query<ApiUser>(sql, new { key = guid })
                    .FirstOrDefault();
            }
        }
    }
}
