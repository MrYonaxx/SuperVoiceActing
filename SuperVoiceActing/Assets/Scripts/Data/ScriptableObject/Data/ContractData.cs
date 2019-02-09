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


    [System.Serializable]
    public class CharacterContractData
    {
        [SerializeField]
        private string[] name;
        public string[] Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField]
        private EmotionStat characterStatMin;
        public EmotionStat CharacterStatMin
        {
            get { return characterStatMin; }
            set { characterStatMin = value; }
        }
        [SerializeField]
        private EmotionStat characterStatMax;
        public EmotionStat CharacterStatMax
        {
            get { return characterStatMax; }
            set { characterStatMax = value; }
        }

    }



    /// <summary>
    /// Definition of the ContractData class
    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "ContractData", menuName = "ContractData", order = 1)]
    public class ContractData : ScriptableObject
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        string name;

        [Space]
        [Space]

        [SerializeField]
        private CharacterContractData[] characters;
        public CharacterContractData[] Characters
        {
            get { return characters; }
        }

        [Space]
        [Space]

        [SerializeField]
        private TextData[] textData;
        public TextData[] TextData
        {
            get { return textData; }
        }

        [Space]
        [Space]

        [SerializeField]
        private DoublageEventData[] eventData;
        public DoublageEventData[] EventData
        {
            get { return eventData; }
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

        
        #endregion

    } // ContractData class

} // #PROJECTNAME# namespace