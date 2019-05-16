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
        Shake,
        BigShake,
        Negative,
        BlurredVision,
        FadeTotal,
        ShakePlayer,
        Tint,
        FadeOut
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
        private bool active;
        public bool Active
        {
            get { return active; }
        }

        [SerializeField]
        private int eventTime;
        public int EventTime
        {
            get { return eventTime; }
        }

        [SerializeField]
        private int viewportID;
        public int ViewportID
        {
            get { return viewportID; }
        }



    } // DoublageEventEffect class
	
}// #PROJECTNAME# namespace
