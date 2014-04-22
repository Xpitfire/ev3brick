using System.IO;
using System.Threading;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.Debug;
using PrgSps2Gr1.State.Error;
using PrgSps2Gr1.State.Master;
using PrgSps2Gr1.State.Normal;

namespace PrgSps2Gr1.State
{
    abstract class AState : IDebug
    {
        private static Ev3ControlImpl _ev3;
        private static ProgramEv3Sps2Gr1 _controller;
        
        protected Ev3ControlImpl Ev3
        {
            get { return _ev3 ?? (_ev3 = new Ev3ControlImpl()); }
        }

        /// <summary>
        /// The program controller with the current used AState instance.
        /// </summary>
        protected ProgramEv3Sps2Gr1 Controller
        {
            set
            {
                if (value == null) throw new InvalidDataException("Controller == null!");
                Ev3.WriteLine("Set new controller!");
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
            Ev3.EscapeReleasedButtonEvent += () => EventQueue.State.Enqueue(MasterExitImpl.Name);
            Ev3.EnterReleasedButtonEvent += handlePauseAndResume;
            Ev3.ReachedEdgeEvent += () => EventQueue.State.Enqueue(ErrorEdgeImpl.Name);
        }

        /// <summary>
        /// Handle how to interpret pause button interaction.
        /// </summary>
        private void handlePauseAndResume() 
        {
            if ((_controller != null) && !(_controller.ProgramAState is MasterPauseImpl))
            {
                EventQueue.State.Enqueue(MasterPauseImpl.Name);
            }
            else if (EventQueue.LastState != null)
            {
                EventQueue.State.Enqueue(EventQueue.LastState);
            }
        }

        /// <summary>
        /// Mothod to override from the implementing sub-classes.
        /// </summary>
        protected abstract void PerformAction();

        public abstract void Log();

        public abstract object[] Debug(object[] args);

        /// <summary>
        /// Sets a new AState instance in the controller.
        /// </summary>
        /// <param name="newAState"></param>
        private void SetState(AState newAState)
        {
            if (Equals(newAState)) return;
			Ev3.WriteLine("StateChanged: " + newAState);
            EventQueue.LastState = _controller.ProgramAState.ToString();
            _controller.ProgramAState = newAState;
        }

        /// <summary>
        /// Update sequence called by the controller.
        /// </summary>
        public void Update()
        {
			// dequeue events from a primary event queue until it's empty and then check the secondary and tertiary
			while (EventQueue.State.Count <= 0) 
			{
                if (EventQueue.Command.Count > 0)
                {
                    EventQueue.Command.Dequeue()();
                }
                Thread.Sleep(100);
                // perform sub-class default action
                PerformAction();
			}

            // set the next AState by converting the AState name queue entry
            var stateName = EventQueue.State.Dequeue();
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
