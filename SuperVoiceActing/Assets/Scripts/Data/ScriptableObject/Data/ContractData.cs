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
        Publicite,
        Autre
    }

    [System.Serializable]
    public class ContractDictionnary
    {
        [SerializeField]
        public string nameID;

        [SerializeField]
        public string[] namesDictionnary;
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

        [Header("PersoCondition")]
        [HorizontalGroup("ConditionPerso", LabelWidth = 100, Width = 100)]
        [ShowIf("condition")]
        [SerializeField]
        [HideLabel]
        private int characterCondition;
        public int CharacterCondition
        {
            get { return characterCondition; }
        }

        [Header("DictCondition")]
        [HorizontalGroup("Dictionnary", LabelWidth = 100, Width = 100)]
        [ShowIf("condition")]
        [SerializeField]
        [HideLabel]
        private string dictID;
        public string DictID
        {
            get { return dictID; }
        }

        [HorizontalGroup("Dictionnary2", LabelWidth = 100, Width = 100)]
        [ShowIf("condition")]
        [SerializeField]
        [HideLabel]
        private string dictCondition;
        public string DictCondition
        {
            get { return dictCondition; }
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
        [GUIColor(1f, 1f, 1f, 1f)]
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


        /*[HorizontalGroup("Atk", LabelWidth = 100, Width = 100)]
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
        }*/
    }






    // TextData Contract ===================================================================

    [System.Serializable]
    public class TextDataContract
    {


        [Space]
        [SerializeField]
        [HorizontalGroup("Test3", Width = 200, MarginLeft = 20)]
        [TextArea(1, 1)]
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

        [Title("Info")]
        [SerializeField]
        private string contractName;
        public string Name
        {
            get { return contractName; }
        }

        [HorizontalGroup("Level")]
        [SerializeField]
        private int level;
        public int Level
        {
            get { return level; }
        }

        [HorizontalGroup("Level")]
        [SerializeField]
        [HideLabel]
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
        [HorizontalGroup("Dictionnaire")]
        [SerializeField]
        ContractDictionnary[] contractDictionnary;
        public ContractDictionnary[] ContractDictionnary
        {
            get { return contractDictionnary; }
        }
        [Space]
        [HorizontalGroup("Dictionnaire")]
        [TextArea]
        [SerializeField]
        private string[] description;
        public string[] Description
        {
            get { return description; }
        }

        [SerializeField]
        private bool isFranchise;
        public bool IsFranchise
        {
            get { return isFranchise; }
        }
        [SerializeField]
        private string[] franchiseNames;
        public string[] FranchiseNames
        {
            get { return franchiseNames; }
        }

        [Space]
        [Space]
        [Space]

        [Title("Characters")]
        [GUIColor(1f, 0.9f, 0.9f, 1f)]
        [SerializeField]
        private RoleContractDatabase[] characters;
        public RoleContractDatabase[] Characters
        {
            get { return characters; }
        }

        [Space]
        [Space]
        [Space]

        [Title("Product Manager")]
        [SerializeField]
        private int producerMP;
        public int ProducerMP
        {
            get { return producerMP; }
        }

        [SerializeField]
        private EnemyAI[] artificialIntelligence;
        public EnemyAI[] ArtificialIntelligence
        {
            get { return artificialIntelligence; }
        }

        /*[SerializeField]
        private int duelAI;
        public int DuelAI
        {
            get { return duelAI; }
        }*/



        [Space]
        [Space]
        [Space]

        [Title("Text")]
        [GUIColor(0.92f, 0.92f, 1f, 1f)]
        [SerializeField]
        private TextDatabaseContract[] textDataContract;
        public TextDatabaseContract[] TextDataContract
        {
            get { return textDataContract; }
        }



        [Space]
        [Space]
        [Space]

        [Title("Events")]

        [SerializeField]
        private DoublageEventData[] eventData;
        public DoublageEventData[] EventData
        {
            get { return eventData; }
        }

        [Space]
        [Space]
        [SerializeField]
        private StoryEventData eventAcceptedContract;
        public StoryEventData EventAcceptedContract
        {
            get { return eventAcceptedContract; }
        }
        [SerializeField]
        private StoryEventData eventEndContract;
        public StoryEventData EventEndContract
        {
            get { return eventEndContract; }
        }
        [Space]
        [SerializeField]
        private bool canGameOver;
        public bool CanGameOver
        {
            get { return canGameOver; }
        }
        [SerializeField]
        private DoublageEventData eventGameOver;
        public DoublageEventData EventGameOver
        {
            get { return eventGameOver; }
        }

        [Space]
        [Space]
        [Space]
        [Title("Autre")]
        [SerializeField]
        private bool changeBGM;
        public bool ChangeBGM
        {
            get { return changeBGM; }
        }

        [HorizontalGroup("MusiqueContrat", LabelWidth = 130)]
        [ShowIf("changeBGM", true)]
        [SerializeField]
        private AudioClip battleTheme;
        public AudioClip BattleTheme
        {
            get { return battleTheme; }
        }

        [HorizontalGroup("MusiqueContrat", LabelWidth = 130)]
        [ShowIf("changeBGM", true)]
        [SerializeField]
        private AudioClip victoryTheme;
        public AudioClip VictoryTheme
        {
            get { return victoryTheme; }
        }

        [Space]
        [Space]
        [Space]
        [SerializeField]
        private bool sessionLock;
        public bool SessionLock
        {
            get { return sessionLock; }
        }



        #endregion


    } // ContractData class

} // #PROJECTNAME# namespace