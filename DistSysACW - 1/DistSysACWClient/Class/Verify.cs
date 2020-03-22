using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DistSysACWClient.Class
{
    public class Verify
    {
        public  bool VerifySignedHash(byte[] OriginalData, byte[] SignedData, dynamic Key)
        {
            try
            {
                bool f = true;
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(RSAalg, Key);
                f = RSAalg.VerifyData(OriginalData, new SHA1Managed(), SignedData);
                return f;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
        public byte[] string_to_ascii(string Message)  //just takes string and converts to ascii
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            Byte[] encodedBytes = ascii.GetBytes(Message);

            return encodedBytes;
        }
    }
}
