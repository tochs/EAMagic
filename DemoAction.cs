using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMagic
{
    public class DemoAction : Action
    {
        public DemoAction(string name, Location location) : base(name, location) { }

        private void DumpElement(EA.Repository repo, EA.Element elm)
        {
            EA.Package pkg = repo.GetPackageByID(elm.PackageID);
            repo.Write($"Element: {elm.Name}");
            foreach (EA.Element sub in elm.Elements)
            {
                DumpElement(repo, sub);
            }
        }

        private void DumpPackage(EA.Repository repo, EA.Package pkg)
        {
            repo.Write($"Package: {pkg.Name}");
            foreach (EA.Element elm in pkg.Elements)
            {
                DumpElement(repo, elm);
            }
            foreach (EA.Package sub in pkg.Packages)
            {
                DumpPackage(repo, sub);
            }
        }

        override public void Do(EA.Repository repo)
        {
            repo.Write(Name);
            switch (repo.GetContextItem(out object obj))
            {
                case EA.ObjectType.otElement:
                    DumpElement(repo, (EA.Element)obj);
                    break;
                case EA.ObjectType.otPackage:
                    DumpPackage(repo, (EA.Package)obj);
                    break;
            }
        }

        override public bool IsEnabled(EA.Repository repository)
        {
            return true;
        }

    }
}
