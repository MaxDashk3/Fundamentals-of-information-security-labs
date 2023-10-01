namespace Lz3_4_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to the file: ");
            string pathto = Console.ReadLine().Replace("\"", "");
            Tools.savepath = pathto.Remove(pathto.LastIndexOf("""\"""));
            Tools.data = File.ReadAllBytes(pathto);


            Console.WriteLine("Do you want to verify a file (v) or hash a file with a verification key (h)?");
            char choice = Console.ReadKey().KeyChar;
            switch (choice)
            {
                case 'v' or 'V':
                    Console.WriteLine("\nDo you have the key as a file (f) or a string (s)?");
                    choice = Console.ReadKey().KeyChar;
                    Tools.GetKey(choice);
                    if (Tools.Verify())
                    {
                        Console.WriteLine("Verification complete! Hashes match!");
                    }
                    else { Console.WriteLine("Verification complete. Hashes don't match"); }
                    break;

                case 'h' or 'H':
                    Console.WriteLine("\nDo you have the key as a file (f), a string (s), or generate one (g)?");
                    choice = Console.ReadKey().KeyChar;
                    Tools.GetKey(choice);
                    Tools.ComputeHash();
                    break;

                default:
                    Console.WriteLine("Error! No such command");
                    break;
            }
        }
    }
}