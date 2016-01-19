using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System.Configuration;
namespace SportsStore.Infrastructure
{
    //Need to Register in App_Start/NinjectWebCommon.cs
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.GetService(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        public void AddBindings()
        {

            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            .WithConstructorArgument("settings", emailSettings);
        }
    }
}