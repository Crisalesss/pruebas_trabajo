using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using Catalog;
using CsvHelper;
using CsvHelper.Configuration;
using Speckle.Newtonsoft.Json;

/// <summary>
/// Este programa lee dos archivos CSV y genera un archivo JSON y otro XML.
/// </summary>
class Program
{
    /// <summary>
    /// Punto de entrada principal del programa.
    /// </summary>
    /// <param name="args">Argumentos de línea de comandos.</param>
    static void Main(string[] args)
    {
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
        };

        string filePathCategories = @"C:\Temporal\Categories.csv";
        string filePathProducts = @"C:\Temporal\Products.csv";
        string xmlFilePath = @"C:\Temporal\Catalog.xml";
        string jsonFilePath = @"C:\Temporal\Catalog.json";

        List<Categories> categories = ReadCsvFile<Categories>(filePathCategories, csvConfig, "ISO-8859-1");
        List<Products> products = ReadCsvFile<Products>(filePathProducts, csvConfig, "ISO-8859-1");

        foreach (Categories category in categories)
        {
            category.Products = products.FindAll(p => p.CategoryId == category.Id);
        }

        using (XmlWriter xmlWriter = XmlWriter.Create(xmlFilePath))
        {
            string xmlns = "http://schemas.datacontract.org/2004/07/Catalog";
            string xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string xsiPrefix = "i";

            xmlWriter.WriteStartElement("ArrayOfCategory", xmlns);
            xmlWriter.WriteAttributeString("xmlns", xsiPrefix, null, xsi);

            foreach (Categories category in categories)
            {
                xmlWriter.WriteStartElement("Category");
                xmlWriter.WriteElementString("Description", category.Description);
                xmlWriter.WriteElementString("Id", category.Id.ToString());
                xmlWriter.WriteElementString("Name", category.Name);
        
                xmlWriter.WriteStartElement("Products");
                foreach (Products product in category.Products)
                {
                    xmlWriter.WriteStartElement("Product");
                    xmlWriter.WriteElementString("CategoryId", product.CategoryId.ToString());
                    xmlWriter.WriteElementString("Id", product.Id.ToString());
                    xmlWriter.WriteElementString("Name", product.Name);
                    xmlWriter.WriteElementString("Price", product.Price.ToString());
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
        }

        using (StreamWriter writer = new StreamWriter(jsonFilePath, false))
        {
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.Formatting = Speckle.Newtonsoft.Json.Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, categories);
            }
        }

        Console.WriteLine("Archivos XML y JSON generados correctamente.");
    }

    static List<T> ReadCsvFile<T>(string filePath, CsvConfiguration csvConfig, string encodingName)
    {
        List<T> records = new List<T>();

        using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding(encodingName)))
        using (CsvReader csvReader = new CsvReader(reader, csvConfig))
        {
            records = csvReader.GetRecords<T>().ToList();
        }

        return records;
    }


}

