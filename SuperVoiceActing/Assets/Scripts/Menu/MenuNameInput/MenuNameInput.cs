/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace VoiceActing
{
	public class MenuNameInput : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */


        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        TMPro.TMP_InputField inputField;
        [SerializeField]
        StoryEventData storyEventData;
        [SerializeField]
        string nextScene;

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

        public void RegisterPlayerName()
        {
            playerData.PlayerName = inputField.text;
            playerData.NextStoryEvents.Add(storyEventData);
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }

        public void RegisterStudioName()
        {
            playerData.StudioName = inputField.text;
            playerData.NextStoryEvents.Add(storyEventData);
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }

        #endregion

    } // MenuNameInput class
	
}// #PROJECTNAME# namespace
