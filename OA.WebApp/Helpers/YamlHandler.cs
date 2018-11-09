using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace OA.WebApp.Helpers
{
    public class YamlHandler
    {
        private static YamlStream _yamlInstance;
        private static string basePath = @"config\";
        private string _path;

        public YamlHandler(string yamlFile)
        {
            if (_yamlInstance == null)
            {
                _yamlInstance = new YamlStream();
            }
            this._path = Path.GetFullPath(basePath + yamlFile);  
        }

        public string ToJsonString()
        {
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(this.readeFile());

            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();
            return serializer.Serialize(yamlObject);
        }

        public JObject ToJson()
        {
            string jsonText = this.ToJsonString();

            return (JObject)JsonConvert.DeserializeObject(jsonText);
        }

        private TextReader readeFile()
        {
            return new StreamReader(this._path);
        }

    }
}
