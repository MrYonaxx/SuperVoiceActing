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
        private List<TextDataContract> textData = new List<TextDataContract>();
        public List<TextDataContract> TextData
        {
            get { return textData; }
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
            this.name = data.Name;
            this.level = data.Level;
            this.money = data.SalaryMin + (Random.Range(0, data.SalaryMax - data.SalaryMin / 10) * 10); // Renvoie toujours une valeur arrondit a la dizaine
            this.weekRemaining = Random.Range(data.WeekMin, data.WeekMax);
            this.totalMixing = Random.Range(data.MixingMin, data.MixingMax);


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
                int nbSelection = Random.Range(data.TextDataContract[i].NbPhraseMin, data.TextDataContract[i].NbPhraseMax);
                if (nbSelection > 0)
                {
                    // Copie des possibilité de text Data
                    List<TextDataContract> listTextDataPossibilities = new List<TextDataContract>(3);
                    for (int j = 0; j < data.TextDataContract[i].TextDataPossible.Length; j++)
                    {
                        listTextDataPossibilities.Add(data.TextDataContract[i].TextDataPossible[j]);
                    }
                    // Selection
                    for (int n = 0; n < nbSelection; n++)
                    {
                        int indexRandom = Random.Range(0, listTextDataPossibilities.Count);
                        textData.Add(listTextDataPossibilities[indexRandom]);
                        Debug.Log(listTextDataPossibilities[indexRandom].Text);
                        listTextDataPossibilities.RemoveAt(indexRandom);
                    }
                }
            }
            totalLine = textData.Count;

        }

        #endregion

    } // Contract class

} // #PROJECTNAME# namespace