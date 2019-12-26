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

    // TextDataCondition Contract =================================================================================================================
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

        //[Space]
        [HorizontalGroup("PhraseMin", LabelWidth = 70, Width = 100)]
        [HideIf("all")]
        [SerializeField]
        private int nbMin;
        public int NbMin
        {
            get { return nbMin; }
        }

        [HorizontalGroup("PhraseMax", LabelWidth = 70, Width = 100)]
        [HideIf("all")]
        [SerializeField]
        private int nbMax;
        public int NbMax
        {
            get { return nbMax; }
        }

        //[Space]
        [FoldoutGroup("Advanced")]
        //[HorizontalGroup("Condition", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private bool conditionChara;
        public bool ConditionChara
        {
            get { return conditionChara; }
        }

        [FoldoutGroup("Advanced")]
        //[HorizontalGroup("ConditionPerso", LabelWidth = 100, Width = 100)]
        [ShowIf("conditionChara")]
        [SerializeField]
        [HideLabel]
        private int characterCondition;
        public int CharacterCondition
        {
            get { return characterCondition; }
        }

        //[Space]
        [FoldoutGroup("Advanced")]
        //[HorizontalGroup("ConditionDico", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private bool conditionDico;
        public bool ConditionDico
        {
            get { return conditionDico; }
        }

        [FoldoutGroup("Advanced")]
        //[HorizontalGroup("Dictionnary", LabelWidth = 100, Width = 100)]
        [ShowIf("conditionDico")]
        [SerializeField]
        [HideLabel]
        private string dictID;
        public string DictID
        {
            get { return dictID; }
        }

        [FoldoutGroup("Advanced")]
        //[HorizontalGroup("Dictionnary2", LabelWidth = 100, Width = 100)]
        [ShowIf("conditionDico")]
        [SerializeField]
        [HideLabel]
        private string dictCondition;
        public string DictCondition
        {
            get { return dictCondition; }
        }
        //[Space]


        [FoldoutGroup("Advanced")]
        //[HorizontalGroup("Copy", LabelWidth = 100, Width = 100)]
        [SerializeField]
        private bool copy;
        public bool Copy
        {
            get { return copy; }
        }

        [FoldoutGroup("Advanced")]
        //[HorizontalGroup("Copy2", LabelWidth = 100, Width = 100)]
        [ShowIf("copy")]
        [SerializeField]
        [HideLabel]
        private int textCopy = -1;
        public int TextCopy
        {
            get { return textCopy; }
        }


        /*[FoldoutGroup("Advanced")]
        [SerializeField]
        [HideLabel]
        private string hintContext;
        public string HintContext
        {
            get { return hintContext; }
        }*/

        /*[SerializeField]
        [HideLabel]
        private DoublageEventData eventData;
        public DoublageEventData EventData
        {
            get { return eventData; }
        }*/


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
    public class TextDataContract
    {
        [Space]
        [SerializeField]
        [HorizontalGroup("Test3", Width = 200, MarginLeft = 15)]
        [VerticalGroup("Test3/Left")]
        [TextArea(1, 1)]
        [HideLabel]
        private string text;
        public string Text
        {
            get { return text; }
        }

        [Space]
        [HorizontalGroup("Test3", LabelWidth = 100, Width = 100)]
        [VerticalGroup("Test3/Center")]
        [SerializeField]
        private int interlocuteurID;
        public int InterlocuteurID
        {
            get { return interlocuteurID; }
        }

        [Space]
        [HorizontalGroup("Test3", LabelWidth = 100, Width = 100)]
        [VerticalGroup("Test3/Right")]
        [SerializeField]
        private bool advanced;
        public bool Advanced
        {
            get { return advanced; }

        }

        [VerticalGroup("Test3/Center")]
        [SerializeField]
        private int hpMin;
        public int HPMin
        {
            get { return hpMin; }
        }
        [VerticalGroup("Test3/Right")]
        [SerializeField]
        private int hpMax;
        public int HPMax
        {
            get { return hpMax; }
        }

        [ShowIf("advanced")]
        [HorizontalGroup("Advanced", MarginLeft = 20, Width = 200, LabelWidth = 50)]
        [SerializeField]
        private string context;
        public string Context
        {
            get { return context; }
        }

        [SerializeField]
        [HideLabel]
        private EmotionStat enemyResistance;
        public EmotionStat EnemyResistance
        {
            get { return enemyResistance; }
        }



        [Space]
        [HorizontalGroup("WeakPoint", MarginLeft = 15, MarginRight = 5)]
        [SerializeField]
        private WeakPoint[] enemyWeakPoints;
        public WeakPoint[] EnemyWeakPoints
        {
            get { return enemyWeakPoints; }
            set { enemyWeakPoints = value; }
        }
        [Space]
        [HorizontalGroup("WeakPoint", MarginLeft = 5, MarginRight = 15)]
        [SerializeField]
        private DoublageEventData[] eventData;
        public DoublageEventData[] EventData
        {
            get { return eventData; }
        }

        [ShowIf("advanced")]
        [SerializeField]
        private CustomTextData[] customTextDatas;
        public CustomTextData[] CustomTextDatas
        {
            get { return customTextDatas; }
        }



    }

    [System.Serializable]
    public class CustomTextData
    {
        [Space]
        [SerializeField]
        [HorizontalGroup("Test3", Width = 200, MarginLeft = 15)]
        [VerticalGroup("Test3/Left")]
        [TextArea(1, 1)]
        [HideLabel]
        private string text;
        public string Text
        {
            get { return text; }
        }

        [Space]
        [HorizontalGroup("Test3", LabelWidth = 100, Width = 100)]
        [VerticalGroup("Test3/Center")]
        [SerializeField]
        private int interlocuteurID;
        public int InterlocuteurID
        {
            get { return interlocuteurID; }
        }

        [Space]
        [HorizontalGroup("Test3", LabelWidth = 100, Width = 100)]
        [VerticalGroup("Test3/Right")]
        [SerializeField]
        private bool advanced;
        public bool Advanced
        {
            get { return advanced; }

        }

        [VerticalGroup("Test3/Center")]
        [SerializeField]
        private int hpMin;
        public int HPMin
        {
            get { return hpMin; }
        }
        [VerticalGroup("Test3/Right")]
        [SerializeField]
        private int hpMax;
        public int HPMax
        {
            get { return hpMax; }
        }

        [ShowIf("advanced")]
        [HorizontalGroup("Advanced", MarginLeft = 20, Width = 200, LabelWidth = 50)]
        [SerializeField]
        private string context;
        public string Context
        {
            get { return context; }
        }

        [SerializeField]
        [HideLabel]
        private EmotionStat enemyResistance;
        public EmotionStat EnemyResistance
        {
            get { return enemyResistance; }
        }



        [Space]
        [HorizontalGroup("WeakPoint", MarginLeft = 15, MarginRight = 5)]
        [SerializeField]
        private WeakPoint[] enemyWeakPoints;
        public WeakPoint[] EnemyWeakPoints
        {
            get { return enemyWeakPoints; }
            set { enemyWeakPoints = value; }
        }
        [Space]
        [HorizontalGroup("WeakPoint", MarginLeft = 5, MarginRight = 15)]
        [SerializeField]
        private DoublageEventData[] eventData;
        public DoublageEventData[] EventData
        {
            get { return eventData; }
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

        [Space]
        [HorizontalGroup("Salary")]
        [SerializeField]
        private int salaryMin;
        public int SalaryMin
        {
            get { return salaryMin; }
        }
        [Space]
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
        private int weekMin = 3;
        public int WeekMin
        {
            get { return weekMin; }
        }
        [HorizontalGroup("Semaine")]
        [SerializeField]
        private int weekMax = 7;
        public int WeekMax
        {
            get { return weekMax; }
        }

        [HorizontalGroup("EXP")]
        [SerializeField]
        private int expGain = 20;
        public int ExpGain
        {
            get { return expGain; }
        }
        [HorizontalGroup("EXP")]
        [SerializeField]
        private int expBonus = 100;
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


        [Space]
        [Space]
        [Space]

        [SerializeField]
        [Toggle("Enabled")]
        private Franchise franchise;
        public Franchise Franchise
        {
            get { return franchise; }
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

        [Title("GlobalEvents")]
        [SerializeField]
        private DoublageEventData[] eventData;
        public DoublageEventData[] EventData
        {
            get { return eventData; }
        }

        [HorizontalGroup("StoryEvent", LabelWidth = 150)]
        [SerializeField]
        private StoryEventData eventAcceptedContract;
        public StoryEventData EventAcceptedContract
        {
            get { return eventAcceptedContract; }
        }
        [HorizontalGroup("StoryEvent", LabelWidth = 150)]
        [SerializeField]
        private StoryEventData eventBeforeSession;
        public StoryEventData EventBeforeSession
        {
            get { return eventBeforeSession; }
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
        [HorizontalGroup("Sound", LabelWidth = 100)]
        [ShowIf("changeBGM", true)]
        [SerializeField]
        private AudioClip battleTheme;
        public AudioClip BattleTheme
        {
            get { return battleTheme; }
        }
        [HorizontalGroup("Sound", LabelWidth = 100)]
        [ShowIf("changeBGM", true)]
        [SerializeField]
        private AudioClip victoryTheme;
        public AudioClip VictoryTheme
        {
            get { return victoryTheme; }
        }

        [Space]
        [SerializeField]
        private bool sessionLock;
        public bool SessionLock
        {
            get { return sessionLock; }
        }


        [Space]
        [Space]
        [Space]
        [Title("Debug")]
        [SerializeField]
        public int debugHPMin;
        public int debugHPMax;

        [Button]
        private void AssignDebugHP()
        {
            for (int i = 0; i < textDataContract.Length; i++)
            {
                for (int j = 0; j < textDataContract[i].TextDataPossible.Length; j++)
                {
                    //textDataContract[i].TextDataPossible[j].TextStats.SetDebugHP(debugHPMin, debugHPMax);
                    for (int k = 0; k < textDataContract[i].TextDataPossible[j].CustomTextDatas.Length; k++)
                    {
                        //textDataContract[i].TextDataPossible[j].CustomTextDatas[k].TextStats.SetDebugHP(debugHPMin, debugHPMax);
                    }
                }
            }
        }

        #endregion


    } // ContractData class

} // #PROJECTNAME# namespace