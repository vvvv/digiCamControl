using System;
using EDSDKLib;

namespace Canon.Eos.Framework
{
    partial class EosCamera
    {
        private void OnStateEventShutdown(EventArgs eventArgs)
        {
            if (this.Shutdown != null)
                this.Shutdown.BeginInvoke(this, eventArgs, null, null);
        }
        private void OnStateEventWillShutdown(EventArgs eventArgs)
        {
            if (this.WillShutdown != null)
                this.WillShutdown.BeginInvoke(this, eventArgs, null, null);
        }

        private uint HandleStateEvent(uint stateEvent, uint param, IntPtr context)
        {
            EosFramework.LogInstance.Debug("HandleStateEvent fired: " + stateEvent);
            switch (stateEvent)
            {
                case EDSDK.StateEvent_Shutdown:
                    this.OnStateEventShutdown(EventArgs.Empty);
                    break;

                case EDSDK.StateEvent_WillSoonShutDown:
                    this.OnStateEventWillShutdown(EventArgs.Empty);
                    break;
            }
            return EDSDK.EDS_ERR_OK;
        }        
    }
}
