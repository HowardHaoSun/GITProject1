using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsStore.MultiObjectModelBinding;
using System.Web.Http.ModelBinding;
namespace SportsStore.Controllers
{
    public class DemoController : ApiController
    {
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            StaticValueProviderFactory.Clear();
            StaticValueProviderFactory.Add("Name","张三");
            StaticValueProviderFactory.Add("Address.City","北京");
        }
        public Contact Get([ModelBinder] Contact contact)
        {
            return contact;
        }
    }
}
