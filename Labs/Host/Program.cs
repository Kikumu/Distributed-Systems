using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Lab4;
namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost myHost = new ServiceHost(typeof(TranslationService));
            myHost.Open();
            Console.WriteLine("op3n");
            myHost.Close();
        }
    }
}
