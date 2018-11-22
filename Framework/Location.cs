using System;

namespace EAMagic
{
    [Flags] public enum Location { None = 0, TreeView = 1, MainMenu = 2, Diagram = 4 };

    public static class LocationExtensions
    {
        public static Location ToLocation(this string locationName)
        {
            if (Enum.IsDefined(typeof(Location), locationName))
                return (Location)Enum.Parse(typeof(Location), locationName);
            return Location.None;
        }
    }
}