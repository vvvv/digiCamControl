﻿using System;
using System.Collections.Generic;
using Canon.Eos.Framework.Helper;
using EDSDKLib;

namespace Canon.Eos.Framework
{
    public sealed class EosCameraCollection : EosDisposable, IEnumerable<EosCamera>
    {
        private readonly IntPtr _cameraList;
        private int _count = -1;

        internal EosCameraCollection()
        {
            Util.Assert(EDSDK.EdsGetCameraList(out _cameraList), "Failed to get cameras.");
        }

        protected internal override void DisposeUnmanaged()
        {
            if (_cameraList != IntPtr.Zero)
                EDSDK.EdsRelease(_cameraList);
            base.DisposeUnmanaged();
        }

        /// <summary>
        /// Gets the number of cameras in this instance.
        /// </summary>
        public int Count
        {
            get
            {
                this.CheckDisposed();
                if(_count < 0)                
                    EDSDK.EdsGetChildCount(_cameraList, out _count);
                return _count;
            }
        }

        /// <summary>
        /// Gets the <see cref="Canon.Eos.Framework.EosCamera"/> at the specified index.
        /// </summary>
        public EosCamera this[int index]
        {
            get
            {
                this.CheckDisposed();
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException();

                IntPtr camera;
                Util.Assert(EDSDK.EdsGetChildAtIndex(_cameraList, index, out camera), string.Format("Failed to get camera #{0}.", index+1));
                if (camera == IntPtr.Zero)
                    throw new EosException(EDSDK.EDS_ERR_DEVICE_NOT_FOUND, string.Format("Failed to get camera #{0}.", index+1));
                return new EosCamera(camera);
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<EosCamera> GetEnumerator()
        {
            for (var i = 0; i < this.Count; ++i)
            {
                this.CheckDisposed();
                EosCamera camera = null;
                try
                {
                    camera = this[i];
                }
                catch (EosException ex)
                {
                    if(ex.EosErrorCode != EosErrorCode.CommDisconnected)                        
                        throw;
                }
                if(camera != null)
                    yield return camera;
            }
        }        

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            this.CheckDisposed();
            return this.GetEnumerator();
        }
    }
}
