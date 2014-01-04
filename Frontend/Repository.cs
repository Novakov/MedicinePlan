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
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
                Converters =
                {
                    new MedicineConverter()
                },
                ContractResolver = new DefaultContractResolver
                {
                    DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                }
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

    public class MedicineConverter : JsonConverter
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
            var dict = (IDictionary)existingValue ?? (IDictionary)Activator.CreateInstance(objectType);

            var jsonDict = (JObject)JToken.ReadFrom(reader);

            foreach (var entry in jsonDict.Properties())
            {
                dict[new Medicine(entry.Name)] = serializer.Deserialize(entry.Value.CreateReader());
            }

            return dict;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType
                && objectType.GetGenericTypeDefinition() == typeof(Dictionary<,>)
                && objectType.GetGenericArguments()[0] == typeof(Medicine);
        }
    }
}
