using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Encryption.RSA
{
    public class RsaDecryptor : IDecryptor
    {
        private RSAParameters rsaParams;

        public RsaDecryptor()
        {
            rsaParams = RSAFacotry.GetRSAProvider().ExportParameters(true);
        }

        public string Decrypt(byte[] data)
        {
            UnicodeEncoding encoder = new UnicodeEncoding();
            return encoder.GetString(Decrypt(data, rsaParams));
        }

        public byte[] Decrypt(byte[] data, RSAParameters RSAKeyInfo, bool DoOAEPPadding = false)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This needs
                //to include the private key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Decrypt(data, DoOAEPPadding);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
