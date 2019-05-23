using System.ComponentModel;
using Bleak.Handlers;
using Bleak.Injection.Interfaces;
using Bleak.Injection.Objects;
using Bleak.Native;
using Bleak.Shared;

namespace Bleak.Injection.Extensions
{
    internal class EjectDll : IInjectionExtension
    {
        private readonly InjectionWrapper _injectionWrapper;

        public EjectDll(InjectionWrapper injectionWrapper)
        {
            _injectionWrapper = injectionWrapper;
        }

        public bool Call(InjectionContext injectionContext)
        {
            var dllBaseAddress = injectionContext.DllBaseAddress;

            if (_injectionWrapper.InjectionMethod == InjectionMethod.ManualMap)
            {
                // Get the address of the entry point of the DLL in the remote process

                var dllEntryPointAddress = _injectionWrapper.RemoteProcess.IsWow64
                                         ? dllBaseAddress.AddOffset(_injectionWrapper.PeParser.GetPeHeaders().NtHeaders32.OptionalHeader.AddressOfEntryPoint)
                                         : dllBaseAddress.AddOffset(_injectionWrapper.PeParser.GetPeHeaders().NtHeaders64.OptionalHeader.AddressOfEntryPoint);

                // Calculate the address of the exception table

                var exceptionTable = _injectionWrapper.PeParser.GetPeHeaders().NtHeaders64.OptionalHeader.DataDirectory[3];

                var exceptionTableAddress = dllBaseAddress.AddOffset(exceptionTable.VirtualAddress);

                // Call the DllMain function of the DLL with DllProcessDetach in the remote process

                if (!_injectionWrapper.RemoteProcess.CallFunction<bool>(dllEntryPointAddress, (ulong) dllBaseAddress, Constants.DllProcessDetach, 0))
                {
                    throw new Win32Exception("Failed to call DllMain in the remote process");
                }

                // Remove the exception table from the dynamic function table of the remote process

                if (!_injectionWrapper.RemoteProcess.CallFunction<bool>("kernel32.dll", "RtlDeleteFunctionTable", (ulong) exceptionTableAddress))
                {
                    throw new Win32Exception("Failed to remove an exception table from the dynamic function table of the remote process");
                }

                // Free the memory previously allocated for the DLL in the remote process

                _injectionWrapper.MemoryManager.FreeVirtualMemory(dllBaseAddress);
            }

            else
            {
                if (!_injectionWrapper.RemoteProcess.CallFunction<bool>("kernel32.dll", "FreeLibrary", (ulong) dllBaseAddress))
                {
                    throw new Win32Exception("Failed to call FreeLibrary in the remote process");
                }
            }

            return true;
        }
    }
}