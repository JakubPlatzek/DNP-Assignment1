using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

namespace Assignment1.Persistence
{
    public class FileContext : IFileContext
    {
        public IList<Family> Families { get; private set; }
        public IList<Adult> Adults { get; private set; }
        
        public IList<User> Users { get; private set; }

        private readonly string familiesFile = "C:/Users/jakub/RiderProjects/Assignment1/Assignment1/Database/families.json";
        private readonly string adultsFile = "C:/Users/jakub/RiderProjects/Assignment1/Assignment1/Database/adults.json";
        private readonly string usersFile = "C:/Users/jakub/RiderProjects/Assignment1/Assignment1/Database/users.json";

        public FileContext()
        {
            Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
            Users = File.Exists(usersFile) ? ReadData<User>(usersFile) : new List<User>();
        }

        public IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        public void SaveChanges()
        {
            // storing families
            string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(familiesFile, false))
            {
                outputFile.Write(jsonFamilies);
            }

            // storing persons
            string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(adultsFile, false))
            {
                outputFile.Write(jsonAdults);
            }
            
            // storing users
            string jsonUsers = JsonSerializer.Serialize(Users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(usersFile, false))
            {
                outputFile.Write(jsonUsers);
            }
        }
    }
}