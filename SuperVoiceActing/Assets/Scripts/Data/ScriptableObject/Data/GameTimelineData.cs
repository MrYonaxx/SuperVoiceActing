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

    /*[System.Serializable]
    public class GameTimelineContractData
    {
        [HorizontalGroup("Hey", PaddingLeft = 50)]
        [SerializeField]
        public ContractData[] addContracts;

        [HorizontalGroup("Hey", PaddingRight = 50)]
        [SerializeField]
        public ContractData[] removeContracts;
    }


    [System.Serializable]
    public class GameTimelineContractBox
    {
        [FoldoutGroup("$name")]
        [SerializeField]
        [HideLabel]
        public GameTimelineContractData contractsData;

        string name;

        public void SetTitle(int i)
        {
            name = "Semaine " + i;
            if (contractsData.addContracts != null)
            {
                if (contractsData.addContracts.Length != 0)
                {
                    name += " | Contrat " + contractsData.addContracts.Length;
                }
            }
        }
    }*/






    [System.Serializable]
    public class GameTimelineEventtData
    {

        [HorizontalGroup("Hey", PaddingLeft = 50)]
        [SerializeField]
        public StoryEventData[] eventsPhone;

        [HorizontalGroup("Hey")]
        [SerializeField]
        public StoryEventData[] eventStartWeek;

        [HorizontalGroup("Hey", PaddingRight = 50)]
        [SerializeField]
        public StoryEventData[] eventEndWeek;
    }


    [System.Serializable]
    public class GameTimelineEventBox
    {
        [FoldoutGroup("$name")]
        [SerializeField]
        [HideLabel]
        public GameTimelineEventtData eventData;

        string name;

        public void SetTitle(int i)
        {
            name = "Semaine " + i;
            if (eventData.eventsPhone != null)
            {
                if (eventData.eventsPhone.Length != 0)
                {
                    name += " | Phone " + eventData.eventsPhone.Length;
                }
            }
            if (eventData.eventStartWeek != null)
            {
                if (eventData.eventStartWeek.Length != 0)
                {
                    name += " | Start " + eventData.eventStartWeek.Length;
                }
            }
            if (eventData.eventEndWeek != null)
            {
                if (eventData.eventEndWeek.Length != 0)
                {
                    name += " | End " + eventData.eventEndWeek.Length;
                }
            }
        }
    }


    [System.Serializable]
    [CreateAssetMenu(fileName = "TimelineEventData", menuName = "TimelineEvents", order = 1)]
    public class GameTimelineData : ScriptableObject
	{

        /*[TabGroup("RandomContract")]
        [Space]
        [Title("Contrat ajoutés à la liste de contrat pouvant apparaitre dans les contrats disponibles")]
        [SerializeField]
        private GameTimelineContractBox[] contractRandomTimeline;
        public GameTimelineContractBox[] ContractRandomTimeline
        {
            get { return contractRandomTimeline; }
        }*/

        //[TabGroup("Events")]
        [Space]
        [Title("Event")]
        [SerializeField]
        private GameTimelineEventBox[] eventsTimeline;
        public GameTimelineEventBox[] EventsTimeline
        {
            get { return eventsTimeline; }
        }


        [Button]
        private void SetTitles()
        {

            /*for (int i = 0; i < contractRandomTimeline.Length; i++)
            {
                contractRandomTimeline[i].SetTitle(i);
            }*/


            for (int i = 0; i < eventsTimeline.Length; i++)
            {
                eventsTimeline[i].SetTitle(i);
            }
        }







        /*public void CheckContractTimeline(PlayerData playerData)
        {
            for (int i = 0; i < ContractRandomTimeline[playerData.Date.week].contractsData.addContracts.Length; i++)
            {
                playerData.ContractGacha.Add(ContractRandomTimeline[playerData.Date.week].contractsData.addContracts[i].name);
            }

        }*/

        public void CheckEventsTimeline(PlayerData playerData)
        {
            for (int i = 0; i < EventsTimeline[playerData.Date.week].eventData.eventsPhone.Length; i++)
            {
                playerData.PhoneStoryEvents.Add(EventsTimeline[playerData.Date.week].eventData.eventsPhone[i]);
            }

            for (int i = 0; i < EventsTimeline[playerData.Date.week].eventData.eventStartWeek.Length; i++)
            {
                playerData.NextStoryEventsStartWeek.Add(EventsTimeline[playerData.Date.week].eventData.eventStartWeek[i]);
            }

            for (int i = 0; i < EventsTimeline[playerData.Date.week].eventData.eventEndWeek.Length; i++)
            {
                playerData.NextStoryEvents.Add(EventsTimeline[playerData.Date.week].eventData.eventEndWeek[i]);
            }
        }


    } // GameTimelineData class
	
}// #PROJECTNAME# namespace
