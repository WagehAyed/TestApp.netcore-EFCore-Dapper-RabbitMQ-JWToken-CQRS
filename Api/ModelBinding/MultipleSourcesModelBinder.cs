using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestApp.Common;
namespace TestApp.Api.ModelBinding
{
    public class MultipleSourcesModelBinder : IModelBinder
    {
        private readonly IModelBinder _bodyBinder;
        private readonly IModelBinder _complexBinder;
        public MultipleSourcesModelBinder(IModelBinder bodyBinder, IModelBinder complexBinder)
        {
            _bodyBinder = bodyBinder;
            _complexBinder = complexBinder;
                 
        }
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
             await _bodyBinder.BindModelAsync(bindingContext);
            var bodyBinderModel = bindingContext.Result.Model;
            if (bindingContext.Result.IsModelSet)
            {
                bindingContext.Model= bodyBinderModel;
            }
            await _complexBinder.BindModelAsync(bindingContext);
            var complexBinderModel = bindingContext.Result.Model;
            if (bindingContext.ModelType.IsRecordType())
            {
                CreateModelFormRecords(bindingContext, bodyBinderModel, complexBinderModel);

            }
        }

        private static void CreateModelFormRecords(ModelBindingContext bindingContext,object bodyBinderModel, object complexBinderModel)
        {
            var ctor = bindingContext.ModelType.GetConstructors().OrderByDescending(x => x.GetParameters().Length).First();
            var args = new ArrayList();
            foreach (var item in ctor.GetParameters().OrderBy(x => x.Position))
            {
                var prop = bindingContext.ModelType.GetProperty(item.Name, BindingFlags.Public| BindingFlags.Instance| BindingFlags.IgnoreCase);
                var routeKey = bindingContext.ActionContext.RouteData.Values.Keys.FirstOrDefault(x => x.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                if (routeKey != null)
                    args.Add(prop.GetValue(complexBinderModel));
                else
                    args.Add(prop.GetValue(bodyBinderModel));
            }

            var finalModel = ctor.Invoke(args.ToArray());
            bindingContext.Result =ModelBindingResult.Success(finalModel);
            bindingContext.Model = finalModel;
        }
    }
}
