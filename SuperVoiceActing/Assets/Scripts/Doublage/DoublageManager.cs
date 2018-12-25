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
        ContractData contrat;


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
        protected TextPerformanceAppear textPerformanceAppear;
        [SerializeField]
        protected PanelPlayer panelPlayer;

        [Header("Events")]
        [SerializeField]
        protected InputController inputEvent;
        [SerializeField]
        protected CameraController[] cameraControllerEvent;
        [SerializeField]
        protected CharacterDialogueController[] characters;
        [SerializeField]
        protected TextPerformanceAppear[] textEvent;

        //[Header("Contrat")]
        int indexPhrase = 0;
        int indexEvent = -1;
        DoublageEventData currentEvent;

        bool startLine = true;

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
            enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            if (CheckEvent() == false)
                StartCoroutine(IntroductionSequence());
            startLine = false;
        }
        
        private IEnumerator IntroductionSequence()
        {
            //
            yield return null;
            emotionAttackManager.SwitchCardTransformIntro();
            inputController.enabled = true;
        }


        public void Attack()
        {
            textPerformanceAppear.ExplodeLetter(enemyManager.DamagePhrase(emotionAttackManager.GetComboEmotion(), textPerformanceAppear.GetWordSelected()));
            CheckEvent();
        }

        public void KillPhrase()
        {
            if(enemyManager.GetHpPercentage() == 0)
            {
                inputController.enabled = false;
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                SetNextPhrase();
            }
        }



        public void SetNextPhrase()
        {
            indexPhrase += 1;
            startLine = true;
            enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            if (CheckEvent() == false)
                SetPhrase();
            startLine = false;
        }

        public void SetPhrase()
        {
            textPerformanceAppear.NewPhrase(contrat.TextData[indexPhrase].Text);
            inputController.enabled = true;
        }


        // =================================================================
        // Faire un DoublageEventManager

        public void PrintAllText()
        {
            if (textPerformanceAppear.PrintAllText() == false)
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
                    FindInterlocutor(node.Interlocuteur).TextActing.NewPhrase(node.Text);
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



        private bool CheckEvent()
        {

            for (int i = 0; i < contrat.EventData.Length; i++)
            {
                if(CheckEventCondition(contrat.EventData[i]) == true)
                {
                    indexEvent = -1;
                    currentEvent = contrat.EventData[i];
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