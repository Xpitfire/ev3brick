using System;
using System.Threading;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace PrgSps2Gr1
{
    class Ev3Utilities
    {
        private const float MinObjectDistanceDelta = 100F;
        private const int SpinningSpeed = 10;
        private readonly Vehicle _vehicle;
        private readonly IRSensor _irSensor;
        private readonly UltraSonicSensor _ultraSonicSensor;
        private readonly Lcd _lcd;
        private readonly TouchSensor _touchSensor;
		private readonly  EV3ColorSensor _colorSensor; //NXTColorSensor _colorSensor;
        private readonly Motor _motorSensorSpinner;
        private int _spinDegree;
        private bool _spinClockwise;
        private const int MinSpin = -35;
        private const int MaxSpin = 35;
        private const int SpinStep = 5;

        public Color SavedColor { get; set; }

        public enum Speed 
        {
            Slow = 10, 
            Medium = 50, 
            Fast = 70, 
            Turbo = 100
        }

        public enum TurnDirection
        {
            Left, Right
        }
        
        public Ev3Utilities()
        {
            // start the general sensor monitoring thread
            var thread = new Thread(SensorMonitorWorkThread);
            thread.Start();

            // init motors
            _motorSensorSpinner = new Motor(MotorPort.OutB);
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

            // init ev3 default settings
            _spinDegree = 0;
            _motorSensorSpinner.ResetTacho();
			_spinClockwise = true;
        }

        // ----- declare events -----
        public event Action ReachedEdgeEvent;

        protected virtual void OnReachedEdgeEvent()
        {
            var handler = ReachedEdgeEvent;
            if (handler != null) handler();
        }

        // ----- ev3 utility method usage -----

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

        public void VehicleReverse(TurnDirection turn, sbyte speed, sbyte turnPercent)
        {
            if (turn == TurnDirection.Left)
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

        public void SpinScanner(bool active)
        {
            if (!active) return;

            if (_spinDegree >= MaxSpin)
            {
                _spinClockwise = false;
            }
            if (_spinDegree <= MinSpin)
            {
                _spinClockwise = true;
            }

            if (_spinClockwise)
            {
                _spinDegree += SpinStep;
            }
            else
            {
                _spinDegree -= SpinStep;
            }
            
			//_motorSensorSpinner.MoveTo(SpinningSpeed, _spinDegree, false, false);
			//_motorSensorSpinner.On (SpinningSpeed, (uint)_spinDegree, false, false);
			//WriteLine("Des is -->" + _motorSensorSpinner.GetTachoCount());
			WriteLine("color: " + _colorSensor.ReadColor());
			WriteLine("  raw: " + _colorSensor.ReadRaw().ToString());
             
        }

		/// <summary>
		/// Check if a object is detected using the IR sensor.
		/// </summary>
		/// <returns><c>true</c>, if detected was objected, <c>false</c> otherwise.</returns>
		/// <param name="atDistance">At distance.</param>
        public bool ObjectDetected(int atDistance)
        {
            return _irSensor.ReadDistance() < atDistance ? true : false ;
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
            var changed = true;
            while (ProgramEv3Sps2Gr1.IsAlive)
            {
				if (changed && (_touchSensor != null) && _touchSensor.IsPressed())
                {
                    OnReachedEdgeEvent();
                    changed = false;
                }
                else if (!changed && !_touchSensor.IsPressed())
                {
                    changed = true;
                }
                Thread.Sleep(100);
            }
        }

    }
}
