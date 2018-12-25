/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageEventWait class
    /// </summary>

    [System.Serializable]
    public class DoublageEventWait : DoublageEvent
    {
        
        [SerializeField]
        private float wait;
        public float Wait
        {
            get { return wait; }
        }


    } // DoublageEventWait class

} // #PROJECTNAME# namespace