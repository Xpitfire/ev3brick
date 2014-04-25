using System;
using System.IO;
using System.Threading;
using PrgSps2Gr1.Control;
using PrgSps2Gr1.Control.Impl;
using PrgSps2Gr1.Debug;
using PrgSps2Gr1.Logging;
using PrgSps2Gr1.State.Error;
using PrgSps2Gr1.State.Master;
using PrgSps2Gr1.State.Normal;
using PrgSps2Gr1.Utility;

namespace PrgSps2Gr1.State
{
    abstract class AState : IDebug
    {
        /// <summary>
        /// This timer is used to process time delayed operations.
        /// </summary>
        protected Ev3Timer StateTimer = new Ev3Timer();

        /// <summary>
        /// Controller instance, which holds the current operated state.
        /// </summary>
        private static ProgramEv3Sps2Gr1 _controller;

        #region Inheritable Properties

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
        
        #endregion

        /// <summary>
        /// AState constructor to inistialize the base class instances.
        /// </summary>
        protected AState()
        {
            // add anonymous method action to event queue
            Ev3.EscapeReleasedButtonEvent += () => EventQueue.EnqueueState(MasterExitImpl.Name);
            Ev3.EnterReleasedButtonEvent += () => EventQueue.EnqueueState(MasterPauseImpl.Name);
            Ev3.ReachedEdgeEvent += ReachedEdgeOrObjectDetected;
            Ev3.IdentifyObjectEvent += () => EventQueue.EnqueueState(NormalIdentifyImpl.Name);
            Ev3.DetectedObjectEvent += () => EventQueue.EnqueueState(NormalFollowImpl.Name);
        }

        private void ReachedEdgeOrObjectDetected()
        {
            EventQueue.EnqueueState(Ev3.HasLostObject() ? ErrorEdgeImpl.Name : NormalIdentifyImpl.Name);
        }

        #region State Update Methods

        /// <summary>
        /// Mothod which has to be implemented by all inheriting state sub-classes, to perform
        /// polling specific behavior. This method is permanently called by an external 
        /// state controller.
        /// </summary>
        protected abstract void PerformRecurrentAction();

        /// <summary>
        /// Mothod which has to be implemented by all inheriting state sub-classes.
        /// It can be used to implement single event logic, which is beeing called
        /// on state change and the action occurred is added to the command queue for execution.
        /// </summary>
        protected abstract void PerformSingleAction();

        /// <summary>
        /// Sets a new AState instance to the controller. Also the old state of the controller will be added
        /// to the <code>LastState</code> object, if the new state differes from the old one.
        /// </summary>
        /// <param name="newAState"></param>
        private void SetState(AState newAState)
        {
            if (Equals(newAState))
            {
                return;
            }
            // save current state to EventQueue.LastState only if the new state is a different one
            if (_controller != null && _controller.ProgramAState != null &&
                (EventQueue.LastState == null || (EventQueue.LastState != _controller.ProgramAState.ToString())))
            {
                EventQueue.LastState = _controller.ProgramAState.ToString();
            }
            // set new state to the controller
            if (_controller == null)
            {
                throw new Exception("Invalid state behavior!");
            }
            Logger.Log("StateChanged: " + newAState);
            _controller.ProgramAState = newAState;
        }

        /// <summary>
        /// Update sequence called by the controller. Is based on two event queues. One is for the state
        /// machine behavior responsible and one for the command events commited by an current state.
        /// In general this method polls on the <code>PerformRecurrentAction</code> method implemented by
        /// the state sub-classes and dequeues the commands form an command queue, until a new state object 
        /// is available. A state change will clear the current actions from the command queue and
        /// repeat the previous sequences. 
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
                PerformRecurrentAction();
			}

            // if the state queue is empty return
            if (EventQueue.GetStateCount() <= 0)
            {
                return;
            }
            // clear previous commands from the command queue
            EventQueue.ClearCommandQueue();

            // set the next AState by converting the AState name queue entry
            var stateName = EventQueue.DequeueState();
            AState aState = null;
            // convert the state name (string) to a new state object
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
                case NormalFollowImpl.Name:
                    aState = new NormalFollowImpl();
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
            // set the new state to the controller
            SetState(aState);
            // invoke the single action after state change
            System.Diagnostics.Debug.Assert(aState != null, "aState != null");
            aState.PerformSingleAction();
        }

        #endregion

        // ----- default class implementations or inheritable methods -----

        public override bool Equals(object o)
        {
            return o != null && o is AState && (ToString() == o.ToString());
        }

        protected bool Equals(AState other)
        {
            return other != null && (ToString() == other.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public abstract object[] Debug(object[] args);
    }
}
