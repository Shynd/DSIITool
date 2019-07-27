using System.Security.Principal;

namespace DSIITool
{
    internal class Utils
    {
        internal static bool HasAdminPrivs()
        {
            using (var id = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
