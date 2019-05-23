using System;

namespace Bleak.RemoteProcess.Objects
{
    internal class Peb
    {
        internal readonly IntPtr ApiSetMap;

        internal readonly IntPtr Ldr;

        internal Peb(IntPtr apiSetMap, IntPtr ldr)
        {
            ApiSetMap = apiSetMap;

            Ldr = ldr;
        }
    }
}