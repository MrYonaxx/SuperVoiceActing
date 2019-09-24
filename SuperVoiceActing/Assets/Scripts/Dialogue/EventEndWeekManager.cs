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
        [Sirenix.OdinInspector.Title("Database")]
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        RandomEventDatabase randomEventDatabase;

        [Sirenix.OdinInspector.Title("StoryEventManager")]
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
            else if (playerData.NextRandomEvent.Count != 0)
            {
                storyEventManager.StartStoryEventDataWithScene(randomEventDatabase.GetRandomEvent(playerData.NextRandomEvent[0]));
                playerData.NextRandomEvent.RemoveAt(0);
            }
            else
            {
                SceneManager.LoadScene("Bureau", LoadSceneMode.Single);
            }
        }

    } // EventEndWeekManager class
	
}// #PROJECTNAME# namespace
