using System;

namespace DSIITool
{
    public class Hack
    {
        public byte[] OriginalBytes { get; private set; }
        public byte[] PatchBytes { get; private set; }
        public long Address { get; private set; }

        private IntPtr _hWnd;

        public Hack(long address, byte[] originalBytes, byte[] patchBytes)
        {
            try
            {
                Address = address;
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
                Native.WriteProcessMemory(_hWnd, Address, PatchBytes, PatchBytes.Length, out var _);
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
                Native.WriteProcessMemory(_hWnd, Address, OriginalBytes, OriginalBytes.Length, out var _);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
