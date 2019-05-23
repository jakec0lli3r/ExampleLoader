using System;
using Bleak.Injection.Interfaces;
using Bleak.Injection.Objects;

namespace Bleak.Injection.Extensions
{
    internal class RandomiseDllHeaders : IInjectionExtension
    {
        private readonly InjectionWrapper _injectionWrapper;

        public RandomiseDllHeaders(InjectionWrapper injectionWrapper)
        {
            _injectionWrapper = injectionWrapper;
        }

        public bool Call(InjectionContext injectionContext)
        {
            var headerSize = _injectionWrapper.RemoteProcess.IsWow64
                           ? _injectionWrapper.PeParser.GetPeHeaders().NtHeaders32.OptionalHeader.SizeOfHeaders
                           : _injectionWrapper.PeParser.GetPeHeaders().NtHeaders64.OptionalHeader.SizeOfHeaders;

            // Write over the header region of the DLL with random bytes

            var randomBuffer = new byte[(int) headerSize];

            new Random().NextBytes(randomBuffer);

            _injectionWrapper.MemoryManager.WriteVirtualMemory(injectionContext.DllBaseAddress, randomBuffer);

            return true;
        }
    }
}