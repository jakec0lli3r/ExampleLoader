using System;

namespace Bleak.Injection.Objects
{
    internal class InjectionContext
    {
        internal IntPtr DllBaseAddress;

        internal bool HeadersRandomised;

        internal bool Injected;

        internal bool PebEntryHidden;
    }
}