namespace Bleak.Injection.Objects
{
    internal class ApiSetMapping
    {
        internal readonly string MappedToDll;

        internal readonly string VirtualDll;

        internal ApiSetMapping(string mappedToDll, string virtualDll)
        {
            MappedToDll = mappedToDll;

            VirtualDll = virtualDll;
        }
    }
}