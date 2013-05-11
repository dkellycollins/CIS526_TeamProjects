using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.Encryption.RSA;
using Demo.Models;

namespace TestApplication
{
    class Program
    {
        const string SITE = "localhost:1054/Task/CompleteTaskExternal/";
        
        static void Main(string[] args)
        {
            string buffer;
            while(true)
            {
                buffer = Console.In.ReadLine();
                if(buffer == "q")
                    Application.Exit();
                else
                    sendPacket(buffer);
            }
        }

        /*static void Main(string[] args)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes("Data to Encrypt");
                byte[] encryptedData;
                string decryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                RsaEncryptor encryptor = new RsaEncryptor();
                RsaDecryptor decryptor = new RsaDecryptor();

                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OAEP padding.
                encryptedData = encryptor.Encrypt("Data to Encrypt");

                //Pass the data to DECRYPT, the private key information 
                //(using RSACryptoServiceProvider.ExportParameters(true),
                //and a boolean flag specifying no OAEP padding.
                decryptedData = decryptor.Decrypt(encryptedData);

                //Display the decrypted plaintext to the console. 
                Console.WriteLine("Decrypted plaintext: {0}", decryptedData);
            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("Encryption failed.");
            }

            Console.In.ReadLine();
        }*/

        static void sendPacket(string input)
        {
            TaskCompletePacket packet = new TaskCompletePacket()
            {
                UserID = input,
                TaskToken = TestAppSettings.Default.TaskToken,
                Source = "TestApp"
            };
            Stream keyStream = Assembly.GetEntryAssembly().GetManifestResourceStream("TestApplication.demo_rsa.pub");
            MemoryStream keyStream2 = new MemoryStream();
            keyStream.CopyTo(keyStream2);
            RsaEncryptor encryptor = new RsaEncryptor(keyStream2.ToArray());
            byte[] encryptedPacket = encryptor.Encrypt(packet.ToString());

            WebClient client = new WebClient();
            client.UploadData(TestAppSettings.Default.Site, encryptedPacket);
        }
    }
}
