using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SportsStore.Domain.Entities;
using SportsStore.Infrastructure.Binders;
using System.Web.Http.ModelBinding;
using SportsStore.MultiObjectModelBinding;
namespace SportsStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //增加复杂Model绑定服务
            GlobalConfiguration.Configuration.Services.ReplaceRange(
                typeof(ModelBinderProvider), new ModelBinderProvider[]{
                    new TypeConverterMobelBinderProvider(),
                    new ComplexModelDtoModelBinderProvider(),
                    new MutableObjectModelBinderProvider()
                });
            GlobalConfiguration.Configuration.Services.ReplaceRange(
                typeof(System.Web.Http.ValueProviders.ValueProviderFactory), new System.Web.Http.ValueProviders.ValueProviderFactory[] { 
                    new StaticValueProviderFactory()}
                );
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
