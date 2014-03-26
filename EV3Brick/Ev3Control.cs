using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace PrgSps2Gr1
{
    class Ev3Control
    {
        private const float MinObjectDistanceDelta = 100F;
        private const int OffSet = 10;
        private readonly Vehicle _vehicle;
        private readonly IRSensor _irSensor;
        private readonly UltraSonicSensor _ultraSonicSensor;
        private readonly Lcd _lcd;
        private Point _point = new Point(0, 0);
        private readonly EV3ColorSensor _colorSensor;

        public Ev3Control()
        {
            _vehicle = new Vehicle(MotorPort.OutA, MotorPort.OutD);
            _irSensor = new IRSensor(SensorPort.In1);
            _ultraSonicSensor = new UltraSonicSensor(SensorPort.In2, UltraSonicMode.Centimeter);
            _lcd = new Lcd();
            _lcd.Clear();
            _colorSensor = new EV3ColorSensor(SensorPort.In2, ColorMode.Color);
        }

        public void VehicleDirve(sbyte speed)
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
            return _irSensor.Read() < 30;
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

        public Color GetColor()
        {
            return _colorSensor.ReadColor();
        }
    }
}
