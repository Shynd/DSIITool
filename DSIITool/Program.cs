using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DSIITool
{
    class Program
    {
        private static IntPtr _wHandle = IntPtr.Zero;
        private static int _procId;
        private static long _baseAddr;

        private static Dictionary<string, Hack> _hacks;

        static void Main(string[] args)
        {
            try
            {
                if (!Utils.HasAdminPrivs())
                {
                    Console.WriteLine("Run this program as administrator!");
                    Console.ReadLine();
                    return;
                }

                var procs = Process.GetProcessesByName("DarkSoulsII");
                Console.WriteLine("PROC: " + procs.FirstOrDefault().Id);
                foreach (var proc in procs)
                {
                    _wHandle = Native.OpenProcess((int) Native.MemoryProtection.AllAccess, false, proc.Id);
                    _procId = proc.Id;
                    _baseAddr = (long) Process.GetProcessById(_procId).MainModule.BaseAddress;
                }

                _hacks = new Dictionary<string, Hack>();

                var staminaHack = new Hack(
                    _wHandle,
                    _baseAddr + 0x32D2EE,
                    new byte[] { 0x89, 0x81, 0xAC, 0x01, 0x00, 0x00 }, // original: mov [rcx+000001AC],eax
                    new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }  // patched:  
                );
                _hacks.Add("stamina", staminaHack);

                var estusHack = new Hack(
                    _wHandle,
                    _baseAddr + 0x1AA952 + 1,
                    new byte[] { 0x8D, 0x51, 0xFF }, // original: lea edx,[rcx-01]
                    new byte[] { 0x8B, 0xD1, 0x90 }  // patched:  mov edx, edx
                );
                _hacks.Add("estus", estusHack);

                // Enable hacks
                _hacks["stamina"].Enable();
                _hacks["estus"].Enable();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();

            _hacks["stamina"].Disable();
            _hacks["estus"].Disable();
        }
    }
}
