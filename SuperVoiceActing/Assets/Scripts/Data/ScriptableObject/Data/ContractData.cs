/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public enum ContractType
    {
        Film,
        Serie,
        JeuVideo,
        Autre
    }

    // TextData Contract ===================================================================

    [System.Serializable]
    public class TextDatabaseContract
    {
        [Header("Parameter")]
        //[HorizontalGroup("NumberPhrase")]
        [SerializeField]
        private int nbPhraseMin;
        public int NbPhraseMin
        {
            get { return nbPhraseMin; }
            set { nbPhraseMin = value; }
        }
        //[HorizontalGroup("NumberPhrase")]
        [SerializeField]
        private int nbPhraseMax;
        public int NbPhraseMax
        {
            get { return nbPhraseMax; }
            set { nbPhraseMax = value; }
        }

        [Header("Database")]
        [SerializeField]
        private TextDataContract[] textDataPossible;
        public TextDataContract[] TextDataPossible
        {
            get { return textDataPossible; }
            set { textDataPossible = value; }
        }
    }

    // TextData Contract ===================================================================

    [System.Serializable]
    public class TextDataContract
    {
        [Header("TextData ---------------")]
        [SerializeField]
        private int interlocuteurID;
        public int InterlocuteurID
        {
            get { return interlocuteurID; }
            set { interlocuteurID = value; }
        }

        [HorizontalGroup("HP")]
        [SerializeField]
        private int hpMin;
        public int HPMin
        {
            get { return hpMin; }
            set { hpMin = value; }
        }
        [HorizontalGroup("HP")]
        [SerializeField]
        private int hpMax;
        public int HPMax
        {
            get { return hpMax; }
            set { hpMax = value; }
        }


        [HorizontalGroup("Atk")]
        [SerializeField]
        private int atkMin;
        public int AtkMin
        {
            get { return atkMin; }
            set { atkMin = value; }
        }
        [HorizontalGroup("Atk")]
        [SerializeField]
        private int atkMax;
        public int AtkMax
        {
            get { return atkMax; }
            set { atkMax = value; }
        }


        [HorizontalGroup("Evade")]
        [SerializeField]
        private int evadeMin;
        public int EvadeMin
        {
            get { return evadeMin; }
            set { evadeMin = value; }
        }
        [HorizontalGroup("Evade")]
        [SerializeField]
        private int evadeMax;
        public int EvadeMax
        {
            get { return evadeMax; }
            set { evadeMax = value; }
        }




        [HorizontalGroup("Texte")]
        [SerializeField]
        [TextArea]
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }


        [TabGroup("ParentGroup", "Statistiques")]
        //[BoxGroup("Statistique")]
        [SerializeField]
        private EmotionStat enemyResistance;
        public EmotionStat EnemyResistance
        {
            get { return enemyResistance; }
            set { enemyResistance = value; }
        }

        [TabGroup("ParentGroup", "Points faibles")]
        [SerializeField]
        private WeakPoint[] enemyWeakPoints;
        public WeakPoint[] EnemyWeakPoints
        {
            get { return enemyWeakPoints; }
            set { enemyWeakPoints = value; }
        }

    }



    /// <summary>
    /// Definition of the ContractData class ==================================================================================
    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "ContractData", menuName = "ContractData", order = 1)]
    public class ContractData : ScriptableObject
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Info")]
        [SerializeField]
        private string name;
        public string Name
        {
            get { return name; }
        }
        [SerializeField]
        private int level;
        public int Level
        {
            get { return level; }
        }

        [SerializeField]
        private ContractType contractType;
        public ContractType ContractType
        {
            get { return contractType; }
        }

        [HorizontalGroup("Salary")]
        [SerializeField]
        private int salaryMin;
        public int SalaryMin
        {
            get { return salaryMin; }
        }
        [HorizontalGroup("Salary")]
        [SerializeField]
        private int salaryMax;
        public int SalaryMax
        {
            get { return salaryMax; }
        }

        [HorizontalGroup("Mixage")]
        [SerializeField]
        private int mixingMin;
        public int MixingMin
        {
            get { return mixingMin; }
        }
        [HorizontalGroup("Mixage")]
        [SerializeField]
        private int mixingMax;
        public int MixingMax
        {
            get { return mixingMax; }
        }

        [HorizontalGroup("Semaine")]
        [SerializeField]
        private int weekMin;
        public int WeekMin
        {
            get { return weekMin; }
        }
        [HorizontalGroup("Semaine")]
        [SerializeField]
        private int weekMax;
        public int WeekMax
        {
            get { return weekMax; }
        }

        [Space]
        [Space]

        [Header("Characters")]
        [SerializeField]
        private RoleContractDatabase[] characters;
        public RoleContractDatabase[] Characters
        {
            get { return characters; }
        }



        [Space]
        [Space]

        [Header("Text")]
        [SerializeField]
        private TextDatabaseContract[] textDataContract;
        public TextDatabaseContract[] TextDataContract
        {
            get { return textDataContract; }
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