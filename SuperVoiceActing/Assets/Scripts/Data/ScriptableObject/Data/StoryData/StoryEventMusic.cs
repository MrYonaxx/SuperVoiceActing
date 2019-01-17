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
    /// Definition of the StoryEventMusic class
    /// </summary>
    [System.Serializable]
    public class StoryEventMusic : StoryEvent
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        bool playMusic = false;
        [SerializeField]
        AudioClip audio = null;


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */


        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        protected override IEnumerator StoryEventCoroutine()
        {
            if (playMusic)
                AudioManager.Instance.PlayMusic(audio);
            else
                AudioManager.Instance.StopMusic();
            yield return null;
        }


        #endregion

        } // StoryEventMusic class

} // #PROJECTNAME# namespace