using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestApp.Application.Json
{
    internal class NullableTimeOnlyJsonConverter : JsonConverter<TimeOnly?>
    {
        public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
             var value=reader.GetString();
            return string.IsNullOrEmpty(value) ? null :   TimeOnly.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
        {
            if (value == null)

                writer.WriteNullValue();
            else
                writer.WriteStringValue(value.Value.ToString("HH:mm"));
        }
    }
}
