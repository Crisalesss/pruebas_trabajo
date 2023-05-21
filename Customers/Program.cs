using CsvHelper;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Text;
using CsvHelper.Configuration;

class Program
{
    static void Main()
    {
        string csvFilePath = @"C:\Temporal\Customers.csv";
        string connectionString = "Server=DESKTOP-Q1U1ASB;Database=Northwind;user id=CRISALESS;password=Cris18208*;TrustServerCertificate=true;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var sqlBulkCopy = new SqlBulkCopy(connection))
            {
                sqlBulkCopy.DestinationTableName = "CustomersCsv";

                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = true,
                };

                using (StreamReader reader = new StreamReader(csvFilePath, Encoding.GetEncoding("ISO-8859-1")))
                using (CsvReader csvReader = new CsvReader(reader, csvConfig))
                using (CsvDataReader csvDataReader = new CsvDataReader(csvReader))
                {
                    sqlBulkCopy.WriteToServer(csvDataReader);
                }
            }
        }

        Console.WriteLine("Datos cargados en la tabla correctamente.");
    }
}

//using System.ComponentModel.DataAnnotations;
//using CsvHelper;
//using System.Text;
//using Microsoft.EntityFrameworkCore;
//using CsvHelper.Configuration;
//using System.Globalization;

//// Definición de la entidad CustomersCsv
//public class CustomersCsv
//{
//    [Key]
//    public string Id { get; set; }
//    public string Name { get; set; }
//    public string Address { get; set; }
//    public string City { get; set; }
//    public string Country { get; set; }
//    public string PostalCode { get; set; }
//    public string Phone { get; set; }
//}

//public class ApplicationDbContext : DbContext
//{
//    public DbSet<CustomersCsv> CustomersCsv { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        string connectionString = "Server=DESKTOP-Q1U1ASB;Database=Northwind;user id=CRISALESS;password=Cris18208*;TrustServerCertificate=true;";
//        optionsBuilder.UseSqlServer(connectionString);
//    }

//}

//class Program
//{
//    static void Main()
//    {
//        string csvFilePath = @"C:\Temporal\Customers.csv";

//        List<CustomersCsv> CustomersCsv = ReadCustomersCsvFromCSV(csvFilePath);

//        using (var dbContext = new ApplicationDbContext())
//        {
//            dbContext.CustomersCsv.AddRange(CustomersCsv);
//            dbContext.SaveChanges();
//        }

//        Console.WriteLine("Datos cargados en la tabla correctamente.");
//    }

//    static List<CustomersCsv> ReadCustomersCsvFromCSV(string csvFilePath)
//    {
//        List<CustomersCsv> CustomersCsv = new List<CustomersCsv>();

//        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
//        {
//            Delimiter = ";",
//            HasHeaderRecord = true,
//        };

//        using (StreamReader reader = new StreamReader(csvFilePath, Encoding.GetEncoding("ISO-8859-1")))
//        using (CsvReader csvReader = new CsvReader(reader, csvConfig))
//        {

//            csvReader.Read();
//            csvReader.ReadHeader();

//            while (csvReader.Read())
//            {
//                CustomersCsv Customers = new CustomersCsv
//                {
//                    Id = csvReader.GetField<string>("Id"),
//                    Name = csvReader.GetField<string>("Name"),
//                    Address = csvReader.GetField<string>("Address"),
//                    City = csvReader.GetField<string>("City"),
//                    Country = csvReader.GetField<string>("Country"),
//                    PostalCode = csvReader.GetField<string>("PostalCode"),
//                    Phone = csvReader.GetField<string>("Phone")
//                };

//                CustomersCsv.Add(Customers);
//            }
//        }

//        return CustomersCsv;
//    }
//}
