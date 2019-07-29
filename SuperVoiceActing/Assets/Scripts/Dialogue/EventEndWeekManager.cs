/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VoiceActing
{
	public class EventEndWeekManager : MonoBehaviour
	{
        [SerializeField]
        PlayerData playerData;

        [SerializeField]
        StoryEventManager storyEventManager;


        private void Start()
        {
            if(storyEventManager.DebugData() == false)
                CheckEventEndWeek();
        }


        public void CheckEventEndWeek()
        {
            if(playerData.NextStoryEvents.Count != 0)
            {
                storyEventManager.StartStoryEventDataWithScene(playerData.NextStoryEvents[playerData.NextStoryEvents.Count-1]);
                playerData.NextStoryEvents.RemoveAt(playerData.NextStoryEvents.Count-1);
            }
            else
            {
                SceneManager.LoadScene("Bureau", LoadSceneMode.Single);
            }
        }

    } // EventEndWeekManager class
	
}// #PROJECTNAME# namespace
