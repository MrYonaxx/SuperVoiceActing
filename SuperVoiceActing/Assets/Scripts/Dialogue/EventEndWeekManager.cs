﻿/*****************************************************************
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


        List<int> randomEventList;

        private void Start()
        {
            //Créer une copie pour que si un random event ajoute un random event, ce random event ne sera pas joué la même semaine.
            randomEventList = new List<int>(playerData.NextRandomEvent.Count);
            for (int i = 0; i < playerData.NextRandomEvent.Count; i++)
                randomEventList.Add(playerData.NextRandomEvent[i]);

            if (storyEventManager.DebugData() == false)
                StartCoroutine(CheckEventEndWeek());
        }


        /*public void CheckEventEndWeek()
        {

        }*/

        public IEnumerator CheckEventEndWeek()
        {
            while (playerData.NextStoryEvents.Count != 0)
            {
                Debug.Log(playerData.NextStoryEvents[playerData.NextStoryEvents.Count - 1]);
                yield return storyEventManager.StartEvent(playerData.NextStoryEvents[playerData.NextStoryEvents.Count - 1]);
                playerData.NextStoryEvents.RemoveAt(playerData.NextStoryEvents.Count - 1);
            }
            while (randomEventList.Count != 0)
            {
                yield return storyEventManager.StartEvent(randomEventDatabase.GetRandomEvent(randomEventList[0]));
                playerData.NextRandomEvent.RemoveAt(0);
                randomEventList.RemoveAt(0);
            }
            SceneManager.LoadScene("Bureau", LoadSceneMode.Single);
        }

    } // EventEndWeekManager class
	
}// #PROJECTNAME# namespace
