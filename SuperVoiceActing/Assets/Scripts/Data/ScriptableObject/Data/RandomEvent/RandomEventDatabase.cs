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
    public class RandomEventStoryDatabase
    {
        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        public StoryEventData randomStoryEvent;

        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        public RandomEventCondition randomStoryEventCondition;

    }



    [CreateAssetMenu(fileName = "RandomEventDatabase", menuName = "RandomEvent/RandomEventDatabase", order = 1)]
    public class RandomEventDatabase : ScriptableObject
	{
        [TabGroup("RandomStoryEvents")]
        [SerializeField]
        RandomEventStoryDatabase[] randomStoryEvents;



        public List<int> SelectRandomEventAvailable(PlayerData playerData)
        {
            List<int> randomEventAvailable = new List<int>();
            for(int i = 0; i < randomStoryEvents.Length; i++)
            {
                if(randomStoryEvents[i].randomStoryEvent == null)
                {
                    continue;
                }
                if (randomStoryEvents[i].randomStoryEventCondition != null)
                {
                    if (randomStoryEvents[i].randomStoryEventCondition.CheckCondition(playerData) == true)
                        randomEventAvailable.Add(i);
                }
                else
                    randomEventAvailable.Add(i);
            }
            return randomEventAvailable;

        }


        public void AddRandomEvent(PlayerData playerData)
        {
            List<int> randomEventAvailable = SelectRandomEventAvailable(playerData);
            int number = Random.Range(0, 12);
            if (number <= 5)
                number = 0;
            else if (number <= 10)
                number = 1;
            else
                number = 2;

            for(int i = 0; i < number; i++)
            {
                if (randomEventAvailable.Count == 0)
                    return;
                int rand = Random.Range(0, randomEventAvailable.Count);
                playerData.NextRandomEvent.Add(randomEventAvailable[rand]);
                randomEventAvailable.RemoveAt(i);
            }
        }

        public StoryEventData GetRandomEvent(int index)
        {
            return randomStoryEvents[index].randomStoryEvent;
        }

    } // RandomEventDatabase class
	
}// #PROJECTNAME# namespace
