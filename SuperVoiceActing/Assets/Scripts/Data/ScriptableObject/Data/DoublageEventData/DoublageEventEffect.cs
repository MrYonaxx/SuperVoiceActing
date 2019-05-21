/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
        [HideLabel]
        [HorizontalGroup("Key", Width = 145, LabelWidth = 100)]
        private EventEffect eventEffect;
        public EventEffect EventEffect
        {
            get { return eventEffect; }
        }

        [SerializeField]
        [HorizontalGroup("Key", Width = 100, LabelWidth = 100)]
        private bool active;
        public bool Active
        {
            get { return active; }
        }

        [SerializeField]
        [HorizontalGroup("Zey", Width = 100, LabelWidth = 100)]
        private int eventTime;
        public int EventTime
        {
            get { return eventTime; }
        }

        [SerializeField]
        [HorizontalGroup("Zey", Width = 100, LabelWidth = 100)]
        private int viewportID;
        public int ViewportID
        {
            get { return viewportID; }
        }



    } // DoublageEventEffect class
	
}// #PROJECTNAME# namespace
