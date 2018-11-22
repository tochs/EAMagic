using System;
using System.Collections.Generic;

namespace EAMagic
{
    public abstract class Action
    {
        public readonly string Name;

        public readonly Location Location;

        public Action(string name, Location location)
        {
            this.Name = name;
            this.Location = location;
        }

        public bool IsVisible(Location location) {
            return this.Location.HasFlag(location);
        }

        abstract public bool IsEnabled(EA.Repository repository);

        abstract public void Do(EA.Repository repository);
    }

}
