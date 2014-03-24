﻿using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;

namespace PrgSps2Gr1
{
    class Ev3Control
    {
        private const int OffSet = 10;
        private readonly Vehicle _vehicle;
        private readonly IRSensor _irSensor;
        private readonly Lcd _lcd;
        private Point _point = new Point(0, 0);

        public Ev3Control()
        {
            _vehicle = new Vehicle(MotorPort.OutA, MotorPort.OutD);
            _irSensor = new IRSensor(SensorPort.In1);
            _lcd = new Lcd();
            _lcd.Clear();
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

    }
}
