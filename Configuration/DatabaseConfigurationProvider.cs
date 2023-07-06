using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace TestApp.Configuration
{
    public class DatabaseConfigurationProvider:ConfigurationProvider
    {
        public readonly DatabaseConfigurationOptions _options;
        public DatabaseConfigurationProvider(DatabaseConfigurationOptions options)
        {
            _options=options;
        }


        public override void Load()
        {
            using var connection = new SqlConnection(_options.ConnectionString);
            connection.Open();
            Data = connection.Query<dynamic>("EXEC ReadConfiguration @scopes", new { Scopes = string.Join(',', _options.Scopes.Select(ParseScopePlaceholders)) }).ToDictionary(x => (string)x.Name, x => (string)x.Value);
            Data.Add("ConnectionString:Configuration", _options.ConnectionString);
            connection.Close();
            connection.Dispose();
        }

        private string ParseScopePlaceholders(string scopes)
        {
            return Regex.Replace(scopes, @"\{MachineName\}", Environment.MachineName, RegexOptions.IgnoreCase);
        }
    }
}
