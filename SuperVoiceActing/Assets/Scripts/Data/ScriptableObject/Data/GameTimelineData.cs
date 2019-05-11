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
        public GameTimelineContractData addContracts;

        string name;

        public void SetTitle(int i)
        {
            name = "Semaine " + i;
        }
    }


    [System.Serializable]
    [CreateAssetMenu(fileName = "TimelineEventData", menuName = "TimelineEvents", order = 1)]
    public class GameTimelineData : ScriptableObject
	{
        [TabGroup("Contract")]
        [Space]
        [Title("Contrat du scénario et quête annexe")]
        [SerializeField]
        private GameTimelineContractBox[] contractTimeline;

        [TabGroup("RandomContract")]
        [Space]
        [Title("Contrat Random")]
        [SerializeField]
        private GameTimelineContractBox[] contractRandomTimeline;

        [TabGroup("Events")]
        [Space]
        [Title("Event")]
        [SerializeField]
        private GameTimelineContractData[] eventsTimeline;

        [TabGroup("VoiceActors")]
        [Space]
        [Title("Comédiens")]
        [SerializeField]
        private GameTimelineContractData[] voiceActorTimeline;

        [Button]
        private void SetTitles()
        {
            for (int i = 0; i < contractTimeline.Length; i++)
            {
                contractTimeline[i].SetTitle(i);
            }

        }

    } // GameTimelineData class
	
}// #PROJECTNAME# namespace
