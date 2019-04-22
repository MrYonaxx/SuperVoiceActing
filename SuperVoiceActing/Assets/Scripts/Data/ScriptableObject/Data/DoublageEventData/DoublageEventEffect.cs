/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{

    public enum EventEffect
    {
        Flash,
        Negative,
        BlurredVision
    }

    [System.Serializable]
    public class DoublageEventEffect : DoublageEvent
	{
        [SerializeField]
        private EventEffect eventEffect;
        public EventEffect EventEffect
        {
            get { return eventEffect; }
        }

        [SerializeField]
        private int eventTime;
        public int EventTime
        {
            get { return eventTime; }
        }



    } // DoublageEventEffect class
	
}// #PROJECTNAME# namespace
