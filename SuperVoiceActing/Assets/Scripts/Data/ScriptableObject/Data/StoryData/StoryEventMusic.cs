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
    /// Definition of the StoryEventMusic class
    /// </summary>
    [System.Serializable]
    public class StoryEventMusic : StoryEvent
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [HorizontalGroup("StoryMusic", LabelWidth = 70, Width = 50)]
        [SerializeField]
        bool playMusic = false;

        [HorizontalGroup("StoryMusic")]
        [SerializeField]
        [HideLabel]
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
        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            if (playMusic)
                AudioManager.Instance.PlayMusic(audio);
            else
                AudioManager.Instance.StopMusic();
            yield return null;
        }

        public override bool InstantNodeCoroutine()
        {
            return true;
        }



        #endregion

        } // StoryEventMusic class

} // #PROJECTNAME# namespace