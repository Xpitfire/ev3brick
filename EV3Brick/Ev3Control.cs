using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace PrgSps2Gr1
{
    class Ev3Control
    {  
        private const float MinObjectDistanceDelta = 100F;
        private const int OffSet = 10;
        private const int SpinningSpeed = 5;
        private readonly Vehicle _vehicle;
        private readonly IRSensor _irSensor;
        private readonly UltraSonicSensor _ultraSonicSensor;
        private readonly Lcd _lcd;
        private readonly TouchSensor _touchSensor;
        private Point _point;
        private readonly EV3ColorSensor _colorSensor;
        private readonly Motor _motorSensorSpinner;
        private int _spinDirection;
        private bool _spinClockwise;
        private const int MinSpin = -90;
        private const int MaxSpin = 90;
        private const int SpinStep = 5;


        public enum Speed 
        {
            Slow = 10, 
            Medium = 50, 
            Fast = 70, 
            Turbo = 100
        }

        public Color SavedColor { get; set; }

        public Ev3Control()
        {
            // init motors
            _motorSensorSpinner = new Motor(MotorPort.OutB);
            _vehicle = new Vehicle(MotorPort.OutA, MotorPort.OutD);

            // init sensors
            _irSensor = new IRSensor(SensorPort.In1);
            _ultraSonicSensor = new UltraSonicSensor(SensorPort.In2, UltraSonicMode.Centimeter);
            _colorSensor = new EV3ColorSensor(SensorPort.In2, ColorMode.Color);
            _touchSensor = new TouchSensor(SensorPort.In3);

            // init display
            _point = new Point(0, 0);
            _lcd = new Lcd();
            _lcd.Clear();

            // init ev3 default settings
            _spinDirection = 0;
            _spinClockwise = true;
        }

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

        public enum TurnDirection
        {
            Left, Right
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

        public void VehicleStop()
        {
            _vehicle.Off();
        }

        public bool ReachedEdge()
        {
            return _touchSensor.IsPressed();
        }

        public void SpinScanner(bool active)
        {
            if (!active) return;

            if (_spinDirection >= MaxSpin)
            {
                _spinClockwise = false;
            }
            if (_spinDirection <= MinSpin)
            {
                _spinClockwise = true;
            }

            if (_spinClockwise)
            {
                _spinDirection += SpinStep;
            }
            else
            {
                _spinDirection -= SpinStep;
            }

            _motorSensorSpinner.MoveTo(SpinningSpeed, _spinDirection, true, false);
        }

        public bool ObjectDetected()
        {
            return _ultraSonicSensor.ReadDistance() < MinObjectDistanceDelta;
        }

        public void WriteLine(string s)
        {
            if (_point.Y <= (Lcd.Height - 2 * OffSet))
            {
                _point.Y += OffSet;
            }
            else
            {
                _lcd.Clear();
                _point.Y = OffSet;
                _lcd.Update(OffSet);
            }
            
            _lcd.WriteText(Font.SmallFont, _point, s, true);
            _lcd.Update(OffSet);
        }

        public Color ScanColor()
        {
            return _colorSensor.ReadColor();
        }


    }
}
