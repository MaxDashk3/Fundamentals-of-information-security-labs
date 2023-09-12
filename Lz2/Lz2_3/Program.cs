using System.Text;

namespace Lz2_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Mit21Bytes = Encoding.UTF8.GetBytes("Mit21");

            Console.WriteLine("Enter path:");
            string path;
            path = Console.ReadLine();

            //path = """C:\Users\Max\Downloads\encfile.dat""";

            var msg = File.ReadAllBytes(path);

            var key = new byte[5];

            for (int i = 0; i < msg.Length - key.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    key[j] = (byte)(msg[i + j] ^ Mit21Bytes[j]);

                }
                Console.WriteLine("\nKey: " + Encoding.UTF8.GetString(key) + "\n");
                Console.WriteLine(Encoding.UTF8.GetString(DecodeFunc(i,key,msg)));

            }

        }
        public static byte[] DecodeFunc(int index, byte[] key, byte[] msg)
        {
            var decryptedmsg = new byte[msg.Length];

            for (int a = index; a < msg.Length + index; a += key.Length)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (a + i > decryptedmsg.Length - 1) break;
                    decryptedmsg[a + i] = (byte)(msg[a + i] ^ key[i]);
                }
            }
            index -= 1;
            for (int a = index; a >= 0;)
            {
                for (int i = key.Length-1; i >= 0; i--)
                {
                    if (a < 0) break;
                    decryptedmsg[a] = (byte)(msg[a] ^ key[i]);
                    a--;
                }
            }

            return decryptedmsg;

        }
    }
}