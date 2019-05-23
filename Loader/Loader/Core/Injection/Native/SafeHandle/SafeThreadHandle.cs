using System;
using Bleak.Handlers;
using Microsoft.Win32.SafeHandles;

namespace Bleak.Native.SafeHandle
{
    internal class SafeThreadHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeThreadHandle() : base(true) { }

        protected override bool ReleaseHandle()
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            if (!PInvoke.CloseHandle(handle))
            {
                ExceptionHandler.ThrowWin32Exception("Failed to close a handle to a thread in the remote process");
            }

            return true;
        }
    }
}