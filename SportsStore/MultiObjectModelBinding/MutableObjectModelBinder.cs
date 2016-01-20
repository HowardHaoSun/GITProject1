using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Metadata;
using System.Reflection;

namespace SportsStore.MultiObjectModelBinding
{
    public class MutableObjectModelBinder:IModelBinder
    {
        internal static bool CanBindType(Type modelType)
        {
            if(TypeDescriptor.GetConverter(modelType).CanConvertFrom(typeof(string)))
            {
                return false;
            }
            if(modelType == typeof(ComplexModelDto))
            {
                return false;
            }
            return true;
        }
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if(!CanBindType(bindingContext.ModelType))
            {
                return false;
            }
            if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                return false;
            }
            bindingContext.Model = Activator.CreateInstance(bindingContext.ModelType);
            ComplexModelDto dto = new ComplexModelDto(bindingContext.ModelMetadata, bindingContext.PropertyMetadata.Values);
            ModelBindingContext subContext = new ModelBindingContext(bindingContext)
            {
                ModelMetadata = actionContext.GetMetadataProvider().GetMetadataForType(() => dto, typeof(ComplexModelDto)),
                ModelName = bindingContext.ModelName
            };

            actionContext.Bind(subContext);

            foreach(KeyValuePair<ModelMetadata,ComplexModelDtoResult>item in dto.Results)
            {
                ModelMetadata propertyMetaData = item.Key;
                ComplexModelDtoResult dtoRestult = item.Value;
                if(dtoRestult != null)
                {
                    PropertyInfo propertyInfo = bindingContext.ModelType.GetProperty(propertyMetaData.PropertyName);
                    if(propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(bindingContext.Model,dtoRestult.Model);
                    }
                }
            }
            return true;
        }
    }
}