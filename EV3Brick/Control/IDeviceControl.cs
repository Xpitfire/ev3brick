using System;
using MonoBrickFirmware.Sensors;
using Sps2Gr1.InTeam.Debug;

namespace Sps2Gr1.InTeam.Control
{
    public interface IDeviceControl : IDebug
    {
        event Action EscapeReleasedButtonEvent;

        event Action EnterReleasedButtonEvent;

        event Action UpReleasedButtonEvent;

        event Action ReachedEdgeEvent;

        event Action IdentifiedEnemyEvent;

        event Action DetectedObjectEvent;

        
        bool HasLostObject();


        void InitColor();
        
        void StopAllMovements();

        void SpinVehicle();

        void VehicleDrive(sbyte speed);

        void VehicleStop();

        void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent);


        void WriteLine(string s);
    }
}
