﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1
{
    class NormalPauseImpl : State
    {
        protected override void PerformAction()
        {
            // do nothing
            Ev3.VehicleStop();
        }

        public override string ToString()
        {
            return "NormalPause";
        }
    }
}
