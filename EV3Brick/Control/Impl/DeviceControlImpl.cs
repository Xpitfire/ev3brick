using System;
using System.Threading;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.UserInput;
using PrgSps2Gr1.Logging;
using PrgSps2Gr1.Utility;

namespace PrgSps2Gr1.Control.Impl
{
    public class DeviceControlImpl : IDeviceControl
    {
        # region Local Variables

        private readonly ButtonEvents _buttonEvents;
        private const float MinObjectDistanceDelta = 100F;
        private const int SpinningSpeed = 10;
        private readonly Vehicle _vehicle;
        private readonly IRSensor _irSensor;
        private readonly UltraSonicSensor _ultraSonicSensor;
        private readonly Lcd _lcd;
        private readonly TouchSensor _touchSensor;
		private readonly  EV3ColorSensor _colorSensor; //NXTColorSensor _colorSensor;
        private readonly Motor _motorSensorSpinner;
        private readonly Ev3Timer _oscillationTimer = new Ev3Timer();
        private readonly Ev3Timer _reactivationTimer = new Ev3Timer();
        private int _spinDegree;
        private bool _spinClockwise;
        private const int MinSpin = 0;
        private const int MaxSpin = 35;
        private bool _objDetectedChange = true;

        # endregion

        #region Properties

        public Color SavedColor { get; set; }

        public bool UseSpinScanner { get; set; }

        #endregion

        /// <summary>
        /// Constructor: initializes the Firmware communication Devices
        /// to interact with the Monobrick API.
        /// </summary>
        public DeviceControlImpl()
        {
            // start the general sensor monitoring thread
            var thread = new Thread(SensorMonitorWorkThread);
            thread.Start();
            thread = new Thread(ControlSpinScannerThread);
            thread.Start();

            // init ev3 button events
            _buttonEvents = new ButtonEvents();
            _buttonEvents.EscapeReleased += () => OnEscapeReleasedButtonEvent(null, null);
            _buttonEvents.EnterReleased += () => OnEnterReleasedButtonEvent(null, null);

            // init motor spinner
            _motorSensorSpinner = new Motor(MotorPort.OutC);
            // init ev3 default settings
            _spinDegree = 0;
            _motorSensorSpinner.ResetTacho();
            _spinClockwise = true;
            UseSpinScanner = true;

            // init motor drive
            _vehicle = new Vehicle(MotorPort.OutA, MotorPort.OutD);

            // init sensors
            _irSensor = new IRSensor(SensorPort.In4);
            //_ultraSonicSensor = new UltraSonicSensor(SensorPort.In2, UltraSonicMode.Centimeter);
			_colorSensor = new EV3ColorSensor (SensorPort.In1, ColorMode.Color);  //new NXTColorSensor(SensorPort.In2);
			//_colorSensor.Mode = ColorMode.Ambient;
			_touchSensor = new TouchSensor(SensorPort.In2);

            // init display
            _lcd = new Lcd();
            _lcd.Clear();
        }

        #region Ev3 Events

        // ----- events declaration -----

        public event Action EscapeReleasedButtonEvent;
        public event Action EnterReleasedButtonEvent;
        public event Action ReachedEdgeEvent;
        public event Action IdentifyObjectEvent;
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

        public void OnIdentifyObjectEvent(object sender, EventArgs e)
        {
            var handler = IdentifyObjectEvent;
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
                _vehicle.Backward(speed);
            }
            else
            {
                _vehicle.Forward(speed);
            }
        }

        public void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
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

        public void InitSpinScanner()
        {
            SpinScannerMaxPlusPos(null, null);
        }

        /// <summary>
        /// Check if a object, which previously has been detected, has disappeared again.
        /// </summary>
        /// <returns><c>true</c>, if object is gone, <c>true</c> otherwise <c>false</c>.</returns>
        public bool HasLostObject()
        {
            Logger.Log("HasLostObject = " + _objDetectedChange);
            return _objDetectedChange;
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
        public Color ScanColor()
        {
            return _colorSensor.ReadColor();
        }

        // ----- special event update thread for state behavior -----

        /// <summary>
        /// Update thread to wrap the polling of sensor behaviors to a event base 
        /// system.
        /// </summary>
        public void SensorMonitorWorkThread()
        {
            var touchSensorChange = true;
            while (ProgramEv3Sps2Gr1.IsAlive)
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
                if (_objDetectedChange && _irSensor != null && _irSensor.ReadDistance() < 40)
                {
                    OnDetectedObjectEvent(null, null);
                    _objDetectedChange = false;
                }
                else if (!_objDetectedChange && _irSensor != null && _irSensor.ReadDistance() >= 40)
                {
                    _objDetectedChange = true;
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
            while (ProgramEv3Sps2Gr1.IsAlive)
            {
                // control motor spin scanner
                if (UseSpinScanner)
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
