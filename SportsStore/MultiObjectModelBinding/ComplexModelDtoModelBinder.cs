using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Metadata;
using System.Reflection;
namespace SportsStore.MultiObjectModelBinding
{
    public class ComplexModelDtoModelBinder:IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ComplexModelDto dto = bindingContext.Model as ComplexModelDto;
            if(dto == null)
            {
                return false;
            }
            foreach(ModelMetadata property in dto.PropertyMetadata)
            {
                ModelBindingContext subContext = new ModelBindingContext(bindingContext)
                {
                    ModelMetadata=property,
                    ModelName = ModelNameBuilder.CreatePropertyModelName(bindingContext.ModelName,property.PropertyName)
                };
                if (actionContext.Bind(subContext))
                {
                    dto.Results[property] = new ComplexModelDtoResult(subContext.Model,subContext.ValidationNode);
                }
            }
            return true;
        }
    }

    public static class ModelNameBuilder
    {
        public static string CreatePropertyModelName(string prifix, string propertyName)
        {
            if(string.IsNullOrEmpty(prifix))
            {
                return (propertyName ?? string.Empty);
            }
            if(!string.IsNullOrEmpty(propertyName))
            {
                return (prifix + "." + propertyName);
            }
            return (prifix ?? string.Empty);
        }
    }
}