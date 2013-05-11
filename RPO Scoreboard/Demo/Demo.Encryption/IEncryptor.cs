using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Encryption
{
    public interface IEncryptor
    {
        byte[] Encrypt(string data);
    }
}
