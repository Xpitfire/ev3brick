using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1
{
    class NormalObjectDetectedImpl : State
    {
        protected override void PerformAction()
        {
            Ev3.WriteLine("got you !!!");
        }

        public override string ToString()
        {
            return "NormalObjectDetected";
        }
    }
}
