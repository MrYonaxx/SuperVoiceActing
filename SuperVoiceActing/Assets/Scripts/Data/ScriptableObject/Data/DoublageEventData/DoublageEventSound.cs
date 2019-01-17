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
    /// Definition of the DoublageEventSound class
    /// </summary>
    /// 
    [System.Serializable]
    public class DoublageEventSound : DoublageEvent
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private AudioClip audio;
        public AudioClip Audio
        {
            get { return audio; }
            set { audio = value; }
        }

        
        #endregion


    } // DoublageEventSound class

} // #PROJECTNAME# namespace