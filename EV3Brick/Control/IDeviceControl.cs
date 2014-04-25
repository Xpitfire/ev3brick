using System;
using PrgSps2Gr1.Debug;

namespace PrgSps2Gr1.Control
{
    public interface IDeviceControl : IDebug
    {
        event Action EscapeReleasedButtonEvent;

        event Action EnterReleasedButtonEvent;

        event Action ReachedEdgeEvent;

        event Action IdentifyObjectEvent;

        event Action DetectedObjectEvent;

        void InitSpinScanner();

        bool HasLostObject();
        
        void StopAllMovements();

        void SpinVehicle();

        void VehicleDrive(sbyte speed);

        void VehicleStop();

        void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent);

        void WriteLine(string s);
    }
}
