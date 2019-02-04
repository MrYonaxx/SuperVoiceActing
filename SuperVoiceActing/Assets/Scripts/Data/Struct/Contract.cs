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
        private VoiceActor[] role;
        public VoiceActor[] Role
        {
            get { return role; }
            set { role = value; }
        }
        [SerializeField]
        private VoiceActor[] actors;
        public VoiceActor[] Actors
        {
            get { return actors; }
            set { actors = value; }
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

        #endregion

    } // Contract class

} // #PROJECTNAME# namespace