using pjsip4net.Core.Configuration;
using pjsip4net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOIPService
{
    public class SIPServiceConfigurator
    {
        private Configure cfg;

        public SIPServiceConfigurator()
        {
            cfg = Configure.Pjsip4Net();
            //Configure.Pjsip4Net().With(x => x.Config.AutoAnswer = false).With(x => x.Config.OutboundProxies.Add("sip:192.168.1.1:5060")).Build().Start();
        }


        public SIPService Build()
        {
            return new SIPService(cfg);
        }


        public SIPServiceConfigurator UseConfig()
        {
            cfg = cfg.FromConfig();
            return this;
        }

        public SIPServiceConfigurator With()
        {
            cfg = cfg.With(o =>
            {
                //o.Config.ThreadCount = 5;

                var transportType = new pjsip4net.Core.Data.TransportConfig();
                transportType.BoundAddress = "0.0.0.0";
                transportType.Port = 5080;
                transportType.PublicAddress = "0.0.0.0";

            var udp = new pjsip4net.Core.Utils.Tuple<pjsip4net.Core.TransportType, pjsip4net.Core.Data.TransportConfig>
                    (pjsip4net.Core.TransportType.Udp, transportType);
                o.RegisterTransport(udp);

                //var tcp = new pjsip4net.Core.Utils.Tuple<pjsip4net.Core.TransportType, pjsip4net.Core.Data.TransportConfig>
                //    (pjsip4net.Core.TransportType.Tcp, transportType);
                //o.RegisterTransport(tcp);


                o.Config.OutboundProxies.Add(@"sip:192.168.2.50:5060");

            });




            return this;
        }



        public SIPServiceConfigurator XXXX()
        {
            return this;
        }

    }
}
