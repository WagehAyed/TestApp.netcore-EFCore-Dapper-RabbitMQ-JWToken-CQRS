using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Configuration
{
    public class DatabaseConfigurationOptions
    {
        public string ConnectionString { get; set; }
        public List<string> Scopes { get; set; } = new List<string>();
    }
}
