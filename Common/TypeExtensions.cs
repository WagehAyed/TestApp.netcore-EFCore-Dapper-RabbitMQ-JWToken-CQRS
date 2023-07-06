using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common
{
    public static class TypeExtensions
    {
        public static bool IsRecordType(this Type type) {
            // This is the only way at the time of writing this method that can detect if type is record or not.

            return type.GetMethod("<Clone>$") != null;
       }
    }
}
