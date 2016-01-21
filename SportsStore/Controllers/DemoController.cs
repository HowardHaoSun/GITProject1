using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsStore.MultiObjectModelBinding;
using System.Web.Http.ModelBinding;
using System.Web.Http.Cors;

namespace SportsStore.Controllers
{
   
    public class DemoController : ApiController
    {
        [EnableCors("*",headers:"*",methods:"*")]
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            StaticValueProviderFactory.Clear();
            StaticValueProviderFactory.Add("Name", "张三");
            StaticValueProviderFactory.Add("PhoneNo", "123456789");
            StaticValueProviderFactory.Add("EmailAddress", "zhangsan@gmail.com");
            StaticValueProviderFactory.Add("Address.Province", "江苏省");
            StaticValueProviderFactory.Add("Address.City", "苏州市");
            StaticValueProviderFactory.Add("Address.District", "工业园区");
            StaticValueProviderFactory.Add("Address.Street", "星湖街328号");
        }
        public IEnumerable<Contact> Get([ModelBinder]IEnumerable<Contact> contacts)
        {
            return contacts;
        }
    }
}
