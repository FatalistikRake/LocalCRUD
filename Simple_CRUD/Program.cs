
namespace Simple_CRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inserire l'operazione che si vuole utilizzare" +
                "1: Inserire dati" +
                "2: Leggere dati" +
                "3: Aggiornare dati" +
                "4: Cancellare dati");
                
            string r = Console.ReadLine();

            switch (r)
            {
                case "1":
                    InsertData();
                    break;
                case "2":
                    ReadData();
                    break;
                case "3":
                    UpdateData();
                    break;
                case "4":
                    DeleteData();
                    break;
                default:
                    Console.WriteLine("Operazione non valida");
                    break;
            }
        }

        private static void InsertData()
        {
            
        }
    }
}
