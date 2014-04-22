using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1.Control
{
    class SimulationControlImpl : IControl
    {
        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action ReachedEdgeEvent;
        public void SpinScanner(bool active)
        {
            throw new NotImplementedException();
        }

        public void StopAllMovements()
        {
            throw new NotImplementedException();
        }

        public void VehicleDrive(sbyte speed)
        {
            throw new NotImplementedException();
        }

        public void VehicleStop()
        {
            throw new NotImplementedException();
        }

        public void VehicleReverse(Ev3ControlImpl.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string s)
        {
            throw new NotImplementedException();
        }

        public void Log()
        {
            throw new NotImplementedException();
        }

        public object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
