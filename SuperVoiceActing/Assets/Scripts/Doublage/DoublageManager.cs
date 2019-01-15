/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageManager class
    /// </summary>
    public class DoublageManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Contrat")]
        [SerializeField]
        protected ContractData contrat;


        [Header("Controllers")]
        [SerializeField]
        protected CameraController cameraController;
        [SerializeField]
        protected InputController inputController;
        [SerializeField]
        protected EmotionAttackManager emotionAttackManager;
        [SerializeField]
        protected EnemyManager enemyManager;
        [SerializeField]
        protected SkillManager skillManager;
        [SerializeField]
        protected TextAppearManager textAppearManager;
        [SerializeField]
        protected PanelPlayer panelPlayer;

        [Header("Feedback & UI")]
        [SerializeField]
        protected TimerDoublage timer;
        [SerializeField]
        protected GameObject feedbackLine;
        [SerializeField]
        protected RectTransform feedbackLineTransform;
        [SerializeField]
        protected TextMeshProUGUI textMeshLine;
        [SerializeField]
        protected TextMeshProUGUI textMeshTurn;
        [SerializeField]
        protected TextMeshProUGUI currentLineNumber;
        [SerializeField]
        protected TextMeshProUGUI maxLineNumber;
        [SerializeField]
        protected GameObject recIcon;

        [Header("Events")]
        [SerializeField]
        protected InputController inputEvent;
        [SerializeField]
        protected CameraController[] cameraControllerEvent;
        [SerializeField]
        protected CharacterDialogueController[] characters;
        [SerializeField]
        protected TextPerformanceAppear[] textEvent;
        [SerializeField]
        protected string endScene;

        [Header("Debug")]
        [SerializeField]
        protected int indexPhrase = 0;
        protected int indexEvent = -1;
        protected DoublageEventData currentEvent;

        protected bool startLine = true;
        protected bool reprintText = true;

        int turnCount = 15;

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


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            if (timer != null)
                timer.SetTurn(turnCount);
            if (maxLineNumber != null)
                maxLineNumber.text = (contrat.TextData.Length-1).ToString();
            if (currentLineNumber != null)
                currentLineNumber.text = indexPhrase.ToString();
            StartCoroutine(IntroductionSequence());
        }
        
        private IEnumerator IntroductionSequence()
        {
            //
            yield return null;
            enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            if (CheckEvent() == false)
            {
                emotionAttackManager.SwitchCardTransformIntro();
                SetPhrase();
            }
            startLine = false;
        }

        // Tape la phrase si les pv sont pas 0
        public void Attack()
        {
            if (textAppearManager.GetEndLine() == true)
            {
                turnCount -= 1;
                if(timer != null)
                    timer.SetTurn(turnCount);
                if(cameraController != null)
                    cameraController.NotQuite();
    
                if(feedbackLine != null)
                    feedbackLine.SetActive(false);
    
                textAppearManager.HideUIButton();
                Emotion[] emotions = emotionAttackManager.GetComboEmotion();
                textAppearManager.ExplodeLetter(enemyManager.DamagePhrase(emotions, textAppearManager.GetWordSelected()), emotions);
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                reprintText = false;
                CheckEvent();
            }
        }

        // Tue la phrase si les pv sont a 0
        public void KillPhrase()
        {
            if(enemyManager.GetHpPercentage() == 0)
            {
                if (textAppearManager.GetEndLine() == true)
                {
                    if (currentLineNumber != null)
                        currentLineNumber.text = indexPhrase.ToString();
                    inputController.gameObject.SetActive(false);
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    textAppearManager.HideUIButton();
                    textAppearManager.TextPop();
                    SetNextPhrase();
                }
            }
        }


        // Modifie les data pour correspondre a la prochaine ligne
        public void SetNextPhrase()
        {
            indexPhrase += 1;
            reprintText = true;
            startLine = true;
            if(indexPhrase < contrat.TextData.Length)
                enemyManager.SetTextData(contrat.TextData[indexPhrase]);

            if (CheckEvent() == false && skillManager.CheckPassiveSkills() == false)
                SetPhrase();
            startLine = false;
        }

        // Affiche la phrase
        public virtual void SetPhrase()
        {
            if (indexPhrase == contrat.TextData.Length)
            {
                EndSession();
                return;
            }
            if(startLine == true)
            {
                FeedbackNewLine();
            }
            inputController.gameObject.SetActive(true);
            inputEvent.gameObject.SetActive(false);
            recIcon.SetActive(true);
            if (reprintText == false)
            {
                reprintText = true;
                return;
            }
            textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text, Emotion.Neutre, true);
            textAppearManager.ShowUIButton(100 - enemyManager.GetHpPercentage());
        }


        public void EndSession()
        {
            SceneManager.LoadScene(endScene);
            Debug.Log("lol");
        }






        private void FeedbackNewLine()
        {
            if (feedbackLine == null)
                return;
            enemyManager.ResetHalo();
            textMeshLine.text = (contrat.TextData.Length - indexPhrase).ToString();
            textMeshTurn.text = turnCount.ToString();
            feedbackLineTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-30f, -10f));
            feedbackLine.SetActive(true);

        }




        // =================================================================
        // Faire un DoublageEventManager

        public void PrintAllText()
        {
            if (textAppearManager.PrintAllText() == false)
            {
                return;
            }
            for (int i = 0; i < textEvent.Length; i++)
            {
                if (textEvent[i].PrintAllText() == false)
                    return;
            }
            inputEvent.gameObject.SetActive(false);
            ExecuteEvent();
        }


        private void ExecuteEvent()
        {
            indexEvent += 1;
            inputController.gameObject.SetActive(false);
            DoublageEvent currentNode = currentEvent.GetEventNode(indexEvent);
            if (currentNode != null)
            {
                if (currentNode is DoublageEventText)
                {
                    DoublageEventText node = (DoublageEventText) currentNode;
                    inputEvent.gameObject.SetActive(true);
                    FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.Text, node.CameraEffectID);
                }
                if (currentNode is DoublageEventCamera)
                {
                    DoublageEventCamera node = (DoublageEventCamera)currentNode;
                    cameraControllerEvent[node.CameraID].ChangeCameraViewport(node.ViewportX, node.ViewportY, node.ViewportWidth, node.ViewportHeight, node.Time);
                    ExecuteEvent();
                }
                if (currentNode is DoublageEventTextPopup)
                {
                    DoublageEventTextPopup node = (DoublageEventTextPopup)currentNode;
                    panelPlayer.StartPopup("Ingé son", node.Text);
                    ExecuteEvent();
                }
                if (currentNode is DoublageEventWait)
                {
                    DoublageEventWait node = (DoublageEventWait)currentNode;
                    StartCoroutine(WaitCoroutine(node.Wait));
                }
                if (currentNode is DoublageEventDeck)
                {
                    DoublageEventDeck node = (DoublageEventDeck)currentNode;
                    emotionAttackManager.ModifiyDeck(node.NewDeck);
                    StartCoroutine(WaitCoroutine(1));
                }
            }
            else // Fin d'event
            {
                emotionAttackManager.SwitchCardTransformToBattle();
                SetPhrase();
            }
        }

        private IEnumerator WaitCoroutine(float time)
        {
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            ExecuteEvent();
        }

        private CharacterDialogueController FindInterlocutor(StoryCharacterData characterToFind)
        {
            for(int i = 0; i < characters.Length; i++)
            {
                if(characters[i].GetStoryCharacterData() == characterToFind)
                {
                    return characters[i];
                }
            }
            return null;
        }



        protected bool CheckEvent()
        {

            for (int i = 0; i < contrat.EventData.Length; i++)
            {
                if(CheckEventCondition(contrat.EventData[i]) == true)
                {
                    indexEvent = -1;
                    currentEvent = contrat.EventData[i];
                    emotionAttackManager.SwitchCardTransformToRessource();
                    recIcon.SetActive(false);
                    ExecuteEvent();
                    return true;
                }
            }
            return false;
        }

        private bool CheckEventCondition(DoublageEventData doublageEvent)
        {
            if(doublageEvent.PhraseNumber == indexPhrase)
            {
                if(doublageEvent.StartPhrase == true && startLine == true)
                {
                    return true;
                }
                else if(doublageEvent.StartPhrase == false)
                {
                    float hp = enemyManager.GetHpPercentage();
                    if (doublageEvent.Equal == true && doublageEvent.HpPercentage == hp)
                    {
                        return true;
                    }
                    else if (doublageEvent.Equal == false && doublageEvent.HpPercentage > hp)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        // =================================================================


        #endregion

    } // DoublageManager class

} // #PROJECTNAME# namespace