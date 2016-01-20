using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace SportsStore.Controllers
{
    [Route("api/resources/{name}/{culture}")]
    public class ResourcesController : ApiController
    {
        public string GetCulture(string name, string culture)
        {
            CultureInfo curUICulture = CultureInfo.CurrentUICulture;
            CultureInfo curCulture = CultureInfo.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                return SportsStore.Properties.Resources.ResourceManager.GetString(name.ToLower());
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = curUICulture;
                Thread.CurrentThread.CurrentCulture = curCulture;
            }
        }
    }
}
