/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
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
        protected ContractData contratData;

        [Header("Contrat")]
        [SerializeField]
        protected PlayerData playerData;


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
        protected ActorsManager actorsManager;
        [SerializeField]
        protected RoleManager roleManager;
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
        [SerializeField]
        protected Image fade;
        [SerializeField]
        protected TextMeshProUGUI textDemo;

        [Header("IntroSequence")]
        [SerializeField]
        CharacterDialogueController introText;
        [SerializeField]
        GameObject textIntro;
        [SerializeField]
        Image introBlackScreen;
        [SerializeField]
        Animator[] animatorsIntro;
        [SerializeField]
        protected AudioSource audioSourceBattleTheme;

        [Header("EndSequence")]
        [SerializeField]
        Animator endBlackScreen;
        [SerializeField]
        Animator bandeRythmo;
        [SerializeField]
        protected AudioSource audioSourceYokaiDisco;
        [SerializeField]
        Animator mcPanel;
        [SerializeField]
        Animator ingeSonPanel;
        [SerializeField]
        GameObject resultScreen;

        [Header("UI")]
        [SerializeField]
        Image buttonUIY;
        [SerializeField]
        Image buttonUIA;

        [Header("AudioSource")]
        [SerializeField]
        protected AudioSource audioSourceKillPhrase;
        [SerializeField]
        protected AudioSource audioSourceKillPhrase2;
        [SerializeField]
        protected AudioSource audioSourceAttack;
        [SerializeField]
        protected AudioSource audioSourceAttack2;
        [SerializeField]
        protected AudioSource audioSourceSpotlight;

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
        protected GameObject[] popups;
        [SerializeField]
        protected string endScene;

        [Header("Debug")]
        [SerializeField]
        protected int indexPhrase = 0;
        protected int indexEvent = -1;
        protected DoublageEventData currentEvent;

        protected bool startLine = true;
        protected bool reprintText = true;

        protected Contract contrat;

        protected int turnCount = 15;

        protected Emotion[] lastAttack = null;

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

            contrat = playerData.CurrentContract;
            if(contrat == null)
            {
                if (contratData != null)
                {
                    contrat = new Contract(contratData); // Pour Debug
                }
            }



            if (timer != null)
                timer.SetTurn(turnCount);
            if (maxLineNumber != null)
                maxLineNumber.text = (contrat.TextData.Count).ToString();
            if (currentLineNumber != null)
                currentLineNumber.text = (indexPhrase + 1).ToString();
            StartCoroutine(IntroductionSequence());
        }

        private IEnumerator IntroductionSequence()
        {
            //
            yield return null;
            enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            enemyManager.SetVoiceActor(contrat.VoiceActors[0]);
            actorsManager.SetActors(contrat.VoiceActors);
            //
            if (CheckEvent() == false)
            {
                yield return new WaitForSeconds(1);
                introText.SetPhraseTextacting("25 Février", 0);
                yield return new WaitForSeconds(1.5f);
                introText.SetPhraseTextacting(contrat.Name, 0);
                yield return new WaitForSeconds(3);
                textIntro.SetActive(false);
                yield return new WaitForSeconds(1f);
                introBlackScreen.gameObject.SetActive(false);
                introBlackScreen.enabled = false;
                audioSourceBattleTheme.enabled = true;
                //FeedbackNewLine();
                for (int i = 0; i < animatorsIntro.Length; i++)
                {
                    animatorsIntro[i].enabled = true;
                }
                audioSourceSpotlight.Play();
                yield return new WaitForSeconds(1);
                cameraController.MoveToInitialPosition(300);
                yield return new WaitForSeconds(1);
                emotionAttackManager.SwitchCardTransformIntro();
                yield return new WaitForSeconds(0.5f);
                SetPhrase();
            }
            startLine = false;
        }






        // Tape la phrase si les pv sont pas 0
        public void Attack()
        {
            if (textAppearManager.GetEndLine() == true)
            {
                if (emotionAttackManager.GetComboCount() == -1)
                {
                    emotionAttackManager.SelectCard("Neutre");
                    return;
                }
                actorsManager.ActorTakeDamage(4);
                audioSourceAttack.Play();
                audioSourceAttack2.Play();
                turnCount -= 1;
                if(timer != null)
                    timer.SetTurn(turnCount);
    
                if(feedbackLine != null)
                    feedbackLine.SetActive(false);
    
                HideUIButton();
                lastAttack = emotionAttackManager.GetComboEmotion();
                Debug.Log(lastAttack[0]);
                textAppearManager.ExplodeLetter(enemyManager.DamagePhrase(lastAttack, textAppearManager.GetWordSelected()), lastAttack);
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                emotionAttackManager.SwitchCardTransformToRessource();
                reprintText = false;
                StartCoroutine(CoroutineAttack(10));
                CheckEvent();
                inputController.gameObject.SetActive(false);
            }
        }


        private IEnumerator CoroutineAttack(int time)
        {
            if (cameraController == null)
            {
                yield break;
            }
            cameraController.ChangeOrthographicSize(-1, time);
            int timer = time;
            while (timer != 0)
            {
                timer -= 1;
                yield return null;
            }
            cameraController.ChangeOrthographicSize(2, time*2);
            timer = time*2;
            while (timer != 0)
            {
                timer -= 1;
                yield return null;
            }
            cameraController.ChangeOrthographicSize(0, time*6);
            StartCoroutine(WaitText());
        }






        // Tue la phrase si les pv sont a 0
        public void KillPhrase()
        {
            /*enemyManager.SetHp(0);
            indexPhrase = contrat.TextData.Count - 1;*/
            if (enemyManager.GetHpPercentage() == 0)
            {
                if (textAppearManager.GetEndLine() == true)
                {
                    
                    indexPhrase += 1;
                    if (currentLineNumber != null)
                        currentLineNumber.text = (indexPhrase+1).ToString();
                    inputController.gameObject.SetActive(false);
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    HideUIButton();

                    //indexPhrase += 1;
                    reprintText = true;
                    startLine = true;
                    if (indexPhrase < contrat.TextData.Count)
                        enemyManager.SetTextData(contrat.TextData[indexPhrase]);

                    if (CheckEvent() == false)
                    {
                        enemyManager.DamagePhrase();
                        textAppearManager.TextPop();                 
                        textAppearManager.SelectWord(0);
                        FeedbackNewLine();
                        if (cameraController != null && indexPhrase < contrat.TextData.Count)
                            cameraController.NotQuite();
                        
                        audioSourceKillPhrase.Play();
                        audioSourceKillPhrase2.Play();
                        if (indexPhrase == contrat.TextData.Count)
                        {
                            EndSession();
                            return;
                        }
                        StartCoroutine(WaitCoroutineNextPhrase(60));
                    }
                    startLine = false;
                }
            }
        }

        private IEnumerator WaitCoroutineNextPhrase(float time)
        {
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            SetPhrase();
        }


        // Modifie les data pour correspondre a la prochaine ligne
        public void SetNextPhrase()
        {
            indexPhrase += 1;
            reprintText = true;
            startLine = true;
            if(indexPhrase < contrat.TextData.Count)
                enemyManager.SetTextData(contrat.TextData[indexPhrase]);

            if (CheckEvent() == false)
                SetPhrase();
            startLine = false;
        }

        // Affiche la phrase
        public virtual void SetPhrase()
        {
            if (indexPhrase == contrat.TextData.Count)
            {
                EndSession();
                return;
            }
            if(startLine == true)
            {
                //FeedbackNewLine();
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
            skillManager.CheckSkillCondition(actorsManager.GetCurrentActor(), "Kill", lastAttack);
        }


        public void EndSession()
        {
            StartCoroutine(EndSessionCoroutine(50));
        }

        private IEnumerator EndSessionCoroutine(int time)
        {
            StartCoroutine(FadeVolume(180));
            yield return new WaitForSeconds(1f);
            ingeSonPanel.gameObject.SetActive(true);
            cameraController.EndSequence1(0.3f, 10f);
            enemyManager.DamagePhrase();
            textAppearManager.TextPop();
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, -10);
            feedbackLine.GetComponent<Animator>().SetTrigger("FeedbackFinalLine");
            audioSourceKillPhrase.Play();
            audioSourceKillPhrase2.Play();

            endBlackScreen.SetBool("Appear", false);
            yield return new WaitForSeconds(1f);
            //ingeSonPanel.gameObject.SetActive(false);
            mcPanel.gameObject.SetActive(true);
            cameraController.EndSequence1(0.6f, -10f);
            enemyManager.DamagePhrase();
            textAppearManager.TextPop();
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, 10);
            feedbackLine.GetComponent<Animator>().SetTrigger("FeedbackFinalLine");
            audioSourceKillPhrase.Play();
            audioSourceKillPhrase2.Play();

            yield return new WaitForSeconds(1f);
            ingeSonPanel.SetTrigger("Feedback");
            mcPanel.SetTrigger("Feedback");
            /*ingeSonPanel.gameObject.SetActive(false);
            mcPanel.gameObject.SetActive(false);*/
            enemyManager.DamagePhrase();
            textAppearManager.TextPop(210);
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, 0);
            feedbackLine.GetComponent<Animator>().SetTrigger("FeedbackUltimeLine");
            audioSourceKillPhrase.Play();
            audioSourceKillPhrase2.Play();

            //StartCoroutine(FadeVolumeWithPitch(180));
            emotionAttackManager.SwitchCardTransformToRessource();
            endBlackScreen.SetBool("Appear", false);
            cameraController.EndSequence();
            audioSourceYokaiDisco.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            bandeRythmo.gameObject.SetActive(true);
            //yield return new WaitForSeconds(1.5f);
            float speedAlpha = 1f / time;
            while (time != 0)
            {
                fade.color += new Color(0, 0, 0, speedAlpha);
                time -= 1;
                yield return null;
            }
            resultScreen.SetActive(true);
            yield return new WaitForSeconds(1);
            introBlackScreen.gameObject.SetActive(true);
            textIntro.SetActive(true);
            introText.SetPhraseTextacting("We did it everyone !", 0);
            yield return new WaitForSeconds(2);
            introText.SetPhraseTextacting("Enregistrement terminé !", 0);
            yield return new WaitForSeconds(99);

        }


        private IEnumerator FadeVolume(int time)
        {
            float speed = audioSourceBattleTheme.volume / time;
            while (time != 0)
            {
                time -= 1;
                audioSourceBattleTheme.volume -= speed;
                yield return null;
            }
        }


        private IEnumerator FadeVolumeWithPitch(int time)
        {
            float speed = audioSourceBattleTheme.volume / time;
            audioSourceBattleTheme.pitch += 1;
            while (time != 0)
            {
                time -= 1;
                audioSourceBattleTheme.volume -= speed;
                yield return null;
                if(audioSourceBattleTheme.pitch >= 1)
                    audioSourceBattleTheme.pitch -= 0.01f;
            }
        }






        private void FeedbackNewLine()
        {
            if (feedbackLine == null)
                return;
            enemyManager.ResetHalo();
            textMeshLine.text = (contrat.TextData.Count - indexPhrase).ToString();
            textMeshTurn.text = turnCount.ToString();
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));
            feedbackLine.SetActive(true);

        }

        public void NewTurn()
        {
            emotionAttackManager.SwitchCardTransformToBattle();
            inputController.gameObject.SetActive(true);
            skillManager.CheckSkillCondition(actorsManager.GetCurrentActor(), "Attack", lastAttack);
        }

        private IEnumerator WaitText()
        {
            int time = 60;
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            yield return null;

            while (textAppearManager.GetEndDamage() == false)
            {
                yield return null;
            }

            // Check Si Role Attaque
            if (enemyManager.GetHpPercentage() != 0)
                roleManager.SelectAttack();

            while (textAppearManager.GetEndLine() == false)
            {
                yield return null;
            }

            // Check si on peut lancer l'attaque sinon nouveau tour pour le joueur
            if (roleManager.IsAttacking() == true)
            {
                yield return new WaitForSeconds(0.1f);
                cameraController.EnemySkill();
                roleManager.EnemyAttackActivation();
            }
            else
            {
                NewTurn();
            }
        }


        public void HideUIButton()
        {
            StartCoroutine(MoveUIButton(buttonUIA, -600));
            StartCoroutine(MoveUIButton(buttonUIY, -600));
        }

        private IEnumerator MoveUIButton(Image button, float targetY)
        {

            int time = 20;
            float speedY = (targetY - button.rectTransform.anchoredPosition.y) / time;
            while (time != 0)
            {
                button.rectTransform.anchoredPosition += new Vector2(0, speedY);
                time -= 1;
                yield return null;
            }
        }





















        // =================================================================
        // Faire un DoublageEventManager

        public virtual void PrintAllText()
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
            for(int i = 0; i < popups.Length; i++)
            {
                popups[i].SetActive(false);
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
                    if(playerData.Language == 1)
                        FindInterlocutor(node.Interlocuteur).SetPhraseTextacting(node.TextEng, node.CameraEffectID);
                    else
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
                if (currentNode is DoublageEventTutoPopup)
                {
                    DoublageEventTutoPopup node = (DoublageEventTutoPopup) currentNode;
                    popups[node.PopupID].SetActive(true);
                    inputEvent.gameObject.SetActive(true);
                }
                if (currentNode is DoublageEventSound)
                {
                    DoublageEventSound node = (DoublageEventSound)currentNode;
                    audioSourceKillPhrase.PlayOneShot(node.Audio);
                    ExecuteEvent();
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