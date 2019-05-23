using System.ComponentModel;
using System.Runtime.InteropServices;
using Bleak.Native;

namespace Bleak.Handlers
{
    internal static class ExceptionHandler
    {
        internal static void ThrowWin32Exception(string message)
        {
            // Get the error code associated with the last Windows error

            var lastWin32ErrorCode = Marshal.GetLastWin32Error();

            throw new Win32Exception($"{message} with error code {lastWin32ErrorCode}");
        }

        internal static void ThrowWin32Exception(string message, Enumerations.NtStatus ntStatus)
        {
            // Convert the NT Status to a DOS error code

            var dosErrorCode = PInvoke.RtlNtStatusToDosError(ntStatus);

            throw new Win32Exception($"{message} with error code {dosErrorCode}");
        }
    }
}