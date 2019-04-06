/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the PlayerData class
    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {

        [SerializeField]
        private bool reset = true;
        public bool Reset
        {
            get { return reset; }
        }

        [Space]
        [Space]
        [Space]
        [Space]
        [Space]
        [SerializeField]
        private int language;

        public int Language
        {
            get { return language; }
        }




        [HorizontalGroup("Date", LabelWidth = 50, Width = 50)]
        [SerializeField]
        private int week;
        public int Week
        {
            get { return week; }
        }

        [HorizontalGroup("Date", LabelWidth = 50, Width = 50 )]
        [SerializeField]
        private int month;
        public int Month
        {
            get { return month; }
        }

        [HorizontalGroup("Date", LabelWidth = 50, Width = 50)]
        [SerializeField]
        private int season;
        public int Season
        {
            get { return season; }
        }


        [HorizontalGroup("Date", LabelWidth = 50, Width = 50)]
        [SerializeField]
        private int year;
        public int Year
        {
            get { return year; }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        [Space]
        [Space]
        [Space]
        [Header("Battle Data")]
        [SerializeField]
        private int comboMax;
        public int ComboMax
        {
            get { return comboMax; }
            set { comboMax = value; }
        }

        [SerializeField]
        [HideLabel]
        private EmotionStat deck;
        public EmotionStat Deck
        {
            get { return deck; }
            set { deck = value; }
        }


        /////////////////////////////////////////////////////////////////////////////////// 
        [Space]
        [Space]
        [Space]
        [Header("Management Data")]
        [SerializeField]
        private List<ContractData> contractAvailableDebug = new List<ContractData>();

        [SerializeField]
        private List<VoiceActorData> voiceActorsDebug = new List<VoiceActorData>();


        // Contrat en cours à doubler
        //[SerializeField]
        private Contract currentContract;

        public Contract CurrentContract
        {
            get { return currentContract; }
            set { currentContract = value; }
        }

        // Contrat accepté
        [SerializeField]
        private List<Contract> contractAccepted;// = new List<Contract>();

        public List<Contract> ContractAccepted
        {
            get { return contractAccepted; }
            set { contractAccepted = value; }
        }

        // Contrat Disponible 
        [SerializeField]
        private List<Contract> contractAvailable;// = new List<Contract>();

        public List<Contract> ContractAvailable
        {
            get { return contractAvailable; }
            set { contractAvailable = value; }
        }

        // Liste des Acteurs
        [SerializeField]
        private List<VoiceActor> voiceActors;// = new List<VoiceActor>();

        public List<VoiceActor> VoiceActors
        {
            get { return voiceActors; }
            set { voiceActors = value; }
        }


        public void CreateList()
        {
            if(voiceActors == null && contractAvailable == null)
            {
                currentContract = null;

                voiceActors = new List<VoiceActor>(voiceActorsDebug.Count);
                for (int i = 0; i < voiceActorsDebug.Count; i++)
                {
                    voiceActors.Add(new VoiceActor(voiceActorsDebug[i]));
                }

                contractAvailable = new List<Contract>(contractAvailableDebug.Count);
                for (int i = 0; i < contractAvailableDebug.Count; i++)
                {
                    contractAvailable.Add(new Contract(contractAvailableDebug[i]));
                }

                contractAccepted = new List<Contract>(3);
                contractAccepted.Add(null);
                contractAccepted.Add(null);
                contractAccepted.Add(null);
            }
            Debug.Log(voiceActors[0].Name);

        }



    } // PlayerData class

} // #PROJECTNAME# namespace