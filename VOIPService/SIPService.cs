using pjsip4net.Configuration;
using pjsip4net.Core.Configuration;
using pjsip4net.Interfaces;
using System;
using System.Diagnostics;

namespace VOIPService
{
    public class SIPService
    {
        private readonly ISipUserAgent ua;

        public SIPService(Configure cfg)
        {
            ua = cfg.Build();
        }


        public bool Start()
        {

            ua.Log += Ua_Log;
            ua.CallManager.CallRedirected += CallManager_CallRedirected;
            ua.CallManager.CallStateChanged += CallManager_CallStateChanged;
            ua.CallManager.CallTransfer += CallManager_CallTransfer;
            ua.CallManager.IncomingCall += CallManager_IncomingCall;
            ua.CallManager.IncomingDtmfDigit += CallManager_IncomingDtmfDigit;
            //ua.CallManager.MaxCalls = 100;
            ua.CallManager.Ring += CallManager_Ring;



            ua.AccountManager.AccountStateChanged += AccountManager_AccountStateChanged;



            ua.Start();

            var da = ua.AccountManager.DefaultAccount;


            IAccount account = ua.AccountManager.Register(o =>
            {

                return o.At("localhost").WithExtension("BRC1").Register();
            });

            return true;
        }

        private void AccountManager_AccountStateChanged(object sender, pjsip4net.Accounts.AccountStateChangedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void CallManager_Ring(object sender, pjsip4net.Calls.RingEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void CallManager_IncomingDtmfDigit(object sender, pjsip4net.Calls.DtmfEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void CallManager_IncomingCall(object sender, pjsip4net.Core.Utils.EventArgs<ICall> e)
        {
            //throw new System.NotImplementedException();
        }

        private void CallManager_CallTransfer(object sender, pjsip4net.Calls.CallTransferEventArgs e)
        {
           // throw new System.NotImplementedException();
        }

        private void CallManager_CallStateChanged(object sender, pjsip4net.Calls.CallStateChangedEventArgs e)
        {
           // throw new System.NotImplementedException();
        }

        private void CallManager_CallRedirected(object sender, pjsip4net.Calls.CallRedirectedEventArgs e)
        {
          //  throw new System.NotImplementedException();
        }

        private void Ua_Log(object sender, pjsip4net.LogEventArgs e)
        {
            Trace.WriteLine(e.Data);
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