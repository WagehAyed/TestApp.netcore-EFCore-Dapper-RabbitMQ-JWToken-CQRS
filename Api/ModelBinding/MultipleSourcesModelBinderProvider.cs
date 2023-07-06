using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TestApp.Application.Commands;
using TestApp.Common;

namespace TestApp.Api.ModelBinding
{
    public class MultipleSourcesModelBinderProvider : IModelBinderProvider
    {
        private BodyModelBinderProvider _bodyModelBinderProvider;
        private ComplexObjectModelBinderProvider _complexObjectModelBinderProvider;

        public MultipleSourcesModelBinderProvider(BodyModelBinderProvider bodyModelBinderProvider, ComplexObjectModelBinderProvider complexObjectModelBinderProvider)
        {
            _bodyModelBinderProvider = bodyModelBinderProvider;
            _complexObjectModelBinderProvider = complexObjectModelBinderProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var bodyBinder = _bodyModelBinderProvider.GetBinder(context);
            var complexBinder = _complexObjectModelBinderProvider.GetBinder(context);

            if (context.Metadata.ModelType.IsAssignableTo(typeof(ICommand)))
            {
                return new MultipleSourcesModelBinder(bodyBinder, complexBinder);
            }
            if (context.BindingInfo.BindingSource != null
                && context.BindingInfo.BindingSource.CanAcceptDataFrom(MultipleSourcesBindingSource.MultipleSources))
            {
                return new MultipleSourcesModelBinder(bodyBinder, complexBinder);
            }
            else
            {
                return null;
            }
        }
    }
}
