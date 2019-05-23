namespace Bleak.PortableExecutable.Objects
{
    internal class ImportedFunction
    {
        internal string Dll;

        internal readonly string Name;

        internal readonly ulong Offset;

        internal readonly ushort Ordinal;

        internal ImportedFunction(string dll, string name, ulong offset)
        {
            Dll = dll;

            Name = name;

            Offset = offset;
        }

        internal ImportedFunction(string dll, string name, ulong offset, ushort ordinal)
        {
            Dll = dll;

            Name = name;

            Offset = offset;

            Ordinal = ordinal;
        }

        internal ImportedFunction(string dll, ulong offset, ushort ordinal)
        {
            Dll = dll;

            Offset = offset;

            Ordinal = ordinal;
        }
    }
}