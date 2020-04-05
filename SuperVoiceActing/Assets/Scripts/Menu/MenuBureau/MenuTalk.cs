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

    [System.Serializable]
    public class StoryCondition
    {
        [HorizontalGroup("StoryCondition")]
        [SerializeField]
        VariableCondition[] variableConditions;

        [HorizontalGroup("StoryCondition")]
        [SerializeField]
        [HideLabel]
        StoryEventData storyEvent;
    }

    public class MenuTalk: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Transform panelCharacter;
        [SerializeField]
        CharacterDialogueController characterPrefab;

        [SerializeField]
        StoryEventManager storyManager;


        [SerializeField]
        StoryCondition[] storyConditions;




        StoryEventData currentEvent;

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


        public void SelectCurrentEvent()
        {
            for(int i = 0; i < storyConditions.Length; i++)
            {

            }
        } 


        public void CreateScene()
        {
            //variableCondition.CheckCondition
        }

        public void Talk()
        {
            StartCoroutine(TalkCoroutine());
        }

        private IEnumerator TalkCoroutine()
        {
            yield return storyManager.StartEvent(currentEvent);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace