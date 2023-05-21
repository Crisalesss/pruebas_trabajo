using Newtonsoft.Json;
using WebTreeView.Models;

namespace WebTreeView.Utilidades
{
    public static class LeerJson
    {
        public static List<Item> Leer(string jsonFilePath)
        {
            string json = File.ReadAllText(jsonFilePath);
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);

            return items;

        }
        
    
    }
}

