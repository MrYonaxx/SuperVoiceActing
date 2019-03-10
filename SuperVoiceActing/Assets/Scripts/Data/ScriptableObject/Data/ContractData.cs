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

    // TextDataCondition Contract ===================================================================
    [System.Serializable]
    public class TextDataCondition
    {
        [Space]
        [HorizontalGroup("All", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private bool all;
        public bool All
        {
            get { return all; }
        }

        [Space]
        [HorizontalGroup("PhraseMin", LabelWidth = 100, Width = 100)]
        [HideIf("all")]
        [SerializeField]
        private int nbPhraseMin;
        public int NbPhraseMin
        {
            get { return nbPhraseMin; }
        }

        [HorizontalGroup("PhraseMax", LabelWidth = 100, Width = 100)]
        [HideIf("all")]
        [SerializeField]
        private int nbPhraseMax;
        public int NbPhraseMax
        {
            get { return nbPhraseMax; }
        }

        [Space]

        [HorizontalGroup("Condition", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private bool condition;
        public bool Condition
        {
            get { return condition; }
        }

        /*[HorizontalGroup("ConditionPerso", LabelWidth = 100, Width = 100)]
        [ShowIf("condition")]
        [SerializeField]
        private int characterCondition;
        public int CharacterCondition
        {
            get { return characterCondition; }
        }*/
        [HorizontalGroup("Dictionnary", LabelWidth = 0, Width = 150)]
        [ShowIf("condition")]
        [SerializeField]
        [HideLabel]
        private string dictionnaryCondition;
        public string DictionnaryCondition
        {
            get { return dictionnaryCondition; }
        }
    }

    // TextData Contract ===================================================================

    [System.Serializable]
    public class TextDatabaseContract
    {
        [HorizontalGroup("Hey", LabelWidth = 100, Width = 100)]
        [HideLabel]
        [SerializeField]
        private TextDataCondition textDataCondition;
        public TextDataCondition TextDataCondition
        {
            get { return textDataCondition; }
        }

        [HorizontalGroup("Hey")]
        [SerializeField]
        private TextDataContract[] textDataPossible;
        public TextDataContract[] TextDataPossible
        {
            get { return textDataPossible; }
        }
    }



    // TextData Contract ===================================================================

    [System.Serializable]
    public class TextStats
    {
        [HorizontalGroup("Interlocuteur", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int interlocuteurID;
        public int InterlocuteurID
        {
            get { return interlocuteurID; }
        }
        [HorizontalGroup("Interlocuteur", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int linked;
        public int Linked
        {
            get { return linked; }
        }

        [HorizontalGroup("HP", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int hpMin;
        public int HPMin
        {
            get { return hpMin; }
        }
        [HorizontalGroup("HP", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int hpMax;
        public int HPMax
        {
            get { return hpMax; }
        }


        [HorizontalGroup("Atk", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int atkMin;
        public int AtkMin
        {
            get { return atkMin; }
        }
        [HorizontalGroup("Atk", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int atkMax;
        public int AtkMax
        {
            get { return atkMax; }
        }


        [HorizontalGroup("Evade", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int evadeMin;
        public int EvadeMin
        {
            get { return evadeMin; }
        }
        [HorizontalGroup("Evade", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private int evadeMax;
        public int EvadeMax
        {
            get { return evadeMax; }
        }
    }






    // TextData Contract ===================================================================

    [System.Serializable]
    public class TextDataContract
    {


        [Space]
        [SerializeField]
        [HorizontalGroup("Test3", Width = 200, MarginLeft = 20)]
        [TextArea(3, 3)]
        [HideLabel]
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }



        [HorizontalGroup("Test3")]
        [SerializeField]
        [HideLabel]
        private TextStats textStats;
        public TextStats TextStats
        {
            get { return textStats; }
        }

        //[TabGroup("ParentGroup", "Statistiques")]
        //[BoxGroup("Statistique")]
        [SerializeField]
        [HideLabel]
        private EmotionStat enemyResistance;
        public EmotionStat EnemyResistance
        {
            get { return enemyResistance; }
            set { enemyResistance = value; }
        }

        //[TabGroup("ParentGroup", "Points faibles")]
        [Space]
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
        private string[] contractTitle;
        public string[] ContractTitle
        {
            get { return contractTitle; }
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

        [HorizontalGroup("EXP")]
        [SerializeField]
        private int expGain;
        public int ExpGain
        {
            get { return expGain; }
        }
        [HorizontalGroup("EXP")]
        [SerializeField]
        private int expBonus;
        public int ExpBonus
        {
            get { return expBonus; }
        }

        [Space]

        [TextArea]
        [SerializeField]
        private string[] description;
        public string[] Description
        {
            get { return description; }
        }

        [Space]
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
        [Space]

        /*[Header("Dictionnaire")]
        [SerializeField]*/


        [Space]
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
        [Space]

        [SerializeField]
        private TextData[] textData;
        public TextData[] TextData
        {
            get { return textData; }
        }

        [Space]
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