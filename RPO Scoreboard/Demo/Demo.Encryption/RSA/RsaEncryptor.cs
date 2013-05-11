using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Demo.Encryption.RSA
{
    public class RsaEncryptor : IEncryptor
    {
        private RSACryptoServiceProvider rsa;

        public RsaEncryptor(byte[] keyBytes)
        {
            rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(keyBytes);
        }

        public byte[] Encrypt(string data)
        {
            UnicodeEncoding encoder = new UnicodeEncoding();
            return rsa.Encrypt(encoder.GetBytes(data), false);
            //return Encrypt(encoder.GetBytes(data), rsaParams);
        }

        public byte[] Encrypt(byte[] data, RSAParameters RSAKeyInfo, bool DoOAEPPadding = false)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.ImportParameters(RSAKeyInfo);
                return RSA.Encrypt(data, DoOAEPPadding);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
