using DistSysACW.Singleton;
using System;
using System.Security.Cryptography;
using System.Text;

namespace cryptography_lab
{
    class Program
    {
      
        static void Main(string[] args)
        {
            converter converter = new converter();
            // Console.WriteLine("Hello World!");
            string message = "60";
            byte[] asciiByteMessage = converter.string_to_ascii(message);
            byte[] sha1ByteMessage;
            SHA256 sha1Provider = new SHA256CryptoServiceProvider();
            //
            sha1ByteMessage = sha1Provider.ComputeHash(asciiByteMessage);
            string hashed_string = converter.ByteArrayToString(sha1ByteMessage);
            Console.WriteLine(hashed_string);
            RSACryptoServiceProvider rSACryptoService = new RSACryptoServiceProvider();
            
            string rsaKeyInfo = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rSACryptoService, true); //private
            string rsaKeyInfo1 = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rSACryptoService); //public
            Console.WriteLine("Key: " + rsaKeyInfo);
            Console.WriteLine("Key1: " + rsaKeyInfo1);
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
            aesProvider.GenerateKey();
            aesProvider.GenerateIV();
            byte[] aes_key = aesProvider.Key;
            byte[] aes_initVector = aesProvider.IV;
            //----------------signed and hasshed using pub-lick ey---------------------------------//
            asciiByteMessage = converter.HashAndSignBytes(asciiByteMessage, rsaKeyInfo1); //encrypted byte
            aes_key = converter.HashAndSignBytes(aes_key, rsaKeyInfo1);                   //encrypted byte
            aes_initVector = converter.HashAndSignBytes(aes_initVector, rsaKeyInfo1);     //encrypted byte
            //-----------------------hex em--------------------------------------------------------//
            string hex_ascii = converter.ByteArrayToString(asciiByteMessage);
            string hex_aesKey= converter.ByteArrayToString(aes_key);
            string hex_vector= converter.ByteArrayToString(aes_initVector);
            //-----------------------decrypt em--------------------------------------------------//
            Console.WriteLine(hex_ascii);
            byte[] encryptedByteMessage;
            byte[] decryptedByteMessage;

            //------------------------------convert2bytes--------------------------------//
            byte[] hex_to_bytes_iv = converter.StringToByteArray(hex_vector);
            byte[] hex_to_bytes_aeskey = converter.StringToByteArray(hex_aesKey);
            byte[] hex_to_bytes_data = converter.StringToByteArray(hex_ascii);
            //---------------------------decrypt now-------------------------------------//
            //RSADecrypt
            //byte[] decrypt_data = rSA.Decrypt(hex_to_bytes_data, false);
            byte[] decrypt_data = converter.RSADecrypt(hex_to_bytes_data, rsaKeyInfo);
            byte[] decrypt_iv = converter.RSADecrypt(hex_to_bytes_iv, rsaKeyInfo);
            byte[] decrypt_aes = converter.RSADecrypt(hex_to_bytes_aeskey, rsaKeyInfo);

            string converted = Encoding.UTF8.GetString(decrypt_data, 0, decrypt_data.Length);

            Console.WriteLine(converted);
            Console.WriteLine(decrypt_iv);
            Console.WriteLine(decrypt_aes);















               // RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            
               // encryptedByteMessage = converter.RSAEncrypt(asciiByteMessage, RSA.ExportParameters(false));
               // Console.Write("Encrypted message: "); 
               // Console.WriteLine(converter.ByteArrayToString(encryptedByteMessage));
               //// decryptedByteMessage = converter.RSADecrypt(encryptedByteMessage, RSA.ExportParameters(true));
               // Console.Write("Decrypted message: ");
               // Console.WriteLine(System.Text.Encoding.ASCII.GetString(decryptedByteMessage));
            
        }
    }
}
