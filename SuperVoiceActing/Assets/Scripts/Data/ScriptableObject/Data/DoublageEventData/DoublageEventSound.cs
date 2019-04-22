/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

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
        private bool music = false;

        [HideIf("music", true)]
        [SerializeField]
        private AudioClip soundClip;
        public AudioClip SoundClip
        {
            get { return soundClip; }
        }

        [ShowIf("music", true)]
        [SerializeField]
        private AudioClip musicClip;
        public AudioClip MusicClip
        {
            get { return musicClip; }
        }


        #endregion


    } // DoublageEventSound class

} // #PROJECTNAME# namespace