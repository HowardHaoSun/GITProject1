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
    
    public class ResourcesController : ApiController
    {
        [Route("api/resources/{name}/{culture}",Name="Test")]
        public string GetCulture(string name, string culture="zh")
        {
            CultureInfo curUICulture = CultureInfo.CurrentUICulture;
            CultureInfo curCulture = CultureInfo.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                return SportsStore.Properties.Resources.ResourceManager.GetString(name.ToLower());
            }
            catch
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh");
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
