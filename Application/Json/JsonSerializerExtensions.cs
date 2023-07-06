using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestApp.Common;
namespace TestApp.Application.Json
{
    public static class JsonSerializerExtensions
    {
        private static readonly JsonSerializerOptions _defaultOptions;
        public static IEnumerable<JsonConverter> DefaultConverters { get; }
        static JsonSerializerExtensions()
        {
            DefaultConverters = new JsonConverter[]
            {
                new JsonStringEnumConverter(),
                new DateOnlyJsonConverter(),
                new NullableDateOnlyJosnConverter(),
                new TimeOnlyJsonConverter(),
                new NullableTimeOnlyJsonConverter()

            };

            _defaultOptions = new JsonSerializerOptions();
            DefaultConverters.Foreach(_defaultOptions.Converters.Add) ;
            _defaultOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

        }
            
    }
}
