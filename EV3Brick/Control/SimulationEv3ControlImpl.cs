using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrgSps2Gr1.Control
{
    class SimulationEv3ControlImpl : IEv3Control
    {
        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action ReachedEdgeEvent;
        
        private int _spinScannerCnt;

        public SimulationEv3ControlImpl()
        {
            new SimControlGui();
        }

        public void SpinScanner(bool active)
        {
            _spinScannerCnt++;
            //txtBoxSpinScanner.Text = _spinScannerCnt.ToString();
        }

        public void StopAllMovements()
        {
            // TODO implement
        }

        public void VehicleDrive(sbyte speed)
        {
            // TODO implement
        }

        public void VehicleStop()
        {
            // TODO implement
        }

        public void VehicleReverse(EV3Constants.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            // TODO implement
        }

        public void WriteLine(string s)
        {
            //txtBoxDisplayLog.Text = s;
        }

        public void Log()
        {
            // TODO implement
        }

        public object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

    }
}
