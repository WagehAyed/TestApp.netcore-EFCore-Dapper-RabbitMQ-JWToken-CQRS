using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestApp.Api.ModelBinding
{
    public class MultipleSourcesBindingSource : BindingSource
    {
        public static readonly BindingSource MultipleSources = new MultipleSourcesBindingSource(
            "MultipleSources",
            "MultipleSources",
            true,
            true);
        public MultipleSourcesBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest) : base(id, displayName, isGreedy, isFromRequest)
        {
        }
        public override bool CanAcceptDataFrom(BindingSource bindingSource)
        {
            return bindingSource == Body || bindingSource == this;
        }
    }
}
