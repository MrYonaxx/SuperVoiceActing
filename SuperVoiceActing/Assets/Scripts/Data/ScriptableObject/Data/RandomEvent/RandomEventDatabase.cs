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


    [System.Serializable]
    public class RandomEventEffectDatabase
    {
        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        public RandomEvent randomEvent;

        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        public RandomEventCondition randomEventCondition;

    }


    [CreateAssetMenu(fileName = "RandomEventDatabase", menuName = "RandomEvent/RandomEventDatabase", order = 1)]
    public class RandomEventDatabase : ScriptableObject
	{
        [TabGroup("RandomStoryEvents")]
        [SerializeField]
        RandomEventStoryDatabase[] randomStoryEvents;

        [TabGroup("RandomEffectEvents")]
        [SerializeField]
        RandomEventEffectDatabase[] randomEffectEvents;


        public StoryEventData GetStoryRandomEvent(PlayerData playerData)
        {
            int rand = Random.Range(0, randomStoryEvents.Length);
            if(randomStoryEvents[rand] != null)
            {
                if (randomStoryEvents[rand].randomStoryEventCondition != null)
                {
                    if (randomStoryEvents[rand].randomStoryEventCondition.CheckCondition(playerData) == true)
                    {
                        return randomStoryEvents[rand].randomStoryEvent;
                    }
                }
                else
                {
                    return randomStoryEvents[rand].randomStoryEvent;
                }

            }
            return null;
        }

        public RandomEvent GetRandomEvent()
        {
            return null;
        }

    } // RandomEventDatabase class
	
}// #PROJECTNAME# namespace
