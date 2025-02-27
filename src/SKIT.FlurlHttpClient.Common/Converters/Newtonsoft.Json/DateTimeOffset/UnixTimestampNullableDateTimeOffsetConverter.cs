﻿using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Newtonsoft.Json.Converters
{
    public class UnixTimestampNullableDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
    {
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override DateTimeOffset? ReadJson(JsonReader reader, Type objectType, DateTimeOffset? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return existingValue;
            } 
            else if (reader.TokenType == JsonToken.Integer)
            {
                long value = serializer.Deserialize<long>(reader);
                return DateTimeOffset.FromUnixTimeSeconds(value);
            }

            throw new JsonReaderException();
        }

        public override void WriteJson(JsonWriter writer, DateTimeOffset? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToUnixTimeSeconds());
            else
                writer.WriteNull();
        }
    }
}
