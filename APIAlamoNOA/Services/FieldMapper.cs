using APIAlamoNOA.Services.Entities;
using Newtonsoft.Json;

namespace APIAlamoNOA.Services
{
    public class FieldMapper
    {
        public List<FieldMap> fieldMap { get; set; }
        private static string fileContent = string.Empty;

        public bool LoadMappingFile(string path)
        {
            try
            {
                fileContent = File.ReadAllText(path);
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                fieldMap = JsonConvert.DeserializeObject<List<FieldMap>>(fileContent, settings);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
