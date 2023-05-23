using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using API.Data.Configuration;

namespace API.Data.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DataContextCollection : Dictionary<string, object>
    {
        #region Constructors

        public DataContextCollection()
            : base()
        {
            base.Add("API.Data.Configuration.DomainNameDbContext", new DomainNameDbContext());
            //base.Add("UWA.Data.Legacy.UWADataContext", new UWA.Data.Legacy.UWADataContext());
            //base.Add("UWA.Data.Legacy.ACAR.ACARDataContext", new UWA.Data.Legacy.ACAR.ACARDataContext());
            //base.Add("UWA.Data.Legacy.ATS.ATSDataContext", new UWA.Data.Legacy.ATS.ATSDataContext());
            //base.Add("UWA.Data.Legacy.UvAuthorizationDataContext", new UWA.Data.Legacy.UvAuthorizationDataContext());
            //base.Add("UWA.Data.Legacy.notamsEntities", new UWA.Data.Legacy.notamsEntities());
            //base.Add("UWA.Data.Legacy.SigmetsEntities", new UWA.Data.Legacy.SigmetsEntities());
            //base.Add("UWA.Data.Legacy.WeatherEntities", new UWA.Data.Legacy.WeatherEntities());
        }
        #endregion
    }
}
