using Newtonsoft.Json;
using SystemLosowaniaOsobyDoOdpowiedzi.Models;
using System.IO;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Services
{
    public class FileService
    {
        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lotteryClasses.json");

        public void SaveClasses(List<Class> classes)
        {
            var json = JsonConvert.SerializeObject(classes, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public List<Class> LoadClasses()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<Class>>(json);
            }
            return new List<Class>();
        }
    }
}