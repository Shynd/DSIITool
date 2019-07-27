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
                    _baseAddr + (uint) Offsets.PlayerHacks.Stamina,
                    new byte[] { 0x89, 0x81, 0xAC, 0x01, 0x00, 0x00 }, // original: mov [rcx+000001AC],eax
                    new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }  // patched:  
                );
                _hacks.Add("stamina", staminaHack);

                var estusHack = new Hack(
                    _wHandle,
                    _baseAddr + (uint) Offsets.PlayerHacks.Estus + 1,
                    new byte[] { 0x8D, 0x51, 0xFF }, // original: lea edx,[rcx-01]
                    new byte[] { 0x8B, 0xD1, 0x90 }  // patched:  mov edx, edx
                );
                _hacks.Add("estus", estusHack);

                // The opcode responsible for decrementing
                // our HP is stored at:
                // DarkSoulsII.exe+16727A - 89 83 68010000        - mov [rbx+00000168],eax
                // where rbx+168 is our current HP, which is
                // overwritten by EAX, which stores what our HP
                // will be after taking damage.
                //
                // TODO: Add infinite health hack.
                // This requires some comparing because the
                // address is used by enemy NPCs as well.
                //
                // Comparation:
                // If [rbx+9C8] == 1 it's the player, 0 == enemy, so jump to our modified code
                // Else, jump to original code.
                // This is needed because if we don't,
                // the enemies will also have infinite health.

                // Enable hacks
                _hacks["stamina"].Enable();
                _hacks["estus"].Enable();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();

            // Disable hacks
            _hacks["stamina"].Disable();
            _hacks["estus"].Disable();
        }
    }
}
