using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestFramework.Config
{
    public class ConfigReader
    {
        public static TestSettings ReadConfig()
        {
            var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/appsettings.json");

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerializerOptions);
        }
    }
}
