using pjsip4net.Core.Configuration;
using pjsip4net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace VOIPService
{
    public class SIPServiceConfigurator
    {
        private Configure cfg;

        public SIPServiceConfigurator()
        {
            //cfg = Configure.Pjsip4Net();//.FromConfig();
            //Configure.Pjsip4Net().With(x => x.Config.AutoAnswer = false).With(x => x.Config.OutboundProxies.Add("sip:192.168.1.1:5060")).Build().Start();

            cfg = Configure.Pjsip4Net()
                .With(x => x.LoggingConfig.TraceAndDebug = true)
                .With(x => x.LoggingConfig.LogMessages = true)
                .With(x => x.LoggingConfig.LogLevel = 100)
                //.With(x=>x.MediaConfig.)
                .With(x => x.Config.AutoAnswer = false)
                .With(x => x.Config.AutoConference = false)
                .With(x => x.Config.AutoRecord = false)
                //.With(x => x.Config.Credentials = false)
                //.With(x => x.Config.DnsServers = false)
                //.With(x => x.Config.ForceLooseRoute = false)
                //.With(x => x.Config.HangupForkedCall = false)
                //.With(x => x.Config.SecureSignalling = false)
                //.With(x => x.Config.ThreadCount = 1)
                .With(x => x.Config.UseSrtp = pjsip4net.Core.Data.SrtpRequirement.Disabled);

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

        public SIPServiceConfigurator UseTcp()
        {
            cfg = cfg.With(o =>
            {
                Socket sock = new Socket(AddressFamily.InterNetwork,                         SocketType.Stream, ProtocolType.Tcp);
                sock.Bind(new IPEndPoint(IPAddress.Any, 0)); // Pass 0 here.
                var tcpPort = ((IPEndPoint)sock.LocalEndPoint).Port;
                sock.Dispose();

                var tcpTransportType = new pjsip4net.Core.Data.TransportConfig()
                {
                    BoundAddress = "0.0.0.0",
                    Port = (uint)tcpPort,
                    PublicAddress = "0.0.0.0",
                };

                var tcp = new pjsip4net.Core.Utils.Tuple<pjsip4net.Core.TransportType, pjsip4net.Core.Data.TransportConfig>
                    (pjsip4net.Core.TransportType.Tcp, tcpTransportType);
                o.RegisterTransport(tcp);
            });

            return this;
        }


        public SIPServiceConfigurator UseUdp()
        {
            cfg = cfg.With(o =>
            {
                Socket socku = new Socket(AddressFamily.InterNetwork,                         SocketType.Dgram, ProtocolType.Udp);
                socku.Bind(new IPEndPoint(IPAddress.Any, 0)); // Pass 0 here.
                var udpPort = ((IPEndPoint)socku.LocalEndPoint).Port;
                socku.Dispose();


                var udpTransportType = new pjsip4net.Core.Data.TransportConfig()
                {
                    BoundAddress = "0.0.0.0",
                    Port = (uint)udpPort,
                    PublicAddress = "0.0.0.0",
                };

                var udp = new pjsip4net.Core.Utils.Tuple<pjsip4net.Core.TransportType, pjsip4net.Core.Data.TransportConfig>
                        (pjsip4net.Core.TransportType.Udp, udpTransportType);
                o.RegisterTransport(udp);
            });

            return this;
        }

        public SIPServiceConfigurator AddOutboundProxy(Uri sipUri)
        {
            cfg = cfg.With(o =>
            {
                o.Config.OutboundProxies.Add(sipUri.ToString());
            });

            return this;
        }



    }
}
