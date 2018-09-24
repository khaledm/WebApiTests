using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using WebApiTests.Services;

namespace WebApiTests.Models
{
    public class PurchaseOrderTypeModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(PurchaseOrderType))
            {
                return false;
            }

            //ValueProviderResult val = bindingContext.ValueProvider.GetValue(
            //    bindingContext.ModelName);
            //if (val == null)
            //{
            //    return false;
            //}

            //string key = val.RawValue as string;
            //if (key == null)
            //{
            //    bindingContext.ModelState.AddModelError(
            //        bindingContext.ModelName, "Wrong value type");
            //    return false;
            //}

            string requestContent = actionContext.Request.Content.ReadAsStringAsync().Result;

            PurchaseOrderType result;
            result = Deserialize<PurchaseOrderType>(new XmlMediaTypeFormatter(), requestContent);
            if (result != null)
            {
                bindingContext.Model = result;
                return true;
            }

            bindingContext.ModelState.AddModelError(
                bindingContext.ModelName, "Cannot convert value to PurchaseOrderType");

            return false;
        }

        T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            // Write the serialized string to a memory stream.
            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            // Deserialize to an object of type T
            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }
    }
}