using System;
using System.Runtime.InteropServices;
using Bleak.Handlers;
using Bleak.Native;
using Microsoft.Win32.SafeHandles;

namespace Bleak.Memory
{
    internal class MemoryManager
    {
        private readonly SafeProcessHandle _processHandle;

        internal MemoryManager(SafeProcessHandle processHandle)
        {
            _processHandle = processHandle;
        }

        internal IntPtr AllocateVirtualMemory(int allocationSize)
        {
            // Allocate a region of virtual memory in the remote process

            const Enumerations.AllocationType allocationType = Enumerations.AllocationType.Commit | Enumerations.AllocationType.Reserve;

            var regionAddress = PInvoke.VirtualAllocEx(_processHandle, IntPtr.Zero, allocationSize, allocationType, Enumerations.MemoryProtection.ExecuteReadWrite);

            if (regionAddress == IntPtr.Zero)
            {
                ExceptionHandler.ThrowWin32Exception("Failed to allocate virtual memory in the remote process");
            }

            return regionAddress;
        }

        internal IntPtr AllocateVirtualMemory(IntPtr baseAddress, int allocationSize)
        {
            // Allocate a region of virtual memory in the remote process at the specified address

            const Enumerations.AllocationType allocationType = Enumerations.AllocationType.Commit | Enumerations.AllocationType.Reserve;

            var regionAddress = PInvoke.VirtualAllocEx(_processHandle, baseAddress, allocationSize, allocationType, Enumerations.MemoryProtection.ExecuteReadWrite);

            if (regionAddress == IntPtr.Zero)
            {
                ExceptionHandler.ThrowWin32Exception("Failed to allocate virtual memory in the remote process");
            }

            return regionAddress;
        }

        internal IntPtr AllocateVirtualMemory<TStructure>() where TStructure : struct
        {
            // Allocate a region of virtual memory in the remote process

            const Enumerations.AllocationType allocationType = Enumerations.AllocationType.Commit | Enumerations.AllocationType.Reserve;

            var regionAddress = PInvoke.VirtualAllocEx(_processHandle, IntPtr.Zero, Marshal.SizeOf<TStructure>(), allocationType, Enumerations.MemoryProtection.ExecuteReadWrite);

            if (regionAddress == IntPtr.Zero)
            {
                ExceptionHandler.ThrowWin32Exception("Failed to allocate virtual memory in the remote process");
            }

            return regionAddress;
        }

        internal void FreeVirtualMemory(IntPtr baseAddress)
        {
            // Free a region of virtual memory in the remote process

            if (!PInvoke.VirtualFreeEx(_processHandle, baseAddress, 0, Enumerations.FreeType.Release))
            {
                ExceptionHandler.ThrowWin32Exception("Failed to free virtual memory in the remote process");
            }
        }

        internal Enumerations.MemoryProtection ProtectVirtualMemory(IntPtr baseAddress, int protectionSize, Enumerations.MemoryProtection protectionType)
        {
            // Change the protection of a region of virtual memory in the remote process

            if (!PInvoke.VirtualProtectEx(_processHandle, baseAddress, protectionSize, protectionType, out var oldProtectionType))
            {
                ExceptionHandler.ThrowWin32Exception("Failed to protect virtual memory in the remote process");
            }

            return oldProtectionType;
        }

        internal byte[] ReadVirtualMemory(IntPtr baseAddress, int bytesToRead)
        {
            // Read the specified number of bytes from the region of virtual memory in the remote process

            var bytesReadBuffer = Marshal.AllocHGlobal(bytesToRead);

            if (!PInvoke.ReadProcessMemory(_processHandle, baseAddress, bytesReadBuffer, bytesToRead, out _))
            {
                ExceptionHandler.ThrowWin32Exception("Failed to read virtual memory from the remote process");
            }

            var bytesRead = new byte[bytesToRead];

            Marshal.Copy(bytesReadBuffer, bytesRead, 0, bytesToRead);

            Marshal.FreeHGlobal(bytesReadBuffer);

            return bytesRead;
        }

        internal TStructure ReadVirtualMemory<TStructure>(IntPtr baseAddress) where TStructure : struct
        {
            var structureSize = Marshal.SizeOf<TStructure>();

            // Read the specified structure from the region of virtual memory in the remote process

            var structureBuffer = Marshal.AllocHGlobal(structureSize);

            if (!PInvoke.ReadProcessMemory(_processHandle, baseAddress, structureBuffer, structureSize, out _))
            {
                ExceptionHandler.ThrowWin32Exception("Failed to read virtual memory from the remote process");
            }

            var structure = Marshal.PtrToStructure<TStructure>(structureBuffer);

            Marshal.FreeHGlobal(structureBuffer);

            return structure;
        }

        internal void WriteVirtualMemory(IntPtr baseAddress, byte[] bytesToWrite)
        {
            // Adjust the protection of the region of virtual memory to ensure it has write privileges

            var originalProtectionType = ProtectVirtualMemory(baseAddress, bytesToWrite.Length, Enumerations.MemoryProtection.ReadWrite);

            // Write the bytes into the region of virtual memory in the remote process

            var bytesToWriteHandle = GCHandle.Alloc(bytesToWrite, GCHandleType.Pinned);

            if (!PInvoke.WriteProcessMemory(_processHandle, baseAddress, bytesToWriteHandle.AddrOfPinnedObject(), bytesToWrite.Length, out _))
            {
                ExceptionHandler.ThrowWin32Exception("Failed to write virtual memory in the remote process");
            }

            // Restore the original protection of the region of virtual memory

            ProtectVirtualMemory(baseAddress, bytesToWrite.Length, originalProtectionType);

            bytesToWriteHandle.Free();
        }

        internal void WriteVirtualMemory<TStructure>(IntPtr baseAddress, TStructure structureToWrite) where TStructure : struct
        {
            var structureSize = Marshal.SizeOf<TStructure>();

            // Adjust the protection of the region of virtual memory to ensure it has write privileges

            var originalProtectionType = ProtectVirtualMemory(baseAddress, structureSize, Enumerations.MemoryProtection.ReadWrite);

            // Write the structure into the region of virtual memory in the remote process

            var structureBuffer = Marshal.AllocHGlobal(structureSize);

            Marshal.StructureToPtr(structureToWrite, structureBuffer, false);

            if (!PInvoke.WriteProcessMemory(_processHandle, baseAddress, structureBuffer, structureSize, out _))
            {
                ExceptionHandler.ThrowWin32Exception("Failed to write virtual memory in the remote process");
            }

            // Restore the original protection of the region of virtual memory

            ProtectVirtualMemory(baseAddress, structureSize, originalProtectionType);

            Marshal.FreeHGlobal(structureBuffer);
        }
    }
}