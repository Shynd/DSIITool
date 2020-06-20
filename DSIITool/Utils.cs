using System.Security.Principal;

namespace DSIITool
{
    internal class Utils
    {
        internal static bool HasAdminPrivs()
        {
            using (var id = WindowsIdentity.GetCurrent())
            {
                return new WindowsPrincipal(id).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
