using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Infrastructure;
using API.Domain.Infrastructure;

namespace API.Domain.Repository
{
    public interface IApiUserRecords
    {
        /// <summary>
        /// Gets a API user by their api key. Returns null if they aren't found
        /// </summary>
        /// <param name="guid">Unique Guid Key</param>
        /// <returns><see cref="ApiUser"/></returns>
        ApiUser Get(Guid guid);
    }
}
