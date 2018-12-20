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
    /// Definition of the CharacterDialogueController class
    /// </summary>
    public class CharacterDialogueController : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        StoryCharacterData storyCharacterData;

        [SerializeField]
        SpriteRenderer spriteRenderer;

        [SerializeField]
        Yeux eyesScript;

        [SerializeField]
        MouthAnimation mouth;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public StoryCharacterData GetStoryCharacterData()
        {
            return storyCharacterData;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public void ActivateMouth()
        {
            mouth.ActivateMouth();
        }

        public void StopMouth()
        {
            mouth.DesactivateMouth();
        }
        
        #endregion

    } // CharacterDialogueController class

} // #PROJECTNAME# namespace