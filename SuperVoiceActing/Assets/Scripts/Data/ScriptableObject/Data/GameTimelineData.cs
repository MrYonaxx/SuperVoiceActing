﻿/*****************************************************************
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
    }


    [System.Serializable]
    [CreateAssetMenu(fileName = "TimelineEventData", menuName = "TimelineEvents", order = 1)]
    public class GameTimelineData : ScriptableObject
	{
        [TabGroup("Contract")]
        [Space]
        [Title("Contrat ajoutés directement aux contrats disponibles")]
        [SerializeField]
        private GameTimelineContractBox[] contractTimeline;
        public GameTimelineContractBox[] ContractTimeline
        {
            get { return contractTimeline; }
        }


        [TabGroup("RandomContract")]
        [Space]
        [Title("Contrat ajoutés à la liste de contrat pouvant apparaitre dans les contrats disponibles")]
        [SerializeField]
        private GameTimelineContractBox[] contractRandomTimeline;
        public GameTimelineContractBox[] ContractRandomTimeline
        {
            get { return contractRandomTimeline; }
        }

        [TabGroup("Events")]
        [Space]
        [Title("Event")]
        [SerializeField]
        private GameTimelineContractData[] eventsTimeline;

        /*[TabGroup("VoiceActors")]
        [Space]
        [Title("Comédiens")]
        [SerializeField]
        private GameTimelineContractData[] voiceActorTimeline;*/



        [Button]
        private void SetTitles()
        {
            for (int i = 0; i < contractTimeline.Length; i++)
            {
                contractTimeline[i].SetTitle(i);
            }

            for (int i = 0; i < contractRandomTimeline.Length; i++)
            {
                contractRandomTimeline[i].SetTitle(i);
            }
        }


        /*public ContractData[] GetContracts(int week)
        {
            if(contractTimeline[week].addContracts.addContracts != null)
            {
                if (contractTimeline[week].addContracts.addContracts.Length != 0)
                    return contractTimeline[week].addContracts.addContracts;

            }
            return null;
        }*/


    } // GameTimelineData class
	
}// #PROJECTNAME# namespace
