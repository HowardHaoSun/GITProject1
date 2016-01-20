using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace SportsStore.MultiObjectModelBinding
{
    public class MutableObjectModelBinderProvider:ModelBinderProvider
    {
        public override IModelBinder GetBinder(System.Web.Http.HttpConfiguration configuration, Type modelType)
        {
            //throw new NotImplementedException();
            if(MutableObjectModelBinder.CanBindType(modelType))
            {
                return new MutableObjectModelBinder();
            }
            return null;
        }
    }
}