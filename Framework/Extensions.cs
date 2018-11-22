using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMagic
{
    public static class Extensions
    {
        private static bool OutputCreated = false;
        public static void Write(this EA.Repository repo, string output, int id = 0)
        {
            if (!OutputCreated)
            {
                OutputCreated = true;
                repo.CreateOutputTab("EAMagic");
                repo.EnsureOutputVisible("EAMagic");
            }
            repo.WriteOutput("EAMagic", output, id);
        }

        public static EA.Package Package(this EA.Element elm, EA.Repository repo)
        {
            return repo.GetPackageByID(elm.PackageID);
        }

        public static EA.Package Parent(this EA.Package pkg, EA.Repository repo)
        {
            if (pkg.ParentID == 0)
                return null;
            return repo.GetPackageByID(pkg.ParentID);
        }

    }
}
