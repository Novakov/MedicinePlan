using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MedicinePlan;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Frontend
{
    public class Repository
    {
        private static readonly JsonSerializerSettings SerializerSettings;

        static Repository()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented,
                Converters =
                {
                    new MedicineDictionaryConverter(),
                    new StockConverter()
                },
                ContractResolver = new DefaultContractResolver
                {
                    DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                },
                //ObjectCreationHandling = ObjectCreationHandling.Replace
            };
        }

        public string DumpJson(Supplies supplies)
        {
            return JsonConvert.SerializeObject(supplies, SerializerSettings);
        }

        public Supplies ReadJson(string json)
        {          
            return JsonConvert.DeserializeObject<Supplies>(json, SerializerSettings);
        }
    }

    public class StockConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var s = (Stock) value;  

            new JArray(s.AsOfDate, s.Count).WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var arr = (JArray) JToken.ReadFrom(reader);

            return new Stock(arr.Value<int>(1), arr.Value<DateTime>(0));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Stock);
        }
    }

    public class MedicineDictionaryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dict = (IDictionary)value;

            writer.WriteStartObject();

            foreach (var key in dict.Keys.OfType<Medicine>())
            {
                writer.WritePropertyName(key.Name);
                serializer.Serialize(writer, dict[key]);
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dict = (IDictionary) existingValue;// Activator.CreateInstance(objectType);

            var valueType = objectType.GetGenericArguments()[1];

            var jsonDict = (JObject)JToken.ReadFrom(reader);

            foreach (var entry in jsonDict.Properties())
            {
                var jsonReader = entry.Value.CreateReader();

                var z = JToken.ReadFrom(jsonReader);

                dict[new Medicine(entry.Name)] = serializer.Deserialize(entry.Value.CreateReader(), valueType);
            }

            return dict;
        }        

        public override bool CanConvert(Type objectType)
        {
            var canConvert = objectType.IsGenericType
                             && (objectType.GetGenericTypeDefinition() == typeof(Dictionary<,>) || objectType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                             && objectType.GetGenericArguments()[0] == typeof(Medicine);
            return canConvert;
        }
    }
}
