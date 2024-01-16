using Microsoft.AspNetCore.Identity;

namespace SysML_Sync
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int PLZ { get; set; }

        public string Ort { get; set; }

    }
}
