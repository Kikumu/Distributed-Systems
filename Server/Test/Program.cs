using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace System.Runtime.Remoting
{

}

namespace System.Runtime.Remoting.Channels
{

}

namespace System.Runtime.Remoting.Channels.Tcp
{

}

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel channel = new TcpChannel(43);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(translator), "Translate", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Press any key to exit...");
            System.Console.ReadLine();
        }
    }
}
