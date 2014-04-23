using System;

namespace PrgSps2Gr1.Control.Impl
{
    public class DeviceEv3SimControlImpl : IDeviceControl
    {
        private static readonly object Sync = new object();

        private static DeviceEv3SimControlImpl _instance;

        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action ReachedEdgeEvent;

        public int SpinScannerCnt { get; set; }

        private string _consoleText;
        public string Ev3ConsoleText { 
            get 
            { 
                lock (Sync) return _consoleText; 
            } 
        }

        private DeviceEv3SimControlImpl()
        {
            _instance = this;
        }

        public static DeviceEv3SimControlImpl GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DeviceEv3SimControlImpl();
            }
            return _instance;
        }

        public void OnEscapeReleasedButtonEvent(object sender, EventArgs e)
        {
            EscapeReleasedButtonEvent.Invoke();
        }

        public void OnEnterReleasedButtonEvent(object sender, EventArgs e)
        {
            EnterReleasedButtonEvent.Invoke();
        }

        public void OnReachedEdgeEvent(object sender, EventArgs e)
        {
            ReachedEdgeEvent.Invoke();
        }


        public void SpinScanner(bool active)
        {
            SpinScannerCnt++;
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

        public void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            // TODO implement
        }

        public void WriteLine(string s)
        {
            lock (Sync)
            {
                _consoleText += "\n" + s;
            }
        }

        public object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

    }
}
