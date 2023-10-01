namespace Lz3_4_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("Do you want to use an existing database(y/n)?");
            char key = Console.ReadKey().KeyChar;
            bool dat = key == 'y' ? true : false;
            if (dat)
            {
                Console.WriteLine("\nEnter path to database:");
                Tools.path = Console.ReadLine().Replace("\"", "");
                Tools.ReadData();
            }
            else
            {
                Console.WriteLine("\nEnter path where you want to save the database:");
                Tools.path = Console.ReadLine().Replace("\"", "") + """\logindatabase.dat""";
            }
            while (true)
            {
                Console.WriteLine("\nDo you want to login (l) or sign up (s)? (press e to exit)");
                key = Console.ReadKey().KeyChar;
                string name;
                string password;

                switch (key)
                {
                    case 'l' or 'L':
                        Console.Write("\nEnter your name: ");
                        name = Console.ReadLine();
                        if (Tools.IsUser(name))
                        {
                            Console.Write("Enter password: ");
                            password = Console.ReadLine();
                            if (Tools.IsPassword(name, Tools.HashPassword(password)))
                            {
                                Console.WriteLine("Welcome! Login succesful!\n\n\n");
                            }
                            else Console.WriteLine("Wrong password!");
                        }
                        else Console.WriteLine("No such user!");
                        break;
                    case 's' or 'S':
                        Console.Write("\nEnter your name: ");
                        name = Console.ReadLine();
                        if (!Tools.IsUser(name))
                        {
                            Console.Write("Create a password: ");
                            password = Console.ReadLine();
                            Tools.AddUser(name, Tools.HashPassword(password));
                            Console.WriteLine("Sign up succesful!\n\n\n");
                        }
                        else Console.WriteLine("User already exists!");
                        break;
                    case 'e' or 'E':

                        break;
                    default:
                        Console.WriteLine("\nError! Unrecognized command!");
                        break;
                }
                if (key == 'e') 
                {
                    Tools.SaveAll();
                    break; 
                }
            }
        }
    }
}