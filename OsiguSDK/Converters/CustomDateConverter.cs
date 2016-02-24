using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OsiguSDK.Converters
{
    class CustomDateConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                var ticks = (long)reader.Value;
                var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddMilliseconds(ticks);
                return date;

            }
            if (reader.TokenType == JsonToken.Date)
            {
                return (DateTime)reader.Value;
            }
            throw new Exception(String.Format("Unexpected token parsing date.",reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime)
            {
                var epoc = new DateTime(1970, 1, 1);
                var delta = ((DateTime)value) - epoc;
                if (delta.TotalMilliseconds < 0)
                {
                    throw new ArgumentOutOfRangeException("Unix epoc starts January 1st, 1970");
                }
                ticks = (long)delta.TotalMilliseconds;
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(ticks);
        }
    }
}
