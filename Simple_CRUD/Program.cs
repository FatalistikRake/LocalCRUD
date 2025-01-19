
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Simple_CRUD
{
    internal class Program
    {
        public static Dictionary<string, string> database = new()
        {
            {"Casa", "Aprire la porta di casa con la chiave"},
            {"Macchina", "Aprire la macchina con il telecomando"},
        };

        static void Main()
        {

            // Type of Data that is going to be stored
            // Title with the Content

            while (true)
            {
                Console.WriteLine("Inserire l'operazione che si vuole utilizzare" +
                    "\n1: Inserire dati" +
                    "\n2: Leggere dati" +
                    "\n3: Aggiornare dati" +
                    "\n4: Cancellare dati" +
                    "\n");

                string r = Console.ReadLine();
                switch (r)
                {
                    case "1":
                        Console.Clear();
                        InsertData();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        ReadData();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        UpdateData();
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        DeleteData();
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Not valid operation");
                        break;
                }
            }
        }

        private static void InsertData()
        {
            Console.WriteLine("Inserire il titolo");
            string? keyTitle = Console.ReadLine();

            Console.WriteLine("Inserire il contenuto");
            string? valueContent = Console.ReadLine();

            if (string.IsNullOrEmpty(keyTitle) || string.IsNullOrEmpty(valueContent))
            {
                Console.Clear();
                Console.WriteLine("You want to redo? 1: yes || 2: no");
                string? r = Console.ReadLine();
                if (r == "2")
                {
                    return;
                }
                Console.Clear();
                InsertData();
                return;
            }

            database.Add(keyTitle, valueContent);
        }

        private static void ReadData()
        {
            foreach (var key in database)
            {
                Console.WriteLine($"\t{key.Key}\n{key.Value}\n\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void UpdateData()
        {
            Console.WriteLine("Insert the title to find");
            string keyTitle = Console.ReadLine()!;

            if (!database.TryGetValue(keyTitle, out string? oldValue))
            {
                Console.WriteLine("Title not found");
                Console.WriteLine("You want to redo? 1: yes || 2: no");
                string? r = Console.ReadLine();
                if (r == "2")
                {
                    return;
                }
                Console.Clear();
                UpdateData();
                return;
            }

            Console.WriteLine("Insert the title to update");
            database.Remove(keyTitle);
            string? newKeyTitle = Console.ReadLine();
            if (string.IsNullOrEmpty(newKeyTitle))
            {
                Console.WriteLine("Title not found");
                Console.WriteLine("You want to redo? 1: yes || 2: no");
                string? r = Console.ReadLine();
                if (r == "2")
                {
                    return;
                }
                Console.Clear();
                UpdateData();
                return;
            }
            database.Add(newKeyTitle, oldValue);

            Console.WriteLine("Insert the content to update");
            database[keyTitle] = Console.ReadLine()!;
        }

        private static void DeleteData()
        {
            Console.WriteLine("Insert the title to find");
            string? keyTitle = Console.ReadLine();

            if (string.IsNullOrEmpty(keyTitle))
            {
                Console.Clear();
                Console.WriteLine("You want to redo? 1: yes || 2: no");
                string? r = Console.ReadLine();
                if (r == "2")
                {
                    return;
                }
                Console.Clear();
                DeleteData();
                return;
            }

            database.Remove(keyTitle);
        }
    }
}
