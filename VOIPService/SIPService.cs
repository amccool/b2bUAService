using pjsip4net.Configuration;
using pjsip4net.Core.Configuration;
using pjsip4net.Interfaces;

namespace VOIPService
{
    internal class SIPService
    {
        private readonly ISipUserAgent ua;

        internal SIPService(Configure cfg)
        {
            ua = cfg.Build();
        }


        public bool Start()
        {
            ua.Start();

            return true;
        }

        public bool Stop()
        {
            if (ua != null)
            { ua.Destroy(); }

            if (ua != null)
            { ua.Dispose(); }

            return true;
        }

    }
}