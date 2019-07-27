using System;

namespace DSIITool
{
    public class Hack
    {
        public byte[] OriginalBytes { get; private set; }
        public byte[] PatchBytes { get; private set; }
        public long Address { get; private set; }
        public IntPtr HWnd { get; private set; }

        public Hack(long address, IntPtr hWnd, byte[] originalBytes, byte[] patchBytes)
        {
            try
            {
                Address = address;
                HWnd = hWnd;
                OriginalBytes = originalBytes;
                PatchBytes = patchBytes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Enable()
        {
            try
            {
                Native.WriteProcessMemory(HWnd, Address, PatchBytes, PatchBytes.Length, out var _);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Disable()
        {
            try
            {
                Native.WriteProcessMemory(HWnd, Address, OriginalBytes, OriginalBytes.Length, out var _);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
