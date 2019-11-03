/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VoiceActing
{
    [System.Serializable]
    public class EmotionUsed 
    {
        [SerializeField]
        public Emotion[] emotions = new Emotion[3];

        public EmotionUsed(EmotionCard[] combo)
        {
            if (combo == null)
                return;
            for(int i = 0; i< emotions.Length; i++)
            {
                if(i >= combo.Length)
                {
                    emotions[i] = Emotion.Neutre;
                }
                else if(combo[i] != null)
                {
                    emotions[i] = combo[i].GetEmotion();
                }
            }
        }
    }

    /// <summary>
    /// Definition of the Contract class
    /// A contract is created from a ContractData
    /// ContractData contains all possibilities for a Contract
    /// A contract object is a possibility of ContractData
    /// </summary>

    [System.Serializable]
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
        private ContractType contractType;
        public ContractType ContractType
        {
            get { return contractType; }
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
        private int moneyBonus;
        public int MoneyBonus
        {
            get { return moneyBonus; }
            set { moneyBonus = value; }
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
        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        [SerializeField]
        private int highScore;
        public int HighScore
        {
            get { return highScore; }
            set { highScore = value; }
        }

        [SerializeField]
        private SoundEngineer soundEngineer;
        public SoundEngineer SoundEngineer
        {
            get { return soundEngineer; }
            set { soundEngineer = value; }
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
        private List<EmotionUsed> emotionsUsed = new List<EmotionUsed>();
        public List<EmotionUsed> EmotionsUsed
        {
            get { return emotionsUsed; }
        }

        [SerializeField]
        private List<DoublageEventData> eventData = new List<DoublageEventData>();
        public List<DoublageEventData> EventData
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


        [SerializeField]
        private bool canGameOver;
        public bool CanGameOver
        {
            get { return canGameOver; }
        }

        [SerializeField]
        private StoryEventData storyEventWhenAccepted;
        public StoryEventData StoryEventWhenAccepted
        {
            get { return storyEventWhenAccepted; }
        }
        [SerializeField]
        private StoryEventData storyEventWhenEnd;
        public StoryEventData StoryEventWhenEnd
        {
            get { return storyEventWhenEnd; }
        }
        [SerializeField]
        private DoublageEventData storyEventWhenGameOver;
        public DoublageEventData StoryEventWhenGameOver
        {
            get { return storyEventWhenGameOver; }
            set { storyEventWhenGameOver = value; }
        }


        [SerializeField]
        private bool sessionLock;
        public bool SessionLock
        {
            get { return sessionLock; }
        }


        [SerializeField]
        private AudioClip battleTheme;
        public AudioClip BattleTheme
        {
            get { return battleTheme; }
        }

        [SerializeField]
        private AudioClip victoryTheme;
        public AudioClip VictoryTheme
        {
            get { return victoryTheme; }
        }

        [SerializeField]
        private bool isNull;
        public bool IsNull
        {
            get { return isNull; }
        }

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */
        public int GetHype()
        {
            int res = 0;
            for(int i = 0; i < characters.Count; i++)
            {
                res += characters[i].Fan;
            }
            return res;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public Contract()
        {
            isNull = true;
        }

        public Contract(string name)
        {
            this.name = name;
        }

        private string StringReplace(string stringB, Dictionary<string, string> dict)
        {
            foreach (string k in dict.Keys)
            {
                stringB = stringB.Replace(k, dict[k]);
            }
            return stringB;
        }

        private void StringBuilderReplace(StringBuilder stringB, Dictionary<string, string> dict)
        {
            foreach (string k in dict.Keys)
            {
                stringB.Replace(k, dict[k]);
            }
        }

        public Contract(ContractData data)
        {
            this.name = data.Name;
            if (data.Description.Length == 0)
                this.description = " ";
            else
                this.description = data.Description[Random.Range(0, data.Description.Length)];

            this.contractType = data.ContractType;

            this.level = data.Level;
            this.money = data.SalaryMin + (Random.Range(0, (data.SalaryMax - data.SalaryMin) / 10) * 10); // Renvoie toujours une valeur arrondit a la dizaine
            this.weekRemaining = Random.Range(data.WeekMin, data.WeekMax+1);
            this.totalMixing = Random.Range(data.MixingMin, data.MixingMax+1);

            this.score = 0;
            this.highScore = 0; // Là on met des algo

            this.expGain = data.ExpGain;
            this.expBonus = data.ExpBonus;

            this.producerMP = data.ProducerMP;
            this.artificialIntelligence = data.ArtificialIntelligence;

            this.canGameOver = data.CanGameOver;
            this.storyEventWhenGameOver = data.EventGameOver;

            this.storyEventWhenAccepted = data.EventAcceptedContract;
            this.storyEventWhenEnd = data.EventEndContract;

            this.sessionLock = data.SessionLock;

            this.victoryTheme = data.VictoryTheme;
            this.battleTheme = data.BattleTheme;

            this.soundEngineer = new SoundEngineer();

            this.eventData = new List<DoublageEventData>(data.EventData.Length);
            for (int i = 0; i < data.EventData.Length; i++)
            {
                eventData.Add(data.EventData[i]);
            }


            // Dictionary ------------------------------------------------------------------------
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (data.ContractDictionnary.Length != 0)
            {
                for (int i = 0; i < data.ContractDictionnary.Length; i++)
                {
                    dictionary.Add(data.ContractDictionnary[i].nameID, data.ContractDictionnary[i].namesDictionnary[Random.Range(0, data.ContractDictionnary[i].namesDictionnary.Length)]);
                }
                this.name = StringReplace(this.name, dictionary);
                this.description = StringReplace(this.description, dictionary);
                //StringBuilderReplace(new StringBuilder(this.name, this.name.Length * 2), dictionary);
                //StringBuilderReplace(new StringBuilder(this.description, this.description.Length * 2), dictionary);
            }
            // Dictionary ------------------------------------------------------------------------





            // Select Characters --------------------------------------------------------------------------------------------------------------
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
                voiceActors.Add(new VoiceActor());
            }

            // Select TextData --------------------------------------------------------------------------------------------------------------

            for (int i = 0; i < data.TextDataContract.Length; i++)
            {
                if (data.TextDataContract[i].TextDataCondition.Condition == true)
                {
                    if (data.TextDataContract[i].TextDataCondition.DictCondition != dictionary[data.TextDataContract[i].TextDataCondition.DictID])
                    {
                        continue;
                    }
                }

                if (data.TextDataContract[i].TextDataCondition.All == true)
                {
                    // Selection all
                    for (int j = 0; j < data.TextDataContract[i].TextDataPossible.Length; j++)
                    {
                        textData.Add(new TextData(data.TextDataContract[i].TextDataPossible[j]));
                        textData[textData.Count - 1].Text = StringReplace(textData[textData.Count - 1].Text, dictionary);
                        /*StringReplace(new StringBuilder(textData[textData.Count - 1].Text, textData[textData.Count - 1].Text.Length * 2), dictionary);
                        Debug.Log(textData[textData.Count - 1].Text);*/
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
                            textData[textData.Count - 1].Text = StringReplace(textData[textData.Count - 1].Text, dictionary);
                            /*StringReplace(new StringBuilder(textData[textData.Count - 1].Text, textData[textData.Count - 1].Text.Length * 2), dictionary);
                            Debug.Log(listTextDataPossibilities[indexRandom].Text);*/
                            listTextDataPossibilities.RemoveAt(indexRandom);
                        }
                    }
                }
            }
            totalLine = textData.Count;

            emotionsUsed = new List<EmotionUsed>(textData.Count);

        }


        public bool CheckCharacterLock(List<VoiceActor> voicesActorsPlayer)
        {
            bool b = false;
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].CharacterLock != null)
                {
                    // On cherche l'acteur dans le monde
                    for (int j = 0; j < voicesActorsPlayer.Count; j++)
                    {
                        if (Characters[i].CharacterLock.Name == voicesActorsPlayer[j].VoiceActorName)
                        {
                            VoiceActors[i] = voicesActorsPlayer[j];
                            continue;
                        }
                    }
                    // Si l'acteur n'est pas dans la liste, on l'invoque
                    if (VoiceActors[i].IsNull == true)
                    {
                        voicesActorsPlayer.Add(new VoiceActor(Characters[i].CharacterLock));
                        VoiceActors[i] = voicesActorsPlayer[voicesActorsPlayer.Count - 1];
                        b = true;

                    }
                }
            }
            return b;
        }

        /*private string ReplaceText()
        {

        }*/

        public void ProgressMixing()
        {
            if(soundEngineer != null)
            {
                currentMixing += soundEngineer.GetMixingPower(this);
                if(currentMixing > totalMixing)
                {
                    currentMixing = totalMixing;
                }
            }
        }

        #endregion

    } // Contract class

} // #PROJECTNAME# namespace