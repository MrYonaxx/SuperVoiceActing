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
        protected ToneManager toneManager;
        [SerializeField]
        protected EnemyManager enemyManager;
        [SerializeField]
        protected ActorsManager actorsManager;
        [SerializeField]
        protected RoleManager roleManager;
        [SerializeField]
        protected ProducerManager producerManager;
        [SerializeField]
        protected SkillManager skillManager;
        [SerializeField]
        protected ResultScreen resultScreenManager;
        [SerializeField]
        protected TextAppearManager textAppearManager;
        [SerializeField]
        protected DoublageEventManager eventManager;
        [SerializeField]
        protected SoundEngineerManager soundEngineerManager;

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

        [Header("IntroSequence")]
        [SerializeField]
        CharacterDialogueController introText;
        [SerializeField]
        GameObject textIntro;
        [SerializeField]
        Image introBlackScreen;
        [SerializeField]
        Animator[] animatorsIntro;


        [Header("EndSequence")]
        [SerializeField]
        Animator endBlackScreen;
        [SerializeField]
        Animator bandeRythmo;

        [SerializeField]
        Animator mcPanel;
        [SerializeField]
        Animator ingeSonPanel;
        [SerializeField]
        GameObject resultScreen;

        [Header("UI")]
        [SerializeField]
        protected Animator buttonUIY;
        [SerializeField]
        protected Animator buttonUIB;
        [SerializeField]
        protected Animator buttonUIA;

        [Header("AudioSource")]
        [SerializeField]
        protected SimpleSpectrum spectrum;
        [SerializeField]
        protected AudioClip audioClipBattleTheme;
        [SerializeField]
        protected AudioClip audioClipYokaiDisco;
        [SerializeField]
        protected AudioClip audioClipKillPhrase;
        [SerializeField]
        protected AudioClip audioClipKillPhrase2;
        [SerializeField]
        protected AudioClip audioClipAttack;
        [SerializeField]
        protected AudioClip audioClipAttack2;
        [SerializeField]
        protected AudioClip audioClipSpotlight;



        [Header("Debug")]
        [SerializeField]
        protected int indexPhrase = 0;






        protected bool intro = true;


        protected bool startLine = true;
        protected bool endAttack = false;

        protected Contract contrat;

        protected int turnCount = 15;
        protected int killCount = 0;

        protected EmotionCard[] lastAttack = null;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetEndAttack()
        {
            endAttack = true;
        }

        public void AddTurn(int addValue)
        {
            turnCount += addValue;
            timer.SetTurn(turnCount);
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */


        public void CheckCharacterLock(Contract newContract)
        {
            for (int i = 0; i < newContract.Characters.Count; i++)
            {
                if (newContract.Characters[i].CharacterLock != null)
                {
                    // On cherche l'acteur dans le monde
                    for (int j = 0; j < playerData.VoiceActors.Count; j++)
                    {
                        if (newContract.Characters[i].CharacterLock.Name == playerData.VoiceActors[j].Name)
                        {
                            newContract.VoiceActors[i] = playerData.VoiceActors[j];
                            continue;
                        }
                    }
                    // Si l'acteur n'est pas dans la liste, on l'invoque
                    if (newContract.VoiceActors[i] == null)
                    {
                        playerData.VoiceActors.Add(new VoiceActor(newContract.Characters[i].CharacterLock));
                        newContract.VoiceActors[i] = playerData.VoiceActors[playerData.VoiceActors.Count - 1];
                    }
                }
            }
        }





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
                    playerData.CreateList();
                    contrat = new Contract(contratData); // Pour Debug
                    CheckCharacterLock(contrat);
                    playerData.CurrentContract = contrat;
                }
            }
            indexPhrase = contrat.CurrentLine;
            if (timer != null)
                timer.SetTurn(turnCount);
            if (maxLineNumber != null)
                maxLineNumber.text = (contrat.TextData.Count).ToString();
            if (currentLineNumber != null)
                currentLineNumber.text = (indexPhrase).ToString();
            StartCoroutine(IntroductionSequence());
        }


        private IEnumerator IntroductionSequence()
        {
            // On attend une frame que les scripts soient chargés

            // Initialisation
            introBlackScreen.gameObject.SetActive(true);
            inputController.gameObject.SetActive(false);
            yield return null;
            if (spectrum != null)
            {
                spectrum.audioSource = AudioManager.Instance.GetAudioSourceMusic();
                spectrum.enabled = true;
            }
            enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            //enemyManager.SetVoiceActor(contrat.VoiceActors[0]);
            actorsManager.SetActors(contrat.VoiceActors);
            actorsManager.ActorTakeDamage(0);
            eventManager.SetCharactersSprites(contrat.VoiceActors);
            emotionAttackManager.SetDeck(playerData.ComboMax, playerData.Deck);
            skillManager.SetManagers(this, cameraController, emotionAttackManager, actorsManager, roleManager, enemyManager);
            producerManager.SetManagers(skillManager, contrat.ProducerMP);
            roleManager.SetRoles(contrat.Characters);
            toneManager.DrawTone();
            // Initialisation

            if(contrat.BattleTheme != null)
            {
                audioClipBattleTheme = contrat.BattleTheme;
            }



            if (eventManager.CheckEvent(contrat, indexPhrase, startLine, enemyManager.GetHpPercentage()) == false)
            {
                yield return new WaitForSeconds(1);
                introText.SetPhraseTextacting(playerData.MonthName[playerData.Date.month -1] + "\nSemaine " + playerData.Date.week.ToString());
                yield return new WaitForSeconds(1.5f);
                introText.SetPhraseTextacting(contrat.Name);
                yield return new WaitForSeconds(3);
                textIntro.SetActive(false);
                yield return new WaitForSeconds(1f);
                introBlackScreen.gameObject.SetActive(false);
                introBlackScreen.enabled = false;
                AudioManager.Instance.PlayMusic(audioClipBattleTheme);
                for (int i = 0; i < animatorsIntro.Length; i++)
                {
                    animatorsIntro[i].enabled = true;
                }
                AudioManager.Instance.PlaySound(audioClipSpotlight);
                yield return new WaitForSeconds(1);
                cameraController.MoveToInitialPosition(300);
                yield return new WaitForSeconds(1);
                emotionAttackManager.SwitchCardTransformIntro();
                yield return new WaitForSeconds(0.5f);
                SetPhrase();
            }
            else
            {
                textIntro.SetActive(false);
                ChangeEventPhase();
                yield return new WaitForSeconds(1f);
                yield return new WaitForSeconds(1f);
                introBlackScreen.gameObject.SetActive(false);
                introBlackScreen.enabled = false;
                AudioManager.Instance.PlayMusic(audioClipBattleTheme);
                for (int i = 0; i < animatorsIntro.Length; i++)
                {
                    animatorsIntro[i].enabled = true;
                }
                AudioManager.Instance.PlaySound(audioClipSpotlight);
                cameraController.MoveToInitialPosition(0);
                yield return new WaitForSeconds(1);

            }
            startLine = false;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Card =================================================================
        public void SelectCard(string emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard(emotion);
            if (card != null)
            {
                AudioManager.Instance.PlaySound(audioClipAttack2);
                actorsManager.AddAttackDamage(roleManager.GetRoleAttack(), card.GetDamagePercentage());
                toneManager.HighlightTone(card.GetEmotion(), true);
            }
        }

        public void RemoveCard()
        {
            EmotionCard card = emotionAttackManager.RemoveCard();
            if (card != null)
            {
                actorsManager.RemoveAttackDamage(roleManager.GetRoleAttack(), card.GetDamagePercentage());
                toneManager.HighlightTone(card.GetEmotion(), false);
            }
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ATTACK =================================================================
        // Tape la phrase si les pv sont pas 0
        public void Attack()
        {
            if (textAppearManager.GetEndLine() == true)
            {
                if (emotionAttackManager.GetComboCount() == -1)
                {
                    SelectCard("Neutre");
                    return;
                }
                HideUIButton();
                StartCoroutine(AttackCoroutine());

            }
        }

        // Phase où l'attaque part
        private void AttackFeedbackPhase()
        {
            actorsManager.ActorAttackDamage();
            AudioManager.Instance.PlaySound(audioClipAttack, 0.5f);
            AudioManager.Instance.PlaySound(audioClipAttack2, 0.8f);
            turnCount -= 1;
            if (timer != null)
                timer.SetTurn(turnCount);

            if (feedbackLine != null)
                feedbackLine.SetActive(false);

            HideUIButton();

            lastAttack = emotionAttackManager.GetComboEmotionCard();
            textAppearManager.ExplodeLetter(enemyManager.DamagePhrase(lastAttack, 
                                                                      textAppearManager.GetWordSelected(), 
                                                                      actorsManager.GetCurrentActorDamageVariance()),
                                            lastAttack);
            if(actorsManager.GetCurrentActorHP() == 0)
            {
                AudioManager.Instance.StopMusic(300);
                textAppearManager.SetLetterSpeed(12);
            }

            emotionAttackManager.CardAttack();
            emotionAttackManager.SwitchCardTransformToRessource();

        }





        private IEnumerator AttackCoroutine()
        {
            inputController.gameObject.SetActive(false);
            // Check Skill =======================================================================


            // Attack Feedback ===================================================================
            if (turnCount == 0)
            {
                AttackFeedbackPhase();
                StartCoroutine(WaitTextLastLine());
                yield break;
            }
            AttackFeedbackPhase();
            // Wait ==============================================================================
            yield return CoroutineAttack(10);
            yield return WaitFrame(60);
            yield return null;

            // Wait End Damage ===================================================================
            while (textAppearManager.GetEndDamage() == false)
            {
                yield return null;
            }

            // Check Role Attack =================================================================


            // Wait End Line =====================================================================
            while (textAppearManager.GetEndLine() == false)
            {
                yield return null;
            }

            // Check Dead ========================================================================
            if (actorsManager.GetCurrentActorHP() == 0 && contrat.StoryEventWhenGameOver != null)
            {
                eventManager.StartEvent(contrat.StoryEventWhenGameOver);
                startLine = false;
                ChangeEventPhase();
                contrat.StoryEventWhenGameOver = null;
                textAppearManager.SetLetterSpeed(2);
                yield break;
            }
            if (actorsManager.GetCurrentActorHP() == 0)
            {
                yield return new WaitForSeconds(1f);
                yield return actorsManager.DeathCoroutine(eventManager.GetCharacterSprites(actorsManager.GetCurrentActorIndex()));
                AudioManager.Instance.PlaySound(audioClipSpotlight);
                introBlackScreen.gameObject.SetActive(true);
                introBlackScreen.enabled = true;
                introBlackScreen.color = new Color(0, 0, 0, 1);
                textIntro.SetActive(true);
                introText.SetPhraseTextacting(" ");
                yield return new WaitForSeconds(1f);
                introText.SetPhraseTextacting(actorsManager.GetCurrentActor().Name + " ?");
                yield return new WaitForSeconds(2f);
                introText.SetPhraseTextacting(actorsManager.GetCurrentActor().Name + " !");
                yield return new WaitForSeconds(2f);
                introText.SetPhraseTextacting(actorsManager.GetCurrentActor().Name.ToUpper() + " !");
                yield return new WaitForSeconds(2f);
                introText.SetPhraseTextacting(" ");
                yield return new WaitForSeconds(1f);
                if (CheckGameOver() == false)
                    ShowResultScreen();
                yield break;
            }

            // Check Role Attack =================================================================
            if (roleManager.IsAttacking() == true)
            {
                yield return new WaitForSeconds(0.1f);
                cameraController.EnemySkill();
                roleManager.EnemyAttackActivation();
                endAttack = false;
                while (endAttack == false)
                {
                    yield return null;
                }
            }

            // Check Producer Attack ==============================================================
            if (producerManager.ProducerDecision(contrat.ArtificialIntelligence, "ENDATTACK", indexPhrase, turnCount, enemyManager.GetHpPercentage()) == true)
            {
                producerManager.ProducerAttackActivation();
                endAttack = false;
                while(endAttack == false)
                {
                    yield return null;
                }
            }

            // Check Skill ========================================================================
            if(skillManager.CheckSkillCondition(actorsManager.GetCurrentActor(), "Attack", lastAttack) == true)
            {
                while (skillManager.InSkillAnimation() == true)
                {
                    yield return null;
                }
            }
            if (enemyManager.GetLastAttackCritical() == true)
            {
                if (skillManager.CheckSkillCondition(actorsManager.GetCurrentActor(), "Critical", lastAttack) == true)
                {
                    while (skillManager.InSkillAnimation() == true)
                    {
                        yield return null;
                    }
                }
            }


            // New turn ===========================================================================
            NewTurn();
        }






        private IEnumerator CoroutineAttack(int time)
        {
            if (cameraController == null)
            {
                yield break;
            }
            cameraController.ChangeOrthographicSize(-2, time);
            yield return WaitFrame(time);
            cameraController.ChangeOrthographicSize(4, time*2);
            yield return WaitFrame(time * 2);
            cameraController.ChangeOrthographicSize(0, time*6);
        }

        private IEnumerator WaitFrame(int frame)
        {
            int time = frame;
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
        }


        private IEnumerator WaitTextLastLine()
        {
            AudioManager.Instance.StopMusicWithScratch(120);
            yield return CoroutineAttack(10);
            yield return WaitFrame(60);
            yield return null;
            while (textAppearManager.GetEndLine() == false)
            {
                yield return null;
            }
            if (enemyManager.GetHpPercentage() == 0)
            {
                killCount += 1;
                if (CheckGameOver() == false)
                    EndSession();
                yield break;
            }

            if (CheckGameOver() == false)
                EndSession();
        }


        public void NewTurn(bool reprint = false)
        {
            // Check Event
            if (eventManager.CheckEvent(contrat, indexPhrase, startLine, enemyManager.GetHpPercentage()) == true)
            {
                if (eventManager.CheckStopSession() == true)
                {
                    startLine = false;
                    ChangeEventPhase();
                    return;
                }
            }
            else // Nouveau tour
            {
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                emotionAttackManager.RemoveCard();
                emotionAttackManager.SwitchCardTransformToBattle();
                inputController.gameObject.SetActive(true);
                actorsManager.CheckBuffs();
                ShowUIButton(buttonUIA);
                ShowUIButton(buttonUIB);
                if (enemyManager.GetHpPercentage() == 0)
                    ShowUIButton(buttonUIY);
            }

            // Reprint éventuel
            if (skillManager.CheckReprintTextEnemy() == true || reprint == true)
            {
                //textAppearManager.CalculateDamageColor(enemyManager.GetHpPercentage());
                //Debug.Log(enemyManager.GetHpPercentage());
                textAppearManager.ReprintText();
                textAppearManager.ApplyDamage((100-enemyManager.GetHpPercentage()));
            }
        }













        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Tue la phrase si les pv sont a 0
        public void KillPhrase()
        {
            if (enemyManager.GetHpPercentage() == 0)
            {
                if (textAppearManager.GetEndLine() == true)
                {
                    inputController.gameObject.SetActive(false);

                    
                    indexPhrase += 1;
                    killCount += 1;
                    if (currentLineNumber != null)
                        currentLineNumber.text = (indexPhrase).ToString();
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    emotionAttackManager.RemoveCard();
                    toneManager.ModifyTone(lastAttack);
                    roleManager.AddScorePerformance(enemyManager.GetLastAttackScore(), enemyManager.GetBestMultiplier());
                    HideUIButton();

                    // Nouvelle Phrase
                    startLine = true;
                    if (indexPhrase < contrat.TextData.Count)
                        enemyManager.SetTextData(contrat.TextData[indexPhrase]);

                    // Check Event
                    if (eventManager.CheckEvent(contrat, indexPhrase, startLine, enemyManager.GetHpPercentage()) == true)
                    {
                        if (eventManager.CheckStopSession() == true)
                        {
                            ChangeEventPhase();
                            return;
                        }
                    }

                    // Si pas d'event anim de destruction de phrase
                    enemyManager.DamagePhrase();
                    textAppearManager.TextPop();
                    textAppearManager.SelectWord(0);
                    FeedbackNewLine();
                    if (cameraController != null && indexPhrase < contrat.TextData.Count)
                        cameraController.NotQuite();

                    AudioManager.Instance.PlaySound(audioClipKillPhrase);
                    AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);
                    if (indexPhrase == contrat.TextData.Count)
                    {

                        EndSession();
                        return;
                    }
                    StartCoroutine(WaitCoroutineNextPhrase(60));
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


        // Affiche la phrase
        public virtual void SetPhrase()
        {
            // Check Event après le check event de kill phrase
            if (eventManager.CheckEvent(contrat, indexPhrase, startLine, enemyManager.GetHpPercentage()) == true)
            {
                if (eventManager.CheckStopSession() == true)
                {
                    ChangeEventPhase();
                    return;
                }
            }
            startLine = false;

            if (indexPhrase == contrat.TextData.Count)
            {
                EndSession();
                return;
            }
            inputController.gameObject.SetActive(true);
            recIcon.SetActive(true);
            textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text, Emotion.Neutre, true);
            ShowUIButton(buttonUIA);
            ShowUIButton(buttonUIB);
            if (intro == true)
            {
                //lastAttack[0] = Emotion.Neutre;
                skillManager.CheckSkillCondition(actorsManager.GetCurrentActor(), "Start", lastAttack);
                intro = false;
            }
            /*{
                while (skillManager.InSkillAnimation() == true)
                {
                    yield return null;
                }
            }*/
            //skillManager.CheckSkillCondition(actorsManager.GetCurrentActor(), "Kill", lastAttack);
        }













        // =========================================================================================================
        // END SESSION
        public void EndSession()
        {
            StartCoroutine(EndSessionCoroutine(50));
        }

        private IEnumerator EndSessionCoroutine(int time)
        {
            actorsManager.ResetStat();
            if (contrat.VictoryTheme == null)
            {
                AudioManager.Instance.StopMusic(180);
            }
            //StartCoroutine(FadeVolume(180));
            yield return new WaitForSeconds(1f);
            ingeSonPanel.gameObject.SetActive(true);
            cameraController.EndSequence1(0.3f, 10f);
            enemyManager.DamagePhrase();
            textAppearManager.TextPop();
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, -10);
            feedbackLine.GetComponent<Animator>().SetTrigger("FeedbackFinalLine");
            AudioManager.Instance.PlaySound(audioClipKillPhrase);
            AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);

            endBlackScreen.SetBool("Appear", false);
            yield return new WaitForSeconds(1f);
            mcPanel.gameObject.SetActive(true);
            cameraController.EndSequence1(0.6f, -10f);
            enemyManager.DamagePhrase();
            textAppearManager.TextPop();
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, 10);
            feedbackLine.GetComponent<Animator>().SetTrigger("FeedbackFinalLine");
            AudioManager.Instance.PlaySound(audioClipKillPhrase);
            AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);

            yield return new WaitForSeconds(1f);
            ingeSonPanel.SetTrigger("Feedback");
            mcPanel.SetTrigger("Feedback");
            enemyManager.DamagePhrase();
            textAppearManager.TextPop(210);
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, 0);
            feedbackLine.GetComponent<Animator>().SetTrigger("FeedbackUltimeLine");
            AudioManager.Instance.PlaySound(audioClipKillPhrase);
            AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);

            emotionAttackManager.SwitchCardTransformToRessource();
            endBlackScreen.SetBool("Appear", false);
            cameraController.EndSequence();
            if (contrat.VictoryTheme == null)
            {
                AudioManager.Instance.PlayMusic(audioClipYokaiDisco);
            }
            //audioSourceYokaiDisco.gameObject.SetActive(true);
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
            ShowResultScreen();
            yield return new WaitForSeconds(1);
            introBlackScreen.gameObject.SetActive(true);
            textIntro.SetActive(true);
            //introText.SetPhraseTextacting("We did it everyone !");
            yield return new WaitForSeconds(2);
            //introText.SetPhraseTextacting("Enregistrement terminé !");
            //yield return new WaitForSeconds(99);

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


        public void ShowUIButton(Animator animButton)
        {
            animButton.gameObject.SetActive(true);
            animButton.SetTrigger("Appear");
            animButton.ResetTrigger("Disappear");
        }


        public void HideUIButton()
        {
            if (buttonUIA == null)
                return;
            buttonUIY.SetTrigger("Disappear");
            buttonUIY.ResetTrigger("Appear");
            buttonUIA.SetTrigger("Disappear");
            buttonUIA.ResetTrigger("Appear");
            buttonUIB.SetTrigger("Disappear");
            buttonUIB.ResetTrigger("Appear");
        }

        /*private IEnumerator MoveUIButton(Image button, float targetY)
        {

            int time = 20;
            float speedY = (targetY - button.rectTransform.anchoredPosition.y) / time;
            while (time != 0)
            {
                button.rectTransform.anchoredPosition += new Vector2(0, speedY);
                time -= 1;
                yield return null;
            }
        }*/

        public IEnumerator WaitSkillManager()
        {
            while (skillManager.InSkillAnimation() == true)
            {
                yield return null;
            }
        }





        protected void ChangeEventPhase()
        {
            if(eventManager.CheckStopSession() == false)
            {
                return;
            }
            emotionAttackManager.RemoveCard();
            emotionAttackManager.RemoveCard();
            emotionAttackManager.RemoveCard();
            emotionAttackManager.SwitchCardTransformToRessource();
            recIcon.SetActive(false);
            inputController.gameObject.SetActive(false);
            actorsManager.HideHealthBar();
        }


        public void SwitchToMixingTable()
        {
            soundEngineerManager.SwitchToMixingTable();
            emotionAttackManager.SwitchCardTransformToRessource();
            cameraController.IngeSon();
            inputController.gameObject.SetActive(false);
        }

        public void SwitchToDoublage()
        {
            soundEngineerManager.SwitchToEmotion();
            emotionAttackManager.SwitchCardTransformToBattle();
            if(cameraController.enabled == true)
                cameraController.IngeSon2Cancel();
            inputController.gameObject.SetActive(true);
            recIcon.SetActive(true);
            actorsManager.ShowHealthBar();
        }


        public void ModifyDeck(EmotionStat addDeck, int addComboMax)
        {
            playerData.Deck.Add(addDeck);
            playerData.ComboMax += addComboMax;
            if (playerData.ComboMax >= 4)
                playerData.ComboMax = 3;
            else if (playerData.ComboMax <= 0)
                playerData.ComboMax = 1;
            emotionAttackManager.ModifiyDeck(addDeck, playerData.ComboMax);
        }


        public void ForceSkill(SkillActorData skill)
        {
            skillManager.SetSkillText(actorsManager.GetCurrentActor(), skill);
            skillManager.ActorSkillFeedback();
            skillManager.ApplySkill(skill);
        }

        public void ForceSkill(SkillData skill)
        {
            skillManager.ApplySkill(skill);
        }

        public void ChangeResultScreenEndScene()
        {

        }


        public bool CheckGameOver()
        {
            if (contrat.CanGameOver == true)
            {
                // Game over
                introBlackScreen.gameObject.SetActive(true);
                introText.gameObject.SetActive(true);
                introText.SetPhraseTextacting("GG C'est Game Over mais on a pas d'écran de game over donc rip.");
                return true;
            }
            else
                return false;
        }




        public void ShowResultScreen()
        {
            actorsManager.ResetStat();
            if (indexPhrase == playerData.CurrentContract.TotalLine)
            {
                if (playerData.CurrentContract.StoryEventWhenEnd != null)
                    playerData.NextStoryEvents.Add(playerData.CurrentContract.StoryEventWhenEnd);
            }
            if (playerData.NextStoryEvents.Count != 0)
                resultScreenManager.ChangeEndScene("EventScene");
            resultScreen.SetActive(true);
            resultScreenManager.SetContract(contrat);
            resultScreenManager.DrawResult(turnCount, killCount);
        }






        #endregion

    } // DoublageManager class

} // #PROJECTNAME# namespace