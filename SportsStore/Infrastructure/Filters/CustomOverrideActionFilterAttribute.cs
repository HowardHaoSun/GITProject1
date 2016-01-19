using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using System.Web.Mvc.Filters;
namespace SportsStore.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,Inherited=true,AllowMultiple=false)]
    public class CustomOverrideActionFilterAttribute: FilterAttribute,IOverrideFilter
    {
        public Type FiltersToOverride
        {
            get { return typeof(IActionFilter); }
        }
    }
}