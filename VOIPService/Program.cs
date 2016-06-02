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
                //x.UseNLog();

                x.Service<SIPService>(s =>
                {
                    //s.ConstructUsing(() => new SIPService(null));

                    s.ConstructUsing(sc =>
                    {
                        sc.XXXX();
                    });


                    s.WhenStarted(service => service.Start());

                    s.WhenStopped(service => service.Stop());
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
