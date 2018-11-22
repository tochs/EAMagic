using System.Collections.Generic;

namespace EAMagic
{
    public class MenuHelper
    {
        private readonly Dictionary<string, HashSet<Action>> Actions;

        public MenuHelper()
        {
            Actions = new Dictionary<string, HashSet<Action>>();
        }

        public void Add(string actionGroup, Action action)
        {
            if (!Actions.ContainsKey(actionGroup))
            {
                Actions.Add(actionGroup, new HashSet<Action>());
            }
            Actions[actionGroup].Add(action);
        }

        public SortedSet<string> ActionGroups
        {
            get { return new SortedSet<string>(Actions.Keys); }
        }

        public HashSet<Action> FindVisibleActions(string actionGroup, Location location)
        {
            HashSet<Action> result = new HashSet<Action>();
            foreach (Action action in Actions[actionGroup])
            {
                if (action.IsVisible(location))
                {
                    result.Add(action);
                }
            }
            return result;
        }

        public Action FindAction(string actionGroup, string actionName)
        {
            HashSet<Action> actionSet = Actions[actionGroup];
            foreach (Action action in actionSet)
            {
                if (action.Name == actionName)
                {
                    return action;
                }
            }
            return null;
        }
    }
}
