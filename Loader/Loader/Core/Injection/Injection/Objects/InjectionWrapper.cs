using System;
using System.IO;
using Bleak.Assembly;
using Bleak.Handlers;
using Bleak.Memory;
using Bleak.Native;
using Bleak.PortableExecutable;
using Bleak.RemoteProcess;
using Bleak.Shared;

namespace Bleak.Injection.Objects
{
    internal class InjectionWrapper : IDisposable
    {
        internal readonly Assembler Assembler;

        internal readonly byte[] DllBytes;

        internal readonly string DllPath;

        internal readonly InjectionMethod InjectionMethod;

        internal readonly MemoryManager MemoryManager;

        internal readonly PeParser PeParser;

        internal readonly ProcessWrapper RemoteProcess;

        internal readonly WindowsVersion WindowsVersion;

        internal InjectionWrapper(InjectionMethod injectionMethod, int processId, byte[] dllBytes)
        {
            RemoteProcess = new ProcessWrapper(processId, WindowsVersion);

            Assembler = new Assembler(RemoteProcess.IsWow64);

            DllBytes = dllBytes;

            InjectionMethod = injectionMethod;

            MemoryManager = new MemoryManager(RemoteProcess.Process.SafeHandle);

            PeParser = new PeParser(dllBytes);

            WindowsVersion = GetWindowsVersion();
        }

        internal InjectionWrapper(InjectionMethod injectionMethod, int processId, string dllPath)
        {
            RemoteProcess = new ProcessWrapper(processId, WindowsVersion);

            Assembler = new Assembler(RemoteProcess.IsWow64);

            DllBytes = File.ReadAllBytes(dllPath);

            DllPath = dllPath;

            InjectionMethod = injectionMethod;

            MemoryManager = new MemoryManager(RemoteProcess.Process.SafeHandle);

            PeParser = new PeParser(dllPath);

            WindowsVersion = GetWindowsVersion();
        }

        internal InjectionWrapper(InjectionMethod injectionMethod, string processName, byte[] dllBytes)
        {
            RemoteProcess = new ProcessWrapper(processName, WindowsVersion);

            Assembler = new Assembler(RemoteProcess.IsWow64);

            DllBytes = dllBytes;

            InjectionMethod = injectionMethod;

            MemoryManager = new MemoryManager(RemoteProcess.Process.SafeHandle);

            PeParser = new PeParser(dllBytes);

            WindowsVersion = GetWindowsVersion();
        }

        internal InjectionWrapper(InjectionMethod injectionMethod, string processName, string dllPath)
        {
            RemoteProcess = new ProcessWrapper(processName, WindowsVersion);

            Assembler = new Assembler(RemoteProcess.IsWow64);

            DllBytes = File.ReadAllBytes(dllPath);

            DllPath = dllPath;

            InjectionMethod = injectionMethod;

            MemoryManager = new MemoryManager(RemoteProcess.Process.SafeHandle);

            PeParser = new PeParser(dllPath);

            WindowsVersion = GetWindowsVersion();
        }

        public void Dispose()
        {
            PeParser.Dispose();

            RemoteProcess.Dispose();
        }

        private WindowsVersion GetWindowsVersion()
        {
            if (PInvoke.RtlGetVersion(out var versionInformation) != Enumerations.NtStatus.Success)
            {
                ExceptionHandler.ThrowWin32Exception("Failed to determine the version of Windows");
            }

            switch (versionInformation.MajorVersion)
            {
                case 6:
                {
                    if (versionInformation.MinorVersion == 1)
                    {
                        return WindowsVersion.Windows7;
                    }

                    if (versionInformation.MinorVersion == 2)
                    {
                        return WindowsVersion.Windows8;
                    }

                    if (versionInformation.MinorVersion == 3)
                    {
                        return WindowsVersion.Windows8Point1;
                    }

                    throw new PlatformNotSupportedException("This library is intended Windows versions >= 7 only");
                }

                case 10:
                {
                    return WindowsVersion.Windows10;
                }

                default:
                {
                    throw new PlatformNotSupportedException("This library is intended Windows versions >= 7 only");
                }
            }
        }
    }
}