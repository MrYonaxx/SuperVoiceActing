/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

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
        private VoiceActor[] characters;
        public VoiceActor[] Characters
        {
            get { return characters; }
            set { characters = value; }
        }
        [SerializeField]
        private VoiceActor[] voiceActors;
        public VoiceActor[] VoiceActors
        {
            get { return voiceActors; }
            set { voiceActors = value; }
        }

        [SerializeField]
        private TextData[] textData;
        public TextData[] TextData
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
            this.money = data.SalaryMin + (Random.Range(0, data.SalaryMax - data.SalaryMin / 10) * 10); // Renvoie toujours une valeur arrondit a la dizaine
            this.weekRemaining = Random.Range(data.WeekMin, data.WeekMax);
            this.totalMixing = Random.Range(data.MixingMin, data.MixingMax);

            // Select Characters
            for (int i = 0; i < data.Characters.Length; i++)
            {
                if (data.Characters[i].MainCharacter == true)
                {
                    //List<CharacterData>
                    int selectProfil = Random.Range(0, data.Characters[i].CharactersProfil.Length);
                    /*for (int j = 0; j < data.Characters[i].CharactersProfil.Length; j++)
                    {

                    }*/
                    //characters.Add
                }
            }
            // Select TextData

        }

        #endregion

    } // Contract class

} // #PROJECTNAME# namespace