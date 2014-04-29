﻿using Sps2Gr1.InTeam.State.Init;
using Sps2Gr1.InTeam.State.Master;
using Sps2Gr1.InTeam.State.Normal;
using Sps2Gr1.InTeam.State.Error;
using Sps2Gr1.InTeam.State.Test;

namespace Sps2Gr1.InTeam.State
{
    internal class StateTypeConstants
    {
        /// <summary>
        /// This method is used to use different startup sequences, depending on the 
        /// declared state name string.
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static AState InitializeStartup(string stateName, StateController controller)
        {
            AState aState = null;
            // convert the state name (string) to a new state object
            switch (stateName)
            {
                case InitImpl.Name:
                    aState = new InitImpl(controller);
                    break;
                case TestDriveImpl.Name:
                    aState = new TestDriveImpl(controller);
                    break;
            }
            return aState;
        }

        /// <summary>
        /// This method is used to convert a state name to a new state object.
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public static AState ConvertState(string stateName)
        {
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
            return aState;
        }

    }
}