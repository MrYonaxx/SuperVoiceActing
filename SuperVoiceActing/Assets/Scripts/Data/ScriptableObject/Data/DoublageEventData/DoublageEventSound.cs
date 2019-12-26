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
        public bool Music
        {
            get { return music; }
        }

        [HideIf("music", true)]
        [SerializeField]
        private AudioClip soundClip;
        public AudioClip SoundClip
        {
            get { return soundClip; }
        }

        [ShowIf("music", true)]
        [SerializeField]
        private bool stopMusic;
        public bool StopMusic
        {
            get { return stopMusic; }
        }

        [ShowIf("music", true)]
        [SerializeField]
        private AudioClip musicClip;
        public AudioClip MusicClip
        {
            get { return musicClip; }
        }

        [ShowIf("music", true)]
        [SerializeField]
        private int timeTransition;
        public int TimeTransition
        {
            get { return timeTransition; }
        }


        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            if (music == true)
            {
                if (stopMusic == true)
                {
                    AudioManager.Instance.StopMusic(timeTransition);
                }
                else
                {
                    AudioManager.Instance.PlayMusic(musicClip, timeTransition);
                }
            }
            else
            {
                AudioManager.Instance.PlaySound(soundClip);
            }

            yield break;
        }


        #endregion


    } // DoublageEventSound class

} // #PROJECTNAME# namespace