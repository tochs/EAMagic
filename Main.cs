using System.Collections.Generic;
namespace EAMagic
{
    public class Main
    {
        private readonly MenuHelper MenuHelper;
        private const string AddinName = "EAMagic";

        public Main() {
            MenuHelper = new MenuHelper();
            MenuHelper.Add("Demo&Group", new DemoAction("MainMenu", Location.MainMenu));
            MenuHelper.Add("Demo&Group", new DemoAction("TreeView", Location.TreeView));
            MenuHelper.Add("Demo&Group", new DemoAction("Diagram", Location.Diagram));
            MenuHelper.Add("Demo&Group", new DemoAction("All", Location.MainMenu | Location.TreeView | Location.Diagram));
        }

        private bool IsProjectOpen(EA.Repository repository)
        {
            try
            {
                EA.Collection c = repository.Models;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string Normalize(string name)
        {
            if (name.StartsWith("-"))
                return name.Substring(1);
            return name;
        }

        public string EA_Connect(EA.Repository repository)
        {
            return "";
        }

        public void EA_ShowHelp(EA.Repository repository, string locationName, string menuName, string itemName)
        {
        }

        public object EA_GetMenuItems(EA.Repository repository, string locationName, string menuName)
        {
            SortedSet<string> resultSet = new SortedSet<string>();

            if (menuName == "")
            {
                foreach (string actionGroup in MenuHelper.ActionGroups)
                {
                    resultSet.Add("-" + actionGroup);
                }
            }
            else
            {
                HashSet<Action> actions = MenuHelper.FindVisibleActions(Normalize(menuName), locationName.ToLocation());
                foreach(Action action in actions)
                {
                    resultSet.Add(action.Name);
                }
            }

            string[] result = new string[resultSet.Count];
            resultSet.CopyTo(result);
            return result;
        }

        public void EA_GetMenuState(EA.Repository repository, string locationName, string menuName, string itemName, ref bool isEnabled, ref bool isChecked)
        {
            isChecked = false;
            if (menuName == "")
            {
                HashSet<Action> actions = MenuHelper.FindVisibleActions(Normalize(menuName), locationName.ToLocation());
                if (actions.Count == 0)
                {
                    isEnabled = false;
                }
                else
                {
                    isEnabled = true;
                }
            }
            else
            {
                Action action = MenuHelper.FindAction(Normalize(menuName), Normalize(itemName));
                if (action == null)
                {
                    isEnabled = false;
                }
                else
                {
                    isEnabled = action.IsEnabled(repository);
                }
            }
        }

        public void EA_MenuClick(EA.Repository repository, string locationName, string menuName, string itemName)
        {
            Action action = MenuHelper.FindAction(Normalize(menuName), Normalize(itemName));
            if (action != null)
            {
                action.Do(repository);
            }
        }

        public void EA_Disconnect()
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
    }
}
