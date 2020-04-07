using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace DistSysACWClient.Class
{
    public class Verify
    {
        public  string VerifySignedHash(byte[] OriginalData, byte[] SignedData, string Key)
        {
            try
            {
                bool f = true;
                string message;
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(RSAalg, Key);
                f = RSAalg.VerifyData(OriginalData, new SHA1Managed(), SignedData);
                if (f == true)
                    message = "Message was successfully signed";
                else
                    message = "Message was not succesfully signed";
                return message;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                string message = "Client doesnt have the public key";
                return message;
            }
        }
        public byte[] string_to_ascii(string Message)  //just takes string and converts to ascii
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] encodedBytes = ascii.GetBytes(Message);
            return encodedBytes;
        }
         //--------------------hex2byte-----------------------------------
        public  byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace("-", "");
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        //-------------------------------------------------HASH AND SIGN DATA--------------------------------------------------------//
        public byte[] EncryptWithPublicKey(byte[] DataToSign, dynamic Key)
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
        //-----------------------------------------------------byte2hex--------------------------------------------------------------
        public string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba);
        }
        //---------------------------------------------------------AES--------------------------------------------------------------
        
    }
}
