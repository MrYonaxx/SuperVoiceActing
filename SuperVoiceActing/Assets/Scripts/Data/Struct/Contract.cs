/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the Contract class
    /// A contract is created from a ContractData
    /// ContractData contains all possibilities for a Contract
    /// A contract object is a possibility of ContractData
    /// </summary>
    public class Contract
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        
        [SerializeField]
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField]
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [SerializeField]
        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [SerializeField]
        private int money;
        public int Money
        {
            get { return money; }
            set { money = value; }
        }


        [SerializeField]
        private int sessionNumber;
        public int SessionNumber
        {
            get { return sessionNumber; }
            set { sessionNumber = value; }
        }
        [SerializeField]
        private int weekRemaining;
        public int WeekRemaining
        {
            get { return weekRemaining; }
            set { weekRemaining = value; }
        }


        [SerializeField]
        private int currentLine;
        public int CurrentLine
        {
            get { return currentLine; }
            set { currentLine = value; }
        }
        [SerializeField]
        private int totalLine;
        public int TotalLine
        {
            get { return totalLine; }
            set { totalLine = value; }
        }


        [SerializeField]
        private int currentMixing;
        public int CurrentMixing
        {
            get { return currentMixing; }
            set { currentMixing = value; }
        }
        [SerializeField]
        private int totalMixing;
        public int TotalMixing
        {
            get { return totalMixing; }
            set { totalMixing = value; }
        }


        [SerializeField]
        private List<Role> characters = new List<Role>();
        public List<Role> Characters
        {
            get { return characters; }
            set { characters = value; }
        }
        [SerializeField]
        private List<VoiceActor> voiceActors = new List<VoiceActor>();
        public List<VoiceActor> VoiceActors
        {
            get { return voiceActors; }
            set { voiceActors = value; }
        }

        [SerializeField]
        private List<TextData> textData = new List<TextData>();
        public List<TextData> TextData
        {
            get { return textData; }
        }

        [SerializeField]
        private DoublageEventData[] eventData;
        public DoublageEventData[] EventData
        {
            get { return eventData; }
        }

        [SerializeField]
        private int expGain;
        public int ExpGain
        {
            get { return expGain; }
            set { expGain = value; }
        }

        [SerializeField]
        private int expBonus;
        public int ExpBonus
        {
            get { return expBonus; }
            set { expBonus = value; }
        }

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

        public Contract()
        {
            name = "Salut";
        }

        public Contract(string name)
        {
            this.name = name;
        }

        public Contract(ContractData data)
        {
            if (data.ContractTitle.Length == 0)
                this.name = data.Name;
            else
                this.name = data.ContractTitle[Random.Range(0, data.ContractTitle.Length)];

            if (data.Description.Length == 0)
                this.description = " ";
            else
                this.description = data.Description[Random.Range(0, data.Description.Length)];

            this.level = data.Level;
            this.money = data.SalaryMin + (Random.Range(0, (data.SalaryMax - data.SalaryMin) / 10) * 10); // Renvoie toujours une valeur arrondit a la dizaine
            this.weekRemaining = Random.Range(data.WeekMin, data.WeekMax+1);
            this.totalMixing = Random.Range(data.MixingMin, data.MixingMax+1);

            this.expGain = data.ExpGain;
            this.expBonus = data.ExpBonus;

            this.eventData = data.EventData;

            // Select Characters -------------------------------------------------------
            for (int i = 0; i < data.Characters.Length; i++)
            {
                if (data.Characters[i].Optional == true)
                {
                    if(Random.Range(0f,1f) >= 0.5f)
                    {
                        int selectProfil = Random.Range(0, data.Characters[i].CharactersProfil.Length);
                        characters.Add(new Role(data.Characters[i].CharactersProfil[selectProfil]));
                    }
                }
                else
                {
                    int selectProfil = Random.Range(0, data.Characters[i].CharactersProfil.Length);
                    characters.Add(new Role(data.Characters[i].CharactersProfil[selectProfil]));
                }
            }


            voiceActors = new List<VoiceActor>(characters.Count);
            for (int i = 0; i < characters.Count; i++)
            {
                voiceActors.Add(null);
            }

            // Select TextData -------------------------------------------------------

            for (int i = 0; i < data.TextDataContract.Length; i++)
            {
                if (data.TextDataContract[i].TextDataCondition.All == true)
                {
                    // Selection all
                    for (int j = 0; j < data.TextDataContract[i].TextDataPossible.Length; j++)
                    {
                        textData.Add(new TextData(data.TextDataContract[i].TextDataPossible[j]));
                    }
                }
                else
                {
                    // Selection aléatoire
                    int nbSelection = Random.Range(data.TextDataContract[i].TextDataCondition.NbPhraseMin, data.TextDataContract[i].TextDataCondition.NbPhraseMax + 1);
                    if (nbSelection > 0)
                    {
                        // Copie des possibilité de text Data
                        List<TextDataContract> listTextDataPossibilities = new List<TextDataContract>();
                        for (int j = 0; j < data.TextDataContract[i].TextDataPossible.Length; j++)
                        {
                            listTextDataPossibilities.Add(data.TextDataContract[i].TextDataPossible[j]);
                        }
                        // Selection
                        for (int n = 0; n < nbSelection; n++)
                        {
                            int indexRandom = Random.Range(0, listTextDataPossibilities.Count);
                            textData.Add(new TextData(listTextDataPossibilities[indexRandom]));
                            Debug.Log(listTextDataPossibilities[indexRandom].Text);
                            listTextDataPossibilities.RemoveAt(indexRandom);
                        }
                    }
                }
            }
            totalLine = textData.Count;

        }




        /*private string ReplaceText()
        {

        }*/

        #endregion

    } // Contract class

} // #PROJECTNAME# namespace