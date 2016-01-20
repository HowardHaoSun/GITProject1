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
        //在路由中增加特性限制，用于处理路由带入的值
        //现在cluture为int，只会相应int。
        //相关初始化工作在WebApiConfig.cs中
        [Route("api/resources/{name}/{culture:int}",Name="Test")]
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
