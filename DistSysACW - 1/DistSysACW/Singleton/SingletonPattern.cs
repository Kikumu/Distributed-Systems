using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using CoreExtensions;



namespace DistSysACW.Singleton
{
    public class SingletonPattern
    {
        
        private static RSACryptoServiceProvider rSA = new RSACryptoServiceProvider();
        private static string rsaKeyInfo = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rSA);
        private static string rsaPrivate = CoreExtensions.RSACryptoExtensions.ToXmlStringCore22(rSA, true);
        private SingletonPattern() { }

        public  static RSACryptoServiceProvider Instance
        {
            get
            {
                
                return rSA;
            }
        }

        public static string Instance_2
        {
            get
            {
                return rsaKeyInfo;
            }
        }

        public static string Instance_3
        {
            get
            {
                return rsaPrivate;
            }
        }

    }
}
