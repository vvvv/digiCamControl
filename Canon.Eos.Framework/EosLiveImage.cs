using System;
using System.Drawing;
using Canon.Eos.Framework.Helper;
using EDSDKLib;

namespace Canon.Eos.Framework
{
    public class EosLiveImage : EosObject
    {
        internal static EosLiveImage CreateFromStream(IntPtr stream)
        {
            IntPtr imagePtr;
            Util.Assert(EDSDK.EdsCreateEvfImageRef(stream, out imagePtr), "Failed to create evf image.");
            return new EosLiveImage(imagePtr);    
        }

        internal EosLiveImage(IntPtr imagePtr)
            : base(imagePtr) { }

        [EosProperty(EDSDK.PropID_Evf_ImagePosition)]
        public Point ImagePosition
        {
            get { return this.GetPropertyPointData(EDSDK.PropID_Evf_ImagePosition); }
        }

        [EosProperty(EDSDK.PropID_Evf_HistogramB)]
        public long[] HistogramB
        {
            get { return this.GetPropertyIntegerArrayData(EDSDK.PropID_Evf_HistogramB); }
        }

        [EosProperty(EDSDK.PropID_Evf_HistogramG)]
        public long[] HistogramG
        {
            get { return this.GetPropertyIntegerArrayData(EDSDK.PropID_Evf_HistogramG); }
        }

        [EosProperty(EDSDK.PropID_Evf_HistogramR)]
        public long[] HistogramR
        {
            get { return this.GetPropertyIntegerArrayData(EDSDK.PropID_Evf_HistogramR); }
        }

        [EosProperty(EDSDK.PropID_Evf_HistogramY)]
        public long[] HistogramY
        {
            get { return this.GetPropertyIntegerArrayData(EDSDK.PropID_Evf_HistogramY); }
        }

        [EosProperty(EDSDK.PropID_Evf_Zoom)]
        public long Zoom
        {
            get { return this.GetPropertyIntegerData(EDSDK.PropID_Evf_Zoom); }
        }

        [EosProperty(EDSDK.PropID_Evf_ZoomRect)]
        public Rectangle ZoomBounds
        {
            get { return this.GetPropertyRectangleData(EDSDK.PropID_Evf_ZoomRect); }
        }

        [EosProperty(EDSDK.PropID_Evf_ZoomPosition)]
        public Point ZoomPosition
        {
            get { return this.GetPropertyPointData(EDSDK.PropID_Evf_ZoomPosition); }
        }

        [EosProperty(EDSDK.PropID_Evf_CoordinateSystem)]
        public Size Size
        {
            get { return this.GetPropertySizeData(EDSDK.PropID_Evf_CoordinateSystem); }
        }
    }
}
