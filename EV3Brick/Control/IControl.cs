using System;
using PrgSps2Gr1.Debug;

namespace PrgSps2Gr1.Control
{
    interface IControl : IDebug
    {
        event Action EscapeReleasedButtonEvent;

        event Action EnterReleasedButtonEvent;

        event Action ReachedEdgeEvent;

        void SpinScanner(bool active);

        void StopAllMovements();

        void VehicleDrive(sbyte speed);

        void VehicleStop();

        void VehicleReverse(Ev3ControlImpl.TurnDirection turn, sbyte speed, sbyte turnPercent);

        void WriteLine(string s);
    }
}
