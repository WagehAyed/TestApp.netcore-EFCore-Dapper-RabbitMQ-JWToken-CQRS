using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common
{
    public class ObjectUtils
    {
        public static bool IsPropertyChanged<TCurrent, TOriginal>(TCurrent current, TOriginal original, Expression<Func<TCurrent, object>> expression)
        {
            var currentProps=typeof(TCurrent).GetProperties();
            var orginalProps=typeof(TOriginal).GetProperties();
            var comparingExpression = expression.Compile().Invoke(current);
            var comparingType= comparingExpression.GetType();   
            var comaringProperties=comparingType.GetProperties()?.Select(p => p.Name).ToArray();
            for (int i = 0; i < comaringProperties?.Length; i++)
            {
                var currentProp = currentProps?.FirstOrDefault(c => c.Name == comaringProperties[i]);
                var orginalProp = orginalProps?.FirstOrDefault(c => c.Name == comaringProperties[i]);
                if (currentProp?.GetValue(current)?.ToString() != orginalProp?.GetValue(original).ToString())
                    return true;
            }
            return false;
        }
    }
}
