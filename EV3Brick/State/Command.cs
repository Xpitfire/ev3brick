using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sps2Gr1.InTeam.State
{
    internal class Command
    {
        private Action _action;

        private EventQueue.StateLevel _level;


        public void SetCommandLevel(EventQueue.StateLevel level)
        {
            _level = level;
        }

        public EventQueue.StateLevel GetCommandLevel()
        {
            return _level;
        }

        public void SetAction(Action a)
        {
            _action = a;
        }

        public void PerformAction()
        {
            _action();
        }

    }
}
