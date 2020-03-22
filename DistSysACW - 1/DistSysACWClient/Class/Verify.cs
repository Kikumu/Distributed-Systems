using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DistSysACWClient.Class
{
    public class Verify
    {
        public  bool VerifySignedHash(byte[] OriginalData, byte[] SignedData, string Key)
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
            byte[] encodedBytes = ascii.GetBytes(Message);
            return encodedBytes;
        }

        public  byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
