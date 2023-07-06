using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.DI
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class DependencyScannerIgnoreAttribute:Attribute
    {
    }
}
