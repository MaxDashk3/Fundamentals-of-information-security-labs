namespace Lz5_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char c;
            string name;
            while (true)
            {
                Console.Write("Do you want to log in (l) or sign up (s) (e to exit)?: ");
                c = Console.ReadKey().KeyChar;
                Console.WriteLine("\n\n");
                switch (c)
                {
                    case 'l' or 's':
                        Console.Write("Enter your name: ");
                        name = Console.ReadLine();
                        PBKDF2.Authenticate(name, c);
                        break;
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Error! Unrecognized command");
                        break;
                }
                if (c == 'e') break;
            }
        }
    }
}