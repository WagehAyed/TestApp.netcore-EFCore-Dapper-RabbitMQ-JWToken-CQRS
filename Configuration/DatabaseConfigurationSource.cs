using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Configuration
{
    public class DatabaseConfigurationSource:IConfigurationSource
    {
        private readonly DatabaseConfigurationOptions _options;
        public DatabaseConfigurationSource(DatabaseConfigurationOptions  options)
        {
            _options= options;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DatabaseConfigurationProvider(_options);
        }
    }
}
