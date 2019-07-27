using System;
using System.Runtime.InteropServices;

namespace DSIITool
{
    internal class Native
    {
        [DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr handle, long address, byte[] bytes, int nsize, ref int op);

        [DllImport("Kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hwind, long Address, byte[] bytes, int nsize, out int output);

        [DllImport("Kernel32.dll")]
        public static extern IntPtr OpenProcess(int Token, bool inheritH, int ProcID);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, long lpAddress,
        uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

        internal enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        internal enum MemoryProtection
        {
            NoAccess = 0x0001,
            ReadOnly = 0x0002,
            ReadWrite = 0x0004,
            WriteCopy = 0x0008,
            Execute = 0x0010,
            ExecuteRead = 0x0020,
            ExecuteReadWrite = 0x0040,
            ExecuteWriteCopy = 0x0080,
            GuardModifierflag = 0x0100,
            NoCacheModifierflag = 0x0200,
            WriteCombineModifierflag = 0x0400,
            AllAccess = 2035711
        }
    }
}
