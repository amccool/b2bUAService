using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace VOIPService
{
    class Program
    {
        static void Main(string[] args)
        {
             HostFactory.Run(x =>
            {
                x.Service<SIPService>(s =>
                {
                    s.ConstructUsing(sc =>
                    {
                        sc.WithTcpSipTransport();
                        sc.WithUdpSipTransport();// use only tcp OR udp , not both, basically only the last one is used

                    });


                    s.WhenStarted(async service => await service.Start());

                    s.WhenStopped(async service => await service.Stop());
                    //s.WithNancyEndpoint(x, c =>
                    //{
                    //    c.AddHost(port: 8080);
                    //});
                });


                x.StartAutomatically();
                x.SetDescription("r5 voip service");
                x.SetServiceName("voipservice");
                x.RunAsNetworkService();
            });
        }
    }
}
