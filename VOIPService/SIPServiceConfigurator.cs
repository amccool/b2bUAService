using pjsip4net.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOIPService
{
    class SIPServiceConfigurator
    {
        private Configure cfg;

        internal SIPServiceConfigurator()
        {
            cfg = new Configure();
        }

        public SIPServiceConfigurator()


        Configure.Pjsip4Net().With(x => x.Config.AutoAnswer = false).With(x => x.Config.OutboundProxies.Add("sip:192.168.1.1:5060")).Build().Start();


        internal SIPService Build()
        {
            return new SIPService(cfg);
        }


        public

    }
}
