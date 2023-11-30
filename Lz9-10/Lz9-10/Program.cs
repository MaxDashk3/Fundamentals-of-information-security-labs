using Lz9_10;

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

char c = ' ';
while (c != 'e')
{
    Console.WriteLine("Do you want to create a signature (c) or validate an existing one (v)? (press e to exit, press s to save key)");
    c = Console.ReadKey().KeyChar;
    Console.WriteLine();

    switch (c)
    {
        case 'e':
            return;
        case 'v' or 'c':
            break;
        case 's':
            Console.Write("Enter path to save key: ");
            var savepath = Console.ReadLine().Replace("\"", "");
            Signature.SavePublic(savepath + "\\PublicKey.xml");
            Console.WriteLine();
            continue;
        default:
            Console.WriteLine("Error! Unrecognized Command!");
            continue;
    }


    Console.Write("Enter path to the document: ");
    var filepath = Console.ReadLine().Replace("\"", "");

    var dir = filepath.Remove(filepath.LastIndexOf("\\") + 1);
    var filebytes = File.ReadAllBytes(filepath);

    switch (c)
    {
        case 'c':
            var signature = Signature.Generate(filebytes);
            Console.WriteLine("Signature: " + Convert.ToBase64String(signature) + "\n");
            File.WriteAllBytes(dir + "Signature.dat", signature);
            break;
        case 'v':
            Console.Write("Enter path to the signature: ");
            signature = File.ReadAllBytes(Console.ReadLine().Replace("\"", ""));

            Console.Write("Enter path to the public xml key: ");
            var keypath = Console.ReadLine().Replace("\"", "");

            bool check = Signature.CheckSignature(keypath, filebytes, signature);

            Console.WriteLine(check ? "The signature is correct!" : "The signature is incorrect!");
            break;
    }
}

