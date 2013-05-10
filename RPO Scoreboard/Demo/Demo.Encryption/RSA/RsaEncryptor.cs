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
        private RSAParameters rsaParams;

        public RsaEncryptor()
        {
        }

        public byte[] Encrypt(string data)
        {
            UnicodeEncoding encoder = new UnicodeEncoding();
            return Encrypt(encoder.GetBytes(data), new RSAParameters());
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
                return null;
            }
        }
    }
}
