using System.IO;
using System.Threading;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.Debug;
using PrgSps2Gr1.Logging;
using PrgSps2Gr1.State.Error;
using PrgSps2Gr1.State.Master;
using PrgSps2Gr1.State.Normal;

namespace PrgSps2Gr1.State
{
    abstract class AState : IDebug
    {
        private static ProgramEv3Sps2Gr1 _controller;

        protected IDeviceControl Ev3
        {
            get { return DeviceControlFactory.Ev3Control; }
        }

        /// <summary>
        /// The program controller with the current used AState instance.
        /// </summary>
        protected ProgramEv3Sps2Gr1 Controller
        {
            set
            {
                if (value == null) throw new InvalidDataException("Controller == null!");
                Logger.Log("Set State controller");
                _controller = value;
            }
            get { return _controller; }
        }

        protected int DetectedObjectDegree
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// AState constructor to inistialize the base class instances.
        /// </summary>
        protected AState()
        {
            // add anonymous method action to event queue
            Ev3.EscapeReleasedButtonEvent += () => EventQueue.EnqueueState(MasterExitImpl.Name);
            Ev3.EnterReleasedButtonEvent += handlePauseAndResume;
            Ev3.ReachedEdgeEvent += () => EventQueue.EnqueueState(ErrorEdgeImpl.Name);
        }

        /// <summary>
        /// Handle how to interpret pause button interaction.
        /// </summary>
        private void handlePauseAndResume() 
        {
            if ((_controller != null) && !(_controller.ProgramAState is MasterPauseImpl))
            {
                EventQueue.EnqueueState(MasterPauseImpl.Name);
            }
            else if (EventQueue.LastState != null)
            {
                EventQueue.EnqueueState(EventQueue.LastState);
            }
        }

        /// <summary>
        /// Mothod to override from the implementing sub-classes.
        /// </summary>
        protected abstract void PerformAction();

        public abstract object[] Debug(object[] args);

        /// <summary>
        /// Sets a new AState instance in the controller.
        /// </summary>
        /// <param name="newAState"></param>
        private void SetState(AState newAState)
        {
            if (Equals(newAState)) return;
			Logger.Log("StateChanged: " + newAState);
            EventQueue.LastState = _controller.ProgramAState.ToString();
            _controller.ProgramAState = newAState;
        }

        /// <summary>
        /// Update sequence called by the controller.
        /// </summary>
        public void Update()
        {
			// dequeue events from a primary event queue until it's empty and then check the secondary and tertiary
			while (EventQueue.GetStateCount() <= 0) 
			{
                if (EventQueue.GetCommandCount() > 0)
                {
                    EventQueue.DequeueCommand()();
                }
                Thread.Sleep(100);
                // perform sub-class default action
                PerformAction();
			}

            // set the next AState by converting the AState name queue entry
            var stateName = EventQueue.DequeueState();
            AState aState = null;
            switch (stateName)
            {
                case ErrorEdgeImpl.Name:
                    aState = new ErrorEdgeImpl();
                    break;
                case MasterExitImpl.Name:
                    aState = new MasterExitImpl();
                    break;
                case MasterPauseImpl.Name:
                    aState = new MasterPauseImpl();
                    break;
                case NormalAdjustImpl.Name:
                    aState = new NormalAdjustImpl();
                    break;
                case NormalDriveImpl.Name:
                    aState = new NormalDriveImpl();
                    break;
                case NormalFoundImpl.Name:
                    aState = new NormalFoundImpl();
                    break;
                case NormalIdentifyImpl.Name:
                    aState = new NormalIdentifyImpl();
                    break;
                case NormalSearchImpl.Name:
                    aState = new NormalSearchImpl();
                    break;
            }
            SetState(aState);
        }

        // ----- default class implementation methods -----

        public override bool Equals(object o)
        {
            return o != null && Equals(ToString(), o.ToString());
        }

    }
}
