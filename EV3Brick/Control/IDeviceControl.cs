using System;
using MonoBrickFirmware.Sensors;
using SPSGrp1Grp2.Cunt.Debug;

namespace SPSGrp1Grp2.Cunt.Control
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


        void PlaySound(ushort Hz, ushort duration, int volume);

        void InitColor();
        
        void StopAllMovements();

        void SpinVehicle();

        void VehicleDrive(sbyte speed);

        void VehicleStop();

        void VehicleReverse(DeviceConstants.TurnDirection turn, sbyte speed, sbyte turnPercent);


        void WriteLine(string s);
    }
}
