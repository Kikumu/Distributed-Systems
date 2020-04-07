using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using CoreExtensions;

namespace DistSysACW.DecryptorClass
{
    public class Decrptor
    {
        //----------------------------CONVERTS STRING TO AN ARRAY OF BYTES------------------------------------------------------------------------------//
        public byte[] string_to_ascii(string Message)  //just takes string and converts to ascii
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] encodedBytes = ascii.GetBytes(Message);

            return encodedBytes;
        }
        //----------------------------------------------CONVERTS BYTE ARRAY TO HEX STRING--------------------------------------------------------------//
        public  string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba);
        }

        //------------------------------------------ENCRYPTOR-------------------------------------------------------------------------------------------------//
        public byte[] SHA256_Encrypt(byte[] message)
        {
            byte[] sha256ByteMessage;
            SHA256 sHA = new SHA256CryptoServiceProvider();
            sha256ByteMessage = sHA.ComputeHash(message);
            return sha256ByteMessage;
        }

        public byte[] SHA1_Encrypt(byte[] message)
        {
            byte[] sha1ByteMessage;
            SHA1 sHA = new SHA1CryptoServiceProvider();
            sha1ByteMessage = sHA.ComputeHash(message);
            return sha1ByteMessage;
        }

        //-------------------------------------------------HASH AND SIGN DATA--------------------------------------------------------//
        public  byte[] HashAndSignBytes(byte[] DataToSign, dynamic Key)
        {
            try
            {
                //Singleton.MySingleton mySingleton = Singleton.MySingleton.Instance;
                RSACryptoServiceProvider rSACrypto = Singleton.SingletonPattern.Instance;       //calls rsa instance
                //CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(mySingleton.provider,Key);
                CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(rSACrypto, Key);
                return rSACrypto.SignData(DataToSign, new SHA1Managed());
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
        //-----------------------------------hex2byte conversion---------------------------------------------------
        public byte[] hex2byte(string hex)
        {
            hex = hex.Replace("-", "");
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        //--------------------------------------decrypt client data---------------------------------------------------------
        public byte[] RSADecrypt(byte[] DataToDecrypt, string Key)
        {
            try
            {
                byte[] decryptedData;
                RSACryptoServiceProvider rSA = new RSACryptoServiceProvider();
                CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(rSA, Key);
                decryptedData = rSA.Decrypt(DataToDecrypt, false);

                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }



    }
}

