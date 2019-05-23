using System;
using Bleak.PortableExecutable;

namespace Bleak.RemoteProcess.Objects
{
    internal class Module : IDisposable
    {
        internal readonly IntPtr BaseAddress;

        internal readonly string FilePath;

        internal readonly string Name;

        internal readonly Lazy<PeParser> PeParser;

        internal Module(IntPtr baseAddress, string filePath, string name)
        {
            BaseAddress = baseAddress;

            FilePath = filePath;

            Name = name;
            
            PeParser = new Lazy<PeParser>(() => new PeParser(filePath));
        }
        
        public void Dispose()
        {
            if (PeParser.IsValueCreated)
            {
                PeParser.Value.Dispose();
            }
        }
    }
}