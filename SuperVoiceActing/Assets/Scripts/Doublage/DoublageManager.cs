﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [Header("Debug")]
        [SerializeField]
        protected ContractData contratData;

        [Title("Contrat")]
        [SerializeField]
        protected PlayerData playerData;
        [SerializeField]
        protected InitialPlayerData initialPlayerData;

        [Title("Controllers")]
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
        protected CharacterDoublageManager characterDoublageManager;
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
        [SerializeField]
        protected SessionRecapManager sessionRecapManager;
        [SerializeField]
        protected DoublageResumeManager doublageResumeManager;

        [Title("Feedback & UI")]
        [SerializeField]
        protected TimerDoublage timer;
        [SerializeField]
        protected RectTransform feedbackLine;
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

        [Title("IntroSequence")]
        [SerializeField]
        TextPerformanceAppear introText;
        [SerializeField]
        GameObject textIntro;
        [SerializeField]
        Image introBlackScreen;
        [SerializeField]
        Animator[] animatorsIntro;


        [Title("EndSequence")]
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

        [Title("UI")]
        [SerializeField]
        protected Animator buttonUIY;
        [SerializeField]
        protected Animator buttonUIB;
        [SerializeField]
        protected Animator buttonUIA;

        [Title("AudioSource")]
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



        [Title("Debug")]
        [SerializeField]
        protected int indexPhrase = 0;


        protected bool reprint = false;
        protected bool intro = true;


        protected bool startLine = true;
        protected bool endAttack = false;

        [SerializeField]
        protected Contract contrat;

        protected int turnCount = 15;
        protected int killCount = 0;


        // Y'a un truc qui va pas là, y'a doublon
        protected EmotionCard[] lastAttack = null;
        protected Emotion[] lastAttackEmotion = null;


        protected bool defencePhase = false;

        protected List<VoiceActor> battleVoiceActors = new List<VoiceActor>();


        public ToneManager ToneManager
        {
            get { return toneManager; }
        }
        public EmotionAttackManager EmotionAttackManager
        {
            get { return emotionAttackManager; }
        }
        public EnemyManager EnemyManager
        {
            get { return enemyManager; }
        }
        public ActorsManager ActorsManager
        {
            get { return actorsManager; }
        }
        public RoleManager RolesManager
        {
            get { return roleManager; }
        }

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetReprintText(bool b)
        {
            reprint = b;
        }

        public void SetEndAttack()
        {
            endAttack = true;
        }

        public void AddTurn(int addValue)
        {
            turnCount += addValue;
            if (turnCount <= 0)
                turnCount = 0;
            timer.SetTurn(turnCount);
            if (addValue < 0)
            {
                for (int i = 0; i < Mathf.Abs(addValue); i++)
                {
                    actorsManager.CheckBuffsActors();
                    actorsManager.CheckBuffsCards();
                }
            }
            actorsManager.DrawBuffIcon();
        }

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
            if(playerData.GetPlayerDebugSave() == true)
            {
                if (contratData != null)
                {
                    playerData.CreatePlayerData(initialPlayerData);
                    contrat = new Contract(contratData); // Pour Debug
                    contrat.CheckCharacterLock(playerData.VoiceActors);
                    playerData.CurrentContract = contrat;
                }
            }
            indexPhrase = contrat.CurrentLine;
            turnCount = playerData.TurnLimit;
            if (timer != null)
                timer.SetTurn(turnCount);
            if (maxLineNumber != null)
                maxLineNumber.text = (contrat.TextData.Count).ToString();
            if (currentLineNumber != null)
                currentLineNumber.text = (indexPhrase).ToString();
            StartCoroutine(IntroductionSequence());
        }

        private void InitializeManagers()
        {
            battleVoiceActors = playerData.GetVoiceActorsFromContractID(contrat);

            SoundEngineer soundEngi = playerData.GetSoundEngiFromName(contrat.SoundEngineerID);

            enemyManager.SetTextData(contrat.TextData[indexPhrase]);
            actorsManager.SetCards(emotionAttackManager.SetDeck(playerData.ComboMax, playerData.Deck));
            actorsManager.SetActors(battleVoiceActors);
            //actorsManager.ActorTakeDamage(0);
            eventManager.SetManagers(skillManager);
            skillManager.SetManagers(this, cameraController, battleVoiceActors);
            skillManager.SetCurrentVoiceActor(actorsManager.GetCurrentActor(), characterDoublageManager.GetCharacter(actorsManager.GetCurrentActorIndex()));
            producerManager.SetManagers(skillManager, contrat.ProducerMP);
            roleManager.SetManagers(skillManager);
            roleManager.SetRoles(contrat.Characters, actorsManager.GetCurrentActorIndex());
            soundEngineerManager.SetManagers(skillManager, soundEngi);
            toneManager.InitializeDefaultTone(contrat.Characters);
            toneManager.DrawTone(actorsManager.GetCurrentActorIndex());
            characterDoublageManager.SetCharactersSprites(battleVoiceActors);
            characterDoublageManager.SetCharacterForeground(actorsManager.GetCurrentActorIndex());
            SetPlayerSettings();
        }

        private void SetPlayerSettings()
        {
            int showCommand = PlayerPrefs.GetInt("ShowCommand");
            if (showCommand == 1)
            {
                buttonUIA.enabled = false;
                buttonUIB.enabled = false;
                buttonUIA.gameObject.SetActive(false);
                buttonUIB.gameObject.SetActive(false);
            }
        }

        private IEnumerator IntroductionSequence()
        {
            // ===== Initialisation ===== //
            introBlackScreen.gameObject.SetActive(true);
            inputController.gameObject.SetActive(false);
            yield return null;  // On attend une frame que les scripts soient chargés
            if (spectrum != null)
            {
                spectrum.audioSource = AudioManager.Instance.GetAudioSourceMusic();
                spectrum.enabled = true;
            }
            InitializeManagers();
            // ===== Initialisation ===== //

            if (contrat.BattleTheme != null)
            {
                audioClipBattleTheme = contrat.BattleTheme;
            }

            if (eventManager.CheckEvent(contrat, indexPhrase, startLine, enemyManager.GetHpPercentage()) == false)
            {
                yield return new WaitForSeconds(1);
                introText.NewPhrase("Semaine " + playerData.Date.week.ToString());
                //introText.NewPhrase(playerData.MonthName[playerData.Date.month -1] + "\nSemaine " + playerData.Date.week.ToString());
                yield return new WaitForSeconds(1.5f);
                introText.NewPhrase(contrat.Name);
                yield return new WaitForSeconds(3);
                textIntro.SetActive(false);
                yield return new WaitForSeconds(1f);
                introBlackScreen.gameObject.SetActive(false);
                introBlackScreen.enabled = false;
                AudioManager.Instance.PlayMusic(audioClipBattleTheme);
                AudioManager.Instance.PlaySound(audioClipSpotlight);

                if (contrat.SessionNumber > 0)
                {
                    cameraController.MoveToInitialPosition(0);
                    yield return doublageResumeManager.ResumeMainCoroutine(contrat);
                }


                for (int i = 0; i < animatorsIntro.Length; i++)
                {
                    animatorsIntro[i].enabled = true;
                }
                yield return new WaitForSeconds(1);
                cameraController.MoveToInitialPosition(300);
                yield return new WaitForSeconds(1);
                emotionAttackManager.SwitchCardTransformIntro();
                yield return new WaitForSeconds(0.5f);
                SetPhrase();
            }
            else
            {
                yield return null;
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
        // Appelé par les boutons
        public void SelectCard(int emotion)
        {
            if(defencePhase == true)
            {
                roleManager.SelectEmotion(emotion);
                return;
            }
            EmotionCard card = emotionAttackManager.SelectCard((Emotion) emotion, true);
            if (card != null)
            {
                AudioManager.Instance.PlaySound(audioClipAttack2);
                actorsManager.AddAttackDamage(roleManager.GetRoleAttack(), card.GetDamagePercentage());

                lastAttackEmotion = emotionAttackManager.GetComboEmotion();
                skillManager.UpdateMovelist(lastAttackEmotion);
                //toneManager.HighlightTone(card.GetEmotion(), true);
            }
        }

        public void SelectCard(string emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard(emotion);
            if (card != null)
            {
                AudioManager.Instance.PlaySound(audioClipAttack2);
                actorsManager.AddAttackDamage(roleManager.GetRoleAttack(), card.GetDamagePercentage());

                lastAttackEmotion = emotionAttackManager.GetComboEmotion();
                skillManager.UpdateMovelist(lastAttackEmotion);
                //toneManager.HighlightTone(card.GetEmotion(), true);
            }
        }

        public void RemoveCard()
        {
            EmotionCard card = emotionAttackManager.RemoveCard();
            if (card != null)
            {
                actorsManager.RemoveAttackDamage(roleManager.GetRoleAttack(), card.GetDamagePercentage());

                lastAttackEmotion = emotionAttackManager.GetComboEmotion();
                skillManager.UpdateMovelist(lastAttackEmotion);
                //toneManager.HighlightTone(card.GetEmotion(), false);
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
                    //SelectCard("Neutre");
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
            ShakeCurrentCharacter();
            AudioManager.Instance.PlaySound(audioClipAttack, 0.5f);
            AudioManager.Instance.PlaySound(audioClipAttack2, 0.8f);
            turnCount -= 1;
            soundEngineerManager.AddTrickery(1);
            if (timer != null)
                timer.SetTurn(turnCount);

            if (feedbackLine != null)
                feedbackLine.gameObject.SetActive(false);

            HideUIButton();

            lastAttack = emotionAttackManager.GetComboEmotionCard();
            lastAttackEmotion = emotionAttackManager.GetComboEmotion();
            textAppearManager.ExplodeLetter(enemyManager.DamagePhrase(lastAttack, 
                                                                      textAppearManager.GetWordSelected(), 
                                                                      actorsManager.GetCurrentActorDamageVariance()),
                                            lastAttack);
            //toneManager.ModifyTone(lastAttack);
            characterDoublageManager.GetCharacter(actorsManager.GetCurrentActorIndex()).ChangeEmotion(lastAttack[0].GetEmotion());
            if (actorsManager.GetCurrentActorHP() == 0)
            {
                AudioManager.Instance.StopMusic(300);
                textAppearManager.SetLetterSpeed(8);
            }

            emotionAttackManager.CardAttack();
            emotionAttackManager.SwitchCardTransformToRessource();

            skillManager.HideSkillWindow();

            roleManager.ShowHUDNextAttack(false, true);

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
            //roleManager.RoleDecision("ENDATTACK", indexPhrase, turnCount, enemyManager.GetHpPercentage());
            //roleManager.
            roleManager.ActivateRoleSpot(); // Active le spot si le role va attaquer.

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
                //yield return actorsManager.DeathCoroutine(eventManager.GetCharacterSprites(actorsManager.GetCurrentActorIndex()));
                AudioManager.Instance.PlaySound(audioClipSpotlight);
                introBlackScreen.gameObject.SetActive(true);
                introBlackScreen.enabled = true;
                introBlackScreen.color = new Color(0, 0, 0, 1);
                textIntro.SetActive(true);
                introText.NewPhrase(" ");
                yield return new WaitForSeconds(1f);
                introText.NewPhrase(actorsManager.GetCurrentActor().VoiceActorName + " ?");
                yield return new WaitForSeconds(2f);
                introText.NewPhrase(actorsManager.GetCurrentActor().VoiceActorName + " !");
                yield return new WaitForSeconds(2f);
                introText.NewPhrase(actorsManager.GetCurrentActor().VoiceActorName.ToUpper() + " !");
                yield return new WaitForSeconds(2f);
                introText.NewPhrase(" ");
                yield return new WaitForSeconds(1f);
                if (CheckGameOver() == false)
                    ShowResultScreen();
                yield break;
            }

            emotionAttackManager.ResetCard();
            emotionAttackManager.SwitchCardTransformToBattle(true);
            // Check Role Attack =================================================================
            if (roleManager.IsAttacking() == true)
            {
                yield return new WaitForSeconds(0.1f);
                cameraController.EnemySkill();
                /*roleManager.EnemyAttackActivation();
                endAttack = false;
                defencePhase = true;
                while (endAttack == false)
                {
                    yield return null;
                }
                defencePhase = false;*/
                yield return roleManager.RoleAttackCoroutine();
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
            List<SkillActiveTiming> activeTimings = new List<SkillActiveTiming> {SkillActiveTiming.AfterAttack };
            if (enemyManager.GetLastAttackCritical() == true)
                activeTimings.Add(SkillActiveTiming.AfterCritical);
            if (enemyManager.GetHpPercentage() == 0)
                activeTimings.Add(SkillActiveTiming.AfterKill);

            skillManager.CheckSkillCondition(battleVoiceActors, activeTimings, lastAttackEmotion, false);

            yield return skillManager.ActivateBigSkill();

            // Les skills se font tous check en même temps, du coup ça peut empêcher certains combos.
            //skillManager.CheckSkillCondition(actorsManager.GetCurrentActor(), activeTimings, lastAttack, true); 
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
            yield return new WaitForSeconds(frame / 60f);
            /*int time = frame;
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }*/
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


        public void NewTurn(bool reprintText = false)
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
                skillManager.ActivateMinorSkills();
                emotionAttackManager.ResetCard();
                emotionAttackManager.SwitchCardTransformToBattle();
                inputController.gameObject.SetActive(true);
                actorsManager.CheckBuffsActors();
                actorsManager.CheckBuffsCards();
                RolesManager.CheckBuffsRoles();
                roleManager.ShowHUDNextAttack(true);
                ShowUIButton(buttonUIA);
                ShowUIButton(buttonUIB);
                if (enemyManager.GetHpPercentage() == 0)
                    ShowUIButton(buttonUIY);
                else
                    roleManager.DetermineCurrentAttack();
            }

            // Reprint éventuel
            if (reprint == true || reprintText == true)
            {
                textAppearManager.ReprintText();
                textAppearManager.ApplyDamage((100-enemyManager.GetHpPercentage()));
                SetReprintText(false);
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

                    //Emotion[] emotionsUsed = { lastAttack[0].GetEmotion(), lastAttack[1].GetEmotion(), lastAttack[2].GetEmotion() };
                    contrat.EmotionsUsed.Add(new EmotionUsed(lastAttack));
                    indexPhrase += 1;
                    killCount += 1;
                    if (currentLineNumber != null)
                        currentLineNumber.text = (indexPhrase).ToString();
                    emotionAttackManager.ResetCard();
                    emotionAttackManager.StartTurnCardFeedback();
                    toneManager.ModifyTone(lastAttack, actorsManager.GetCurrentActorIndex());

                    roleManager.AddScorePerformance(enemyManager.GetLastAttackScore(), enemyManager.GetBestMultiplier());
                    soundEngineerManager.ShowCharacterShadows();
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
                    AudioManager.Instance.PlaySound(audioClipKillPhrase);
                    AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);

                    if (indexPhrase == contrat.TextData.Count)
                    {
                        FeedbackNewLine();
                        EndSession();
                        return;
                    }

                    // Check si changement d'acteur
                    if (enemyManager.CheckInterlocutor(actorsManager.GetCurrentActorIndex()) == false)
                    {
                        FeedbackNewLine();
                        SwitchActors();
                    }
                    else
                    {
                        FeedbackNewLine();
                        if (cameraController != null)
                            cameraController.NotQuite();
                        StartCoroutine(WaitCoroutineNextPhrase(60));
                    }

                    startLine = false;
                }
            }
        }

        private IEnumerator WaitCoroutineNextPhrase(float time)
        {
            yield return new WaitForSeconds(time / 60f);
            SetPhrase();
        }

        public void SwitchActors()
        {
            if (characterDoublageManager.SwitchActors(enemyManager.GetInterlocutor()) == true) // GoLeft
            {
                FeedbackNewLineSwitch(true);
            }
            else
            {
                FeedbackNewLineSwitch(false);
            }
            actorsManager.SetIndexActors(enemyManager.GetInterlocutor());
            roleManager.SetIndexRole(enemyManager.GetInterlocutor());

            emotionAttackManager.SwitchCardTransformToRessource();
            actorsManager.SetCards(emotionAttackManager.SetDeck(playerData.ComboMax, playerData.Deck));
            actorsManager.DrawActorStat();
            actorsManager.DrawBuffIcon();
            textAppearManager.SetMouth(characterDoublageManager.GetCharacter(actorsManager.GetCurrentActorIndex()));
            skillManager.SetCurrentVoiceActor(actorsManager.GetCurrentActor(), characterDoublageManager.GetCharacter(actorsManager.GetCurrentActorIndex()));
        }

        // Appelé par l'event Switch Actors
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
            StartCoroutine(SetPhraseCoroutine());
        }

        private IEnumerator SetPhraseCoroutine()
        {

            // Check Skill ========================================================================
            if (intro == true)
            {
                //lastAttack[0] = Emotion.Neutre;
                skillManager.CheckSkillCondition(battleVoiceActors, new List<SkillActiveTiming> {SkillActiveTiming.AfterStart }, lastAttackEmotion, false);
                yield return skillManager.ActivateBigSkill();
                intro = false;
                yield return new WaitForSeconds(0.5f);
            }
            // Producteur Decision ======================================================================
            if (producerManager.ProducerDecision(contrat.ArtificialIntelligence, "STARTPHRASE", indexPhrase, turnCount, enemyManager.GetHpPercentage()) == true)
            {
                producerManager.ProducerAttackActivation();
                endAttack = false;
                while (endAttack == false)
                {
                    yield return null;
                }
            }

            // Role Decision ======================================================================
            /*roleManager.RoleDecision("STARTPHRASE", indexPhrase, turnCount, enemyManager.GetHpPercentage());
            if (roleManager.IsAttacking() == true)
            {
                yield return new WaitForSeconds(0.1f);
                cameraController.EnemySkill();
                emotionAttackManager.SwitchCardTransformToBattle(true);
                roleManager.EnemyAttackActivation();    
                endAttack = false;
                emotionAttackManager.ShowComboSlot(true);
                while (endAttack == false)
                {
                    yield return null;
                }
                emotionAttackManager.ShowComboSlot(true);
            }*/
            emotionAttackManager.SwitchCardTransformToBattle();
            inputController.gameObject.SetActive(true);
            recIcon.SetActive(true);
            textAppearManager.NewPhrase(contrat.TextData[indexPhrase].Text, Emotion.Neutre, true);
            ShowUIButton(buttonUIA);
            ShowUIButton(buttonUIB);
            roleManager.ShowHUDNextAttack(true);
            roleManager.DetermineCurrentAttack();
            characterDoublageManager.DrawActorsOrder(contrat.TextData, contrat.VoiceActorsID, indexPhrase);
            toneManager.DrawTone(actorsManager.GetCurrentActorIndex());
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
            yield return new WaitForSeconds(2);
        }






        private void FeedbackNewLine()
        {
            if (feedbackLine == null)
                return;
            enemyManager.ResetHalo();
            textMeshLine.text = (contrat.TextData.Count - indexPhrase).ToString();
            textMeshTurn.text = turnCount.ToString();
            feedbackLine.anchoredPosition = new Vector2(feedbackLine.anchoredPosition.x, -50);
            feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));
            feedbackLine.gameObject.SetActive(true);
        }

        private void FeedbackNewLineSwitch(bool goLeft)
        {
            if (feedbackLine == null)
                return;
            enemyManager.ResetHalo();
            textMeshLine.text = (contrat.TextData.Count - indexPhrase).ToString();
            textMeshTurn.text = turnCount.ToString();
            feedbackLine.anchoredPosition = new Vector2(feedbackLine.anchoredPosition.x, -125);
            if (goLeft == true)
            {
                feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, -20);
            }
            else
            {
                feedbackLine.transform.localRotation = Quaternion.Euler(0, 0, 20);
            }
            feedbackLine.gameObject.SetActive(true);
        }


        public void ShowUIButton(Animator animButton)
        {
            if (animButton.enabled == false)
                return;
            animButton.gameObject.SetActive(true);
            animButton.SetTrigger("Appear");
            animButton.ResetTrigger("Disappear");
        }


        public void HideUIButton()
        {
            if (buttonUIA == null)
                return;

            if (buttonUIY.gameObject.activeInHierarchy == true)
            {
                buttonUIY.SetTrigger("Disappear");
                buttonUIY.ResetTrigger("Appear");
            }
            if (buttonUIA.enabled == true)
            {
                buttonUIA.SetTrigger("Disappear");
                buttonUIA.ResetTrigger("Appear");
            }
            if (buttonUIB.enabled == true)
            {
                buttonUIB.SetTrigger("Disappear");
                buttonUIB.ResetTrigger("Appear");
            }
        }

        public IEnumerator WaitSkillManager()
        {
            while (skillManager.InSkillAnimation() == true)
            {
                yield return null;
            }
        }



        public void ShakeCurrentCharacter()
        {
            characterDoublageManager.ShakeCharacter(actorsManager.GetCurrentActorIndex());
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
            if (soundEngineerManager.CanMixTable() == false)
                return;
            soundEngineerManager.SwitchToMixingTable();
            emotionAttackManager.ResetCard();
            //emotionAttackManager.SwitchCardTransformToRessource();
            cameraController.IngeSon();
            inputController.gameObject.SetActive(false);
            //actorsManager.HideHealthBar();
            emotionAttackManager.ShowComboSlot(false);
        }

        public void SwitchToDoublage()
        {
            soundEngineerManager.SwitchToBattle();
            emotionAttackManager.SwitchCardTransformToBattle();
            if(cameraController.enabled == true)
                cameraController.IngeSon2Cancel();
            inputController.gameObject.SetActive(true);
            recIcon.SetActive(true);
            actorsManager.ShowHealthBar();
            emotionAttackManager.ShowComboSlot(true);
        }

        public void SwitchToRecap()
        {
            emotionAttackManager.SwitchCardTransformToRessource();
            emotionAttackManager.ShowComboSlot(false);
            inputController.gameObject.SetActive(false);
            recIcon.SetActive(false);
            actorsManager.ShowHealthBar();
            sessionRecapManager.DrawContract(contrat, battleVoiceActors, indexPhrase);
        }

        public void RecapToSession()
        {
            emotionAttackManager.SwitchCardTransformToBattle();
            emotionAttackManager.ShowComboSlot(true);
            inputController.gameObject.SetActive(true);
            recIcon.SetActive(true);
            actorsManager.ShowHealthBar();
            sessionRecapManager.MenuDisappear();
        }


        public void ModifyPlayerDeck(EmotionStat addDeck, int addComboMax)
        {
            playerData.Deck.Add(addDeck);
            playerData.ComboMax += addComboMax;
            if (playerData.ComboMax >= 4)
                playerData.ComboMax = 3;
            else if (playerData.ComboMax <= 0)
                playerData.ComboMax = 1;
            ModifyDeck(addDeck);
            ModifyComboMax(addComboMax);
        }

        public void ModifyDeck(EmotionStat addDeck)
        {
            emotionAttackManager.ResetCard();
            actorsManager.SetCards(emotionAttackManager.AddDeck(addDeck));
            actorsManager.DrawActorStat();
        }

        public void ModifyComboMax(int addComboMax)
        {
            emotionAttackManager.AddComboMax(addComboMax);
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
                introText.NewPhrase("GG C'est Game Over mais on a pas d'écran de game over donc rip.");
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
            if (playerData.NextStoryEvents.Count != 0 || playerData.NextRandomEvent.Count != 0)
                resultScreenManager.ChangeEndScene("EventScene");
            resultScreen.SetActive(true);
            resultScreenManager.SetContract(contrat, battleVoiceActors);
            resultScreenManager.DrawResult(turnCount, killCount);
        }






        #endregion

    } // DoublageManager class

} // #PROJECTNAME# namespace