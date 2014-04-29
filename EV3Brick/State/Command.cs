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

        private int _level;


        public void SetCommandLevel(int level)
        {
            _level = level;
        }

        public int GetCommandLevel()
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
