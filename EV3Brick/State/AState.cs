using System;
using System.IO;
using System.Threading;
using SPSGrp1Grp2.Cunt.Debug;
using SPSGrp1Grp2.Cunt.Control;
using SPSGrp1Grp2.Cunt.State.Init;
using SPSGrp1Grp2.Cunt.State.Master;
using SPSGrp1Grp2.Cunt.State.Normal;
using SPSGrp1Grp2.Cunt.Logging;
using SPSGrp1Grp2.Cunt.State.Error;
using SPSGrp1Grp2.Cunt.Utility;

namespace SPSGrp1Grp2.Cunt.State
{
    abstract class AState : IDebug
    {
        #region Static members

        /// <summary>
        /// Controller instance, which holds the current operated state.
        /// </summary>
        private static StateController _controller;

        /// <summary>
        /// The default state event queue to process commands and state changes.
        /// </summary>
        private static EventQueue _eventQueue;

        protected static IDeviceControl Ev3
        {
            get { return DeviceControlFactory.Ev3Control; }
        }

        protected static EventQueue StateEventQueue
        {
            get { return _eventQueue ?? (_eventQueue = new EventQueue(_controller)); }
        }

        /// <summary>
        /// The program controller with the current used AState instance.
        /// </summary>
        protected static StateController Controller
        {
            set
            {
                if (value == null) throw new InvalidDataException("Controller == null!");
                Logger.Log("Set State controller");
                _controller = value;
            }
            get { return _controller; }
        }
        
        /// <summary>
        /// Static AState constructor to inistialize the base class functionality.
        /// </summary>
        static AState()
        {
            // add anonymous method action to event queue
            Ev3.EscapeReleasedButtonEvent += EscapeButton;
            Ev3.EnterReleasedButtonEvent += EnterButton;
            Ev3.UpReleasedButtonEvent += UpButton;
            Ev3.DownReleasedButtonEvent += DownButton;
            Ev3.LeftReleasedButtonEvent += LeftButton;
            Ev3.RightReleasedButtonEvent += RightButton;
            Ev3.ReachedEdgeEvent += ReachedEdgeOrObjectDetected;
            Ev3.IdentifiedEnemyEvent += IdentifiedEnemy;
            Ev3.DetectedObjectEvent += DetectedObject;
        }

        private static void EscapeButton()
        {
            Logger.Log("CMD: Exit ...");
            var cmd = new Command();
            cmd.SetAction(() => StateEventQueue.EnqueueState(MasterExitImpl.Name));
            cmd.SetCommandLevel(EventQueue.StateLevel.Level1);
            StateEventQueue.EnqueueCommand(cmd);
        }

        private static void EnterButton()
        {
            Logger.Log("CMD: Pause ...");
            var cmd = new Command();
            cmd.SetAction(() => StateEventQueue.EnqueueState(MasterPauseImpl.Name));
            cmd.SetCommandLevel(EventQueue.StateLevel.Level1);
            StateEventQueue.EnqueueCommand(cmd);
        }

        private static void UpButton()
        {
            Logger.Log("CMD: Scanning Color ...");
            // ATTENTION: color scan blocks until a valid color has been found!
            // Here it is not allowed to use the command queue and to enqueue
            // this event, because it would block the state machine update
            // cycle, due to its blocking implementation.
            Ev3.InitColor();
        }

        private static void DownButton()
        {
            Logger.Log("CMD: Goto NormalSearch ...");
            var cmd = new Command();
            cmd.SetAction(() => StateEventQueue.EnqueueState(NormalSearchImpl.Name));
            cmd.SetCommandLevel(EventQueue.StateLevel.Level1);
            StateEventQueue.EnqueueCommand(cmd);
        }

        private static void LeftButton()
        {
            Logger.Log("CMD: Goto Init ...");
            var cmd = new Command();
            cmd.SetAction(() => StateEventQueue.EnqueueState(InitImpl.Name));
            cmd.SetCommandLevel(EventQueue.StateLevel.Level1);
            StateEventQueue.EnqueueCommand(cmd);
        }

        private static void RightButton()
        {
            Logger.Log("CMD: Goto NormalFlee ...");
            var cmd = new Command();
            cmd.SetAction(() => StateEventQueue.EnqueueState(NormalFleeImpl.Name));
            cmd.SetCommandLevel(EventQueue.StateLevel.Level1);
            StateEventQueue.EnqueueCommand(cmd);
        }

        private static void DetectedObject()
        {
            // do not use logging here, because it would flood the EV3 screen
            // with logging events
            var cmd = new Command();
            cmd.SetAction(() => StateEventQueue.EnqueueState(NormalFollowImpl.Name));
            cmd.SetCommandLevel(EventQueue.StateLevel.Level3);
            StateEventQueue.EnqueueCommand(cmd);
            cmd = new Command();
            cmd.SetAction(() => Ev3.VehicleAdjust());
            cmd.SetCommandLevel(EventQueue.StateLevel.Level3);
            StateEventQueue.EnqueueCommand(cmd);
        }

        private static void ReachedEdgeOrObjectDetected()
        {
            Logger.Log("CMD: Edge or Identify ...");
            var cmd = new Command();
            if (Ev3.HasLostObject())
            {
                cmd.SetAction(() => StateEventQueue.EnqueueState(ErrorEdgeImpl.Name));
            }
            else
            {
                cmd.SetAction(() => StateEventQueue.EnqueueState(NormalIdentifyImpl.Name));
            }
            cmd.SetCommandLevel(EventQueue.StateLevel.Level3);
            StateEventQueue.EnqueueCommand(cmd);
        }

        private static void IdentifiedEnemy()
        {
            // do not use logging here, because it would flood the EV3 screen
            // with logging events
            var cmd = new Command();
            cmd.SetAction(() => StateEventQueue.EnqueueState(NormalFoundImpl.Name));
            cmd.SetCommandLevel(EventQueue.StateLevel.Level2);
            StateEventQueue.EnqueueCommand(cmd);
        }

        #endregion

        #region State Update Methods

        /// <summary>
        /// This timer is used to process time delayed operations.
        /// </summary>
        protected Ev3Timer StateTimer = new Ev3Timer();

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
        /// Returns the state level of the current state.
        /// </summary>
        /// <returns></returns>
        public abstract int GetStateLevel();
        
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
            // save current state to StateEventQueue.LastState only if the new state is a different one
            if (_controller != null && _controller.CurrentState != null &&
                (StateEventQueue.LastState == null || (StateEventQueue.LastState != _controller.CurrentState.ToString())))
            {
                StateEventQueue.LastState = _controller.CurrentState.ToString();
            }
            // set new state to the controller
            if (_controller == null)
            {
                throw new Exception("Invalid state behavior!");
            }
            Logger.Log("StateChanged: " + newAState);
            _controller.CurrentState = newAState;
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
			while (StateEventQueue.GetStateCount() <= 0) 
			{
                if (StateEventQueue.GetCommandCount() > 0)
                {
                    StateEventQueue.DequeueCommand().PerformAction();
                }
                Thread.Sleep(100);
                // perform sub-class default action
                PerformRecurrentAction();
			}

            // if the state queue is empty return
            if (StateEventQueue.GetStateCount() <= 0)
            {
                return;
            }
            // clear previous commands from the command queue
            StateEventQueue.ClearCommandQueue();

            // set the next AState by converting the AState name queue entry
            var stateName = StateEventQueue.DequeueState();
            // convert state name to a state object
            var aState = StateTypeConstants.ConvertState(stateName);

            if (aState != null)
            {
                // set the new state to the controller
                SetState(aState);
                // invoke the single action after state change
                aState.PerformSingleAction();
            }
            else
            {
                Logger.Log("STATE FAULT! aState == null");
            }
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
