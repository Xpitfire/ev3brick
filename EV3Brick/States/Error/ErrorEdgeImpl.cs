using System;

namespace PrgSps2Gr1
{
    class ErrorEdgeImpl : State
    {
        private readonly int _timeout = Environment.TickCount + 2000;
        
        private bool Timeout()
        {
            return Environment.TickCount > _timeout;
        }

        protected override void PerformAction()
        {
            Ev3.VehicleReverse(Ev3Utilities.TurnDirection.Left, 25, 90);
            
            if (Timeout()) {
                SetState(new NormalDriveImpl());
            }
        }

        public override String ToString()
        {
            return "ErrorEdge";
        }
    }
}
