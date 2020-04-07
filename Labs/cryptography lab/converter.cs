using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CoreExtensions;
using DistSysACW.Singleton;

namespace cryptography_lab
{
    class converter
    {
        public byte[] string_to_ascii(string Message)  //just takes string and converts to ascii
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] encodedBytes = ascii.GetBytes(Message);
            return encodedBytes;
        }

        public string ByteArrayToHexString(byte[] byteArray) { 
            string hexString = "";
            if (null != byteArray)
            { 
                foreach (byte b in byteArray) 
                {
                    
                    hexString += (b + 2).ToString("x2"); //the +6 is the salt 
                    hexString += "-";
                } 
            } 
            return hexString; 
        }
        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba);
        }

        public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo)
        {
            try
            {
                byte[] encryptedData; using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    RSA.ImportParameters(RSAKeyInfo); encryptedData = RSA.Encrypt(DataToEncrypt, false);
                }
                return encryptedData;
            }
            catch (CryptographicException e) { Console.WriteLine(e.Message); return null; }
        }

         public byte[] RSADecrypt(byte[] DataToDecrypt, string Key)
        {
            try 
            { 
                byte[] decryptedData;
               RSACryptoServiceProvider rSA = new RSACryptoServiceProvider();
               CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(rSA, Key);
               decryptedData = rSA.Decrypt(DataToDecrypt,false);
                
               return decryptedData;
            } 
            catch (CryptographicException e) 
            { 
                Console.WriteLine(e.ToString()); 
                return null; 
            } 
        }

        public byte[] HashAndSignBytes(byte[] DataToSign, dynamic Key)
        {
            try
            {
                RSACryptoServiceProvider rSACrypto = new RSACryptoServiceProvider();     //calls rsa instance
                CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(rSACrypto, Key);
                return rSACrypto.Encrypt(DataToSign, false);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace("-", "");
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

    }
}
