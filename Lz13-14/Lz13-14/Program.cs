using Lz13_14;
using System.Security;

//User Registration
Protector.Register("Alice", "Pa$$word", new[] { "Admins" });
Protector.Register("Bob", "Passw0rd", new[] { "Sales", "TeamLeads" });
Protector.Register("Hehe", "1234567890");
Protector.Register("Makima", "<O> <O>", new[] { "Admins", "Sales", "TeamLeads" });

//User Authentication
Authentication();

var p = Thread.CurrentPrincipal;

//checking if login is successful
if (p == null)
{
    Console.WriteLine("Login failed!");
    return;
}

//Showing info about current user
Console.WriteLine($"\n----User authentication Info----");
Console.WriteLine($"Name: {p.Identity.Name}");
Console.Write($"Roles:");
Console.Write((p.IsInRole("Admins") ? "\n\tAdmins" : ""));
Console.Write((p.IsInRole("Sales") ? "\n\tSales" : ""));
Console.Write((p.IsInRole("TeamLeads") ? "\n\tTeamLeads" : ""));

Console.WriteLine();
//Showing user commands
while (true)
{
    Console.WriteLine("\nChoose a command:" +
        (p.IsInRole("Admins") ? "\na) AdminOnlyCommand" : "") +
        (p.IsInRole("Sales") ? "\ns) SalesOnlyCommand" : "") +
        (p.IsInRole("TeamLeads") ? "\nt) TeamLeadOnlyCommand" : "") +
        "\ne) Available for all command" +
        "\nx) Exit");

    char c = Console.ReadKey().KeyChar;
    Console.WriteLine();
    switch (c)
    {
        case 'a':
            try { AdminsOnlyCommand(); }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 's':
            try { SalesOnlyCommand(); }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 'e':
            Console.WriteLine("This is message is available for all");
            break;
        case 't':
            try { TeamLeadsOnlyCommand(); }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 'x':
            return;
        default:
            Console.WriteLine("Unrecognized command!!!");
            break;
    }
}
void AdminsOnlyCommand()
{
    if (Thread.CurrentPrincipal == null)
    {
        throw new Exception("You are not authenticated!!!");
    }

    if (!Thread.CurrentPrincipal.IsInRole("Admins"))
    {
        throw new Exception("You are not Admin!!!");
    }

    Console.WriteLine("--You are an Admin--");
    return;
}
void SalesOnlyCommand()
{
    if (Thread.CurrentPrincipal == null)
    {
        throw new Exception("You are not authenticated!!!");
    }

    if (!Thread.CurrentPrincipal.IsInRole("Sales"))
    {
        throw new Exception("You are not in Sales!!!");
    }

    Console.WriteLine("--You are in Sales--");
    return;
}
void TeamLeadsOnlyCommand()
{
    if (Thread.CurrentPrincipal == null)
    {
        throw new Exception("You are not authenticated!!!");
    }

    if (!Thread.CurrentPrincipal.IsInRole("TeamLeads"))
    {
        throw new Exception("You are not in TeamLeads!!!");
    }

    Console.WriteLine("--You are a TeamLead--");
    return;
}
void Authentication()
{
    Console.Write("Enter your name: ");
    var name = Console.ReadLine();
    Console.Write("Enter password: ");
    var password = Console.ReadLine();

    Protector.LogIn(name, password);
}
