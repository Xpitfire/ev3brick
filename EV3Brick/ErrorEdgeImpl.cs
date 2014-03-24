using System;

namespace PrgSps2Gr1
{
    class ErrorEdgeImpl : State
    {
        protected override void PerformAction()
        {
            if (Ev3.ReachedEdge())
            {
                Ev3.VehicleReverse(Ev3Control.TurnDirection.Left, 25, 90);
            }
            else
            {
                SetState(new NormalDriveImpl());
            }
        }

        public override String ToString()
        {
            return "ErrorEdge";
        }
    }
}
