using System;
using SPSGrp1Grp2.Cunt.Logging;

namespace SPSGrp1Grp2.Cunt.Control.Impl
{
    public class DeviceEv3SimControlImpl : IDeviceControl
    {
        private static readonly object Sync = new object();

        private static DeviceEv3SimControlImpl _instance;
        private string _consoleText;

        public string Ev3ConsoleText
        {
            get { lock (Sync) return _consoleText; }
        }

        #region Ev3 Simulation Events

        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action UpReleasedButtonEvent;
        public event Action ReachedEdgeEvent;
        public event Action IdentifiedEnemyEvent;
        public event Action DetectedObjectEvent;

        public void OnEscapeReleasedButtonEvent(object sender, EventArgs e)
        {
            var handler = EscapeReleasedButtonEvent;
            if (handler != null) handler();
        }

        public void OnEnterReleasedButtonEvent(object sender, EventArgs e)
        {
            var handler = EnterReleasedButtonEvent;
            if (handler != null) handler();
        }

        public void OnUpReleasedButtonEvent(object sender, EventArgs e)
        {
            var handler = UpReleasedButtonEvent;
            if (handler != null) handler();
        }

        public void OnReachedEdgeEvent(object sender, EventArgs e)
        {
            var handler = ReachedEdgeEvent;
            if (handler != null) handler();
        }

        public void OnDetectedObjectEvent(object sender, EventArgs e)
        {
            var handler = DetectedObjectEvent;
            if (handler != null) handler();
        }

        public void OnIdentifiredEnemyEvent(object sender, EventArgs e)
        {
            var handler = IdentifiedEnemyEvent;
            if (handler != null) handler();
        }

        #endregion

        /// <summary>
        /// Private constructor to create a sigleton for external
        /// simulation tests.
        /// </summary>
        private DeviceEv3SimControlImpl()
        {
            _instance = this;
        }

        public static DeviceEv3SimControlImpl GetInstance()
        {
            return _instance ?? (_instance = new DeviceEv3SimControlImpl());
        }

        #region Ev3 Device simulated implementaiton

        public void PlaySound(ushort Hz, ushort duration, int volume)
        {
            throw new NotImplementedException();
        }

        public void InitColor()
        {
            Logger.Log("InitColor action");
        }

        public bool HasLostObject()
        {
            Logger.Log("HasLostObject action not implemented --> true");
            return true;
        }

        public void StopAllMovements()
        {
            Logger.Log("StopAllMovements action");
        }

        public void SpinVehicle()
        {
            Logger.Log("SpinVehicle action");
        }

        public void VehicleDrive(sbyte speed)
        {
            Logger.Log("VehicleDrive action: speed = " + speed);
        }

        public void VehicleStop()
        {
            Logger.Log("VehicleStop action");
        }

        public void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            Logger.Log("VehicleReverse action: turn = " + turn + ", speed = " + speed + ", turnPercent = " + turnPercent);
        }

        public void WriteLine(string s)
        {
            lock (Sync)
            {
                _consoleText += "\n" + s;
            }
        }

        #endregion

        public object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }

    }
}
