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

        [Header("Events")]
        [SerializeField]
        protected InputController inputEvent;
        [SerializeField]
        protected CameraController[] cameraControllerEvent;
        [SerializeField]
        protected CharacterDialogueController[] characters;
        [SerializeField]
        protected TextPerformanceAppear[] textEvent;

        [Header("Debug")]
        [SerializeField]
        protected int indexPhrase = 0;
        protected int indexEvent = -1;
        protected DoublageEventData currentEvent;

        protected bool startLine = true;

        int turnCount = 0;

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


        public void Attack()
        {
            if (enemyManager.GetHpPercentage() != 0)
            {
                if (textAppearManager.GetEndLine() == true)
                {
                    turnCount += 1;
                    if(timer != null)
                        timer.SetTurn(turnCount);
                    if(cameraController != null)
                        cameraController.NotQuite();

                    textAppearManager.HideUIButton();
                    Emotion[] emotions = emotionAttackManager.GetComboEmotion();
                    textAppearManager.ExplodeLetter(enemyManager.DamagePhrase(emotions, textAppearManager.GetWordSelected()), emotions);
                    CheckEvent();
                }
            }
        }

        public void KillPhrase()
        {
            if(enemyManager.GetHpPercentage() == 0)
            {
                if (textAppearManager.GetEndLine() == true)
                {
                    inputController.enabled = false;
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    textAppearManager.HideUIButton();
                    SetNextPhrase();
                }
            }
        }



        public void SetNextPhrase()
        {
            indexPhrase += 1;
            startLine = true;
            enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            if (CheckEvent() == false && skillManager.CheckPassiveSkills() == false)
                SetPhrase();
            startLine = false;
        }

        public virtual void SetPhrase()
        {
            textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text);
            textAppearManager.ShowUIButton(100 - enemyManager.GetHpPercentage());
            inputController.enabled = true;
            inputEvent.enabled = false;
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
            inputEvent.enabled = false;
            ExecuteEvent();
        }


        private void ExecuteEvent()
        {
            indexEvent += 1;
            inputController.enabled = false;
            DoublageEvent currentNode = currentEvent.GetEventNode(indexEvent);
            if (currentNode != null)
            {
                if (currentNode is DoublageEventText)
                {
                    DoublageEventText node = (DoublageEventText) currentNode;
                    inputEvent.enabled = true;
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