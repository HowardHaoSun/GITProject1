using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web;
using System.Web.Http.ModelBinding;
namespace SportsStore.MultiObjectModelBinding
{
    public class TypeConverterMobelBinderProvider:ModelBinderProvider
    {
        public override IModelBinder GetBinder(System.Web.Http.HttpConfiguration configuration, Type modelType)
        {
            if (TypeDescriptor.GetConverter(modelType).CanConvertFrom(typeof(string)))
            {
                return new TypeConverterMobelBinder();
            }
            return null;
            //throw new NotImplementedException();
        }
    }
}