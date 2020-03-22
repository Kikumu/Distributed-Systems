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
    public class MySingleton
    {

        private static MySingleton instance = null;
        private MySingleton() { }

        public static MySingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySingleton();
                }
                return instance;
            }
        }
        public RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
    }
}
