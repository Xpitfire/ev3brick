using System;
using System.Threading;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.UserInput;
using Sps2Gr1.InTeam.Logging;
using MonoBrickFirmware.Sound;
using Sps2Gr1.InTeam.Utility;

namespace Sps2Gr1.InTeam.Control.Impl
{
    public class DeviceControlImpl : IDeviceControl
    {
        # region Local Variables

        private readonly object _sync = new object();
        private readonly ButtonEvents _buttonEvents;
        private readonly Vehicle _vehicle;
        private readonly IRSensor _irSensor;
        private readonly Lcd _lcd;
        private readonly TouchSensor _touchSensor;
		private readonly  EV3ColorSensor _colorSensor; //NXTColorSensor _colorSensor;
        private readonly Motor _motorSensorSpinner;
        private readonly Ev3Timer _oscillationTimer = new Ev3Timer();
        private readonly Ev3Timer _reactivationTimer = new Ev3Timer();
        private readonly Speaker _speaker = new Speaker(100);
        private bool _objDetectedChange = true;
        private static Color _enemyColor;

        # endregion

        #region Properties

        Color SavedColor
        {
            get { lock (_sync) return _enemyColor; }
            set {
                lock (_sync)
                {
                    _enemyColor = value;
                }
            }
        }

        public bool UseSpinScanner { get; set; }

        #endregion

        /// <summary>
        /// Constructor: initializes the Firmware communication Devices
        /// to interact with the Monobrick API.
        /// </summary>
        public DeviceControlImpl()
        {
            // init motor spinner
            _motorSensorSpinner = new Motor(MotorPort.OutB);
            // init ev3 default settings
            _motorSensorSpinner.ResetTacho();
            UseSpinScanner = true;
            
            // init ev3 button events
            _buttonEvents = new ButtonEvents();
            _buttonEvents.EscapeReleased += () => OnEscapeReleasedButtonEvent(null, null);
            _buttonEvents.EnterReleased += () => OnEnterReleasedButtonEvent(null, null);
            _buttonEvents.UpReleased += () => OnUpReleasedButtonEvent(null, null);

            // init motor drive
            _vehicle = new Vehicle(MotorPort.OutA, MotorPort.OutD);

            // init sensors
            _irSensor = new IRSensor(SensorPort.In3);
			_colorSensor = new EV3ColorSensor (SensorPort.In1, ColorMode.Color);  //new NXTColorSensor(SensorPort.In2); 
            SavedColor = Color.White;

            // init touch sensor
			_touchSensor = new TouchSensor(SensorPort.In2);

            // init display
            _lcd = new Lcd();
            _lcd.Clear();

            // start the general sensor monitoring thread
            var threadMonitor = new Thread(SensorMonitorWorkThread);
            threadMonitor.Start();
            // start motor spinner thread
            var threadScanner = new Thread(ControlSpinScannerThread);
            threadScanner.Start();
        }

        #region Ev3 Events

        // ----- events declaration -----

        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action UpReleasedButtonEvent;
        public event Action ReachedEdgeEvent;
        public event Action IdentifiedEnemyEvent;
        public event Action DetectedObjectEvent;

        // ----- events implementation -----

        protected void OnReachedEdgeEvent(object sender, EventArgs e)
        {
            var handler = ReachedEdgeEvent;
            if (handler != null) handler();
        }

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

        public void OnIdentifiedEnemyEvent(object sender, EventArgs e)
        {
            var handler = IdentifiedEnemyEvent;
            if (handler != null) handler();
        }

        public void OnDetectedObjectEvent(object sender, EventArgs e)
        {
            var handler = DetectedObjectEvent;
            if (handler != null) handler();
        }

        # endregion

        #region Ev3 Firmware / Device implementation

        public void SpinVehicle()
        {
            Logger.Log("SpinLeft: speed" + DeviceConstants.Speed.Slower);
            _vehicle.SpinLeft(DeviceConstants.Speed.Slower);
        }

        /// <summary>
		/// Drive straight with the given speed.
		/// </summary>
		/// <param name="speed">Speed.</param>
        public void VehicleDrive(sbyte speed)
        {
            _vehicle.ReverseLeft = false;
            _vehicle.ReverseRight = false;
            if (speed < 0)
            {
                Logger.Log("Backward: speed = " + speed);
                _vehicle.Backward(speed);
            }
            else
            {
                Logger.Log("Forward: speed = " + speed);
                _vehicle.Forward(speed);
            }
        }

        public void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            Logger.Log("Turn: speed = " + speed + ", dir = " + turn + ", % = " + turnPercent);
            if (turn == DeviceConstants.TurnDirection.Left)
            {
                _vehicle.ReverseLeft = true;
                _vehicle.ReverseRight = false;
                _vehicle.TurnLeftReverse(speed, turnPercent);
            }
            else
            {
                _vehicle.ReverseLeft = false;
                _vehicle.ReverseRight = true;
                _vehicle.TurnRightReverse(speed, turnPercent);
            }
        }

        /// <summary>
        /// Check if a object, which previously has been detected, has disappeared again.
        /// This method is intended to be used via polling.
        /// </summary>
        /// <returns><c>true</c>, if object is gone, <c>true</c> otherwise <c>false</c>.</returns>
        public bool HasLostObject()
        {
            return _objDetectedChange;
        }

        public void PlaySound()
        {
            _speaker.Beep();
        }

        /// <summary>
		/// Stops all movements.
		/// </summary>
        public void StopAllMovements()
        {
            VehicleStop();
            _motorSensorSpinner.Off();
        }

		/// <summary>
		/// Stops the robot.
		/// </summary>
        public void VehicleStop()
        {
            _vehicle.Off();
        }

		/// <summary>
		/// Writes a string line to the EV3 Screen.
		/// </summary>
		/// <param name="s">S.</param>
        public void WriteLine(string s)
        {
            LcdConsole.WriteLine(s);
        }

        /// <summary>
        /// Read the color value of the color sensor.
        /// </summary>
        /// <returns>A color or none.</returns>
        public void InitColor()
        {
            SavedColor = _colorSensor.ReadColor();
            if (SavedColor == Color.None)
            {
                SavedColor = Color.White;
            }
            Logger.Log("Scanned enemy color: " + SavedColor);
        }

        // ----- special event update thread for state behavior -----

        /// <summary>
        /// Update thread to wrap the polling of sensor behaviors to a event base 
        /// system.
        /// ATTENTION! Be aware when adding new sensor to check if they are null.
        /// Because of a new init state multithreading call, a device may not be ready to
        /// use on startup.
        /// </summary>
        public void SensorMonitorWorkThread()
        {
            var touchSensorChange = true;
            while (StateController.IsAlive)
            {
                // monitor touch sensor activity
				if (touchSensorChange && _touchSensor != null && _touchSensor.IsPressed())
                {
                    OnReachedEdgeEvent(null, null);
                    touchSensorChange = false;
                }
                else if (!touchSensorChange && _touchSensor != null && !_touchSensor.IsPressed())
                {
                    touchSensorChange = true;
                }

                // monitor infrared sensor activity
                if (_objDetectedChange && _irSensor != null && _irSensor.ReadDistance() < 50)
                {
                    OnDetectedObjectEvent(null, null);
                    Logger.Log("Infrared value: " + _irSensor.ReadDistance());
                    _objDetectedChange = false;
                }
                else if (!_objDetectedChange && _irSensor != null && _irSensor.ReadDistance() >= 50)
                {
                    _objDetectedChange = true;
                }

                // monitor color sensor activity
                if (_colorSensor != null && SavedColor == _colorSensor.ReadColor())
                {
                    OnIdentifiedEnemyEvent(null, null);
                }
                
                Thread.Sleep(100);
            }
        }

        public void SpinScannerMaxPlusPos(object o, EventArgs e)
        {
            _oscillationTimer.Reset();
            _motorSensorSpinner.MoveTo((byte)DeviceConstants.Speed.Slower, 35, true, false);
        }

        public void SpinScannerToMaxMinusPos(object o, EventArgs e)
        {
            _oscillationTimer.Reset();
            _motorSensorSpinner.MoveTo((byte)DeviceConstants.Speed.Slower, -35, true, false);
        }

        public void ControlSpinScannerThread()
        {
            _oscillationTimer.TickTimeout = Ev3Timer.TickTime.Short;
            _reactivationTimer.TickTimeout = Ev3Timer.TickTime.Long;
            var initPos = true;
            // send the spin scanner to the first position
            SpinScannerToMaxMinusPos(null, null);
            Logger.Log("Spin scanner head.");
            while (StateController.IsAlive)
            {
                // control motor spin scanner
                if (UseSpinScanner && _motorSensorSpinner != null)
                {

                    if (initPos)
                    {
                        initPos = false;
                        SpinScannerMaxPlusPos(null, null);
                    }
                    else
                    {
                        if (_oscillationTimer.IsTimeout() && _motorSensorSpinner.GetTachoCount() > 30)
                        {
                            SpinScannerToMaxMinusPos(null, null);
                            _reactivationTimer.Reset();
                        }

                        if (_oscillationTimer.IsTimeout() && _motorSensorSpinner.GetTachoCount() < -30)
                        {
                            SpinScannerMaxPlusPos(null, null);
                            _reactivationTimer.Reset();
                        }


                        if (_reactivationTimer.IsTimeout())
                        {
                            SpinScannerToMaxMinusPos(null, null);
                            _reactivationTimer.Reset();
                            Logger.Log("Reactivated scanner.");
                        }
                    }
                }
                else
                {
                    _oscillationTimer.Reset();
                    _reactivationTimer.Reset();
                }

                Thread.Sleep(100);
            }
        }

        #endregion

        public object[] Debug(object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
