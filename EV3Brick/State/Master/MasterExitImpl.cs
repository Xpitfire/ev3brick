﻿using System;
using Sps2Gr1.InTeam.Logging;

namespace Sps2Gr1.InTeam.State.Master
{
    class MasterExitImpl : AState
    {
        public const string Name = "MasterExit";
        private const int Level = EventQueue.StateLevel.Level1;

        protected override void PerformRecurrentAction()
        {
            // nothing must be called
        }

        protected override void PerformSingleAction()
        {
            Logger.Log("Stopping motors");
            Ev3.StopAllMovements();
            Logger.Log("Exiting application...");
            StateController.Exit();
        }

        public override int GetStateLevel()
        {
            return Level;
        }

        public override object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

        public override String ToString()
        {
            return Name;
        }

    }
}
