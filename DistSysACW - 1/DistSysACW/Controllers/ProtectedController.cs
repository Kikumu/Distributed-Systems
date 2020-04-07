using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading;
using System.Security.Cryptography;
using System.Text;

namespace DistSysACW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProtectedController : BaseController
    {
        public ProtectedController(Models.UserContext context) : base(context)
        {

        }
        
        // GET: api/Protected/5
        [HttpGet]
        [ActionName("hello")]
        [Authorize(Roles = "Admin,user")]
        public ActionResult Get()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            var role_ = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
            string message = name;
            string msg = "";
            if (message == null || message == "")
            {
                this.Response.StatusCode = 400;
                msg = "BAD REQUEST";
            }
            else
            {
                this.Response.StatusCode = 200;
                msg = "Hello " + name;
                update_log(name, "User Requested Protected Hello");
            }
            return new ObjectResult(msg);
        }
        [HttpGet]
        [ActionName("sha1")]
        [Authorize(Roles = "Admin,user")]
        public ActionResult security1([FromQuery]string message)
        {
            string msg = "";
            if (message == null || message == "")
            {
                this.Response.StatusCode = 400;
                msg = "BAD REQUEST";
            }
            else
            {

                msg = encrypt_SHA1(message);
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                update_log(name, "User Requested Protected SHA1");
            }
            return new ObjectResult(msg);
        }
        [HttpGet]
        [ActionName("sha256")]
        [Authorize(Roles = "Admin,user")]
        public ActionResult security2([FromQuery]string message)
        {
            string msg = "";
            if(message == null || message == "")
            {
                this.Response.StatusCode = 400;
                msg = "BAD REQUEST";
            }
            else
            {
                msg = encrypt_SHA256(message);
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                update_log(name, "User Requested Protected SHA256");
            }
            return new ObjectResult(msg);
        }
        [HttpGet]
        [ActionName("GetPublicey")]
        [Authorize(Roles = "Admin,user")]
        public ActionResult GetPublicKey()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            update_log(name, "User Requested Public Key");
            RSACryptoServiceProvider rSACrypto = Singleton.SingletonPattern.Instance;  //calls rsa instance
            string publicKeyXml = Singleton.SingletonPattern.Instance_2;              //calls public key instance
            return new ObjectResult(publicKeyXml);

        }
        [HttpGet]
        [ActionName("sign")]
        [Authorize(Roles = "Admin,user")]
        public ActionResult Signing([FromQuery]string message)
        {
            DecryptorClass.Decrptor decrptor = new DecryptorClass.Decrptor();
            byte[]data = decrptor.string_to_ascii(message);
            string pKey = Singleton.SingletonPattern.Instance_3;             //calls private key instance
            byte[] signed_data = decrptor.HashAndSignBytes(data, pKey);      //sha1
            string hex_return = decrptor.ByteArrayToHexString(signed_data);


            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            update_log(name, "User Signed a message using private key");
            return new ObjectResult(hex_return);
        }
        [HttpGet]
        [ActionName("AddFifty")]
        [Authorize(Roles ="Admin")]
        public ActionResult AddEncryptedIntegers([FromQuery]string [] encrpted_message)
        {
            DecryptorClass.Decrptor decrptor = new DecryptorClass.Decrptor(); //call decryptor class
            AES_Functions.AES_E_D AesFunctions = new AES_Functions.AES_E_D();
            //FORMAT = DATA + KEY + IV
            string [] encrypted_hex = encrpted_message;
            byte[] data = decrptor.hex2byte(encrypted_hex[0]);
            byte[] key  = decrptor.hex2byte(encrypted_hex[1]);
            byte[] iv   = decrptor.hex2byte(encrypted_hex[2]);

            string pKey = Singleton.SingletonPattern.Instance_3; //call private key instance
            data =decrptor.RSADecrypt(data, pKey);
            key  =decrptor.RSADecrypt(key, pKey);
            iv   =decrptor.RSADecrypt(iv, pKey);

            //----------------from decrypted data, convert back to int and add 50 then convert back to string
            string Original_string = Encoding.UTF8.GetString(data, 0, data.Length);
            int Original_int = Convert.ToInt32(Original_string);
            Original_int += 50;
            Original_string = Convert.ToString(Original_int);
            //--------------------ENCRYPT AND SEND-----------------------------------------------------------//
            data = AesFunctions.EncryptStringToBytes_Aes(Original_string, key, iv);
            string hex_data = decrptor.ByteArrayToHexString(data);
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string name = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            update_log(name, "User Encrypted an integer message ");
            return new ObjectResult(hex_data);
        }
    }
}
