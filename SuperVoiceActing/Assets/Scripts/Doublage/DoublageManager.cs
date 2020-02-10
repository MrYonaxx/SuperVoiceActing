/*****************************************************************
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
        [SerializeField]
        protected TurnManager turnManager;
        [SerializeField]
        protected LineManager lineManager;

        [Title("Feedback & UI")]
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

        [Title("UI")]
        [SerializeField]
        protected Animator buttonUIY;
        [SerializeField]
        protected Animator buttonUI;

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
        protected Contract contrat;

        protected bool reprint = false;


        protected bool endAttack = false;
        protected bool skipIntro = false;


        // Tout les paramètres du combat en cours
        DoublageBattleParameter battleParameter;

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

        /*public void AddTurn(int addValue)
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
        }*/

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
            if (skipIntro == true)
                return;
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
            StartCoroutine(IntroductionSequence());
        }

        public void StartSession()
        {
            contrat = playerData.CurrentContract;
            if (playerData.GetPlayerDebugSave() == true)
            {
                if (contratData != null)
                {
                    playerData.CreatePlayerData(initialPlayerData);
                    contrat = new Contract(contratData); // Pour Debug
                    contrat.CheckCharacterLock(playerData.VoiceActors);
                    playerData.CurrentContract = contrat;
                }
            }
            skipIntro = true;
            this.gameObject.SetActive(true);
            InitializeManagers();
            ShowUI(true);
            SetPhrase();
        }

        private void InitializeManagers()
        {
            battleParameter = new DoublageBattleParameter(playerData, emotionAttackManager.SetDeck(playerData.ComboMax, playerData.Deck));
            battleParameter.SetManagers(actorsManager, enemyManager, turnManager);

            enemyManager.SetTextData(contrat.TextData[contrat.CurrentLine]);

            //actorsManager.SetActors(battleVoiceActors);
            eventManager.SetManagers(contrat.EventData);
            skillManager.SetManagers(battleParameter, cameraController);
            producerManager.SetManagers(skillManager, contrat.ProducerMP);
            roleManager.SetManagers(skillManager);
            roleManager.SetRoles(contrat.Characters, battleParameter.IndexCurrentCharacter);
            //soundEngineerManager.SetManagers(skillManager, soundEngi);
            toneManager.InitializeDefaultTone(contrat.Characters);
            toneManager.DrawTone(battleParameter.IndexCurrentCharacter);
            characterDoublageManager.SetCharactersSprites(battleParameter.VoiceActors);
            characterDoublageManager.SetCharacterForeground(battleParameter.IndexCurrentCharacter);

            skillManager.SetMovelist(battleParameter.CurrentActor(), characterDoublageManager.GetCharacter(battleParameter.IndexCurrentCharacter));

            actorsManager.DrawActorStat(battleParameter.CurrentActor(), battleParameter.Cards);
            turnManager.DrawTurn(battleParameter.Turn);
            lineManager.DrawLineNumber(battleParameter.Contract.CurrentLine);
            lineManager.DrawMaxLineNumber(battleParameter.Contract.TotalLine);

            resultScreenManager.SetManagers(contrat, battleParameter.VoiceActors, actorsManager);

            sessionRecapManager.DrawContract(contrat, battleParameter.VoiceActors, contrat.CurrentLine);

            SetPlayerSettings();
        }

        private void SetPlayerSettings()
        {
            int showCommand = PlayerPrefs.GetInt("ShowCommand");
            if (showCommand == 1)
            {
                buttonUI.enabled = false;
                buttonUI.gameObject.SetActive(false);
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

            if (eventManager.CheckEvent(contrat.CurrentLine, true, enemyManager.GetHpPercentage()) == false)
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
            else // Event
            {
                yield return null;
                textIntro.SetActive(false);
                for (int i = 0; i < animatorsIntro.Length; i++)
                {
                    animatorsIntro[i].enabled = true;
                }
                ShowUI(false);
                eventManager.ShowBlackBand(true);
                yield return new WaitForSeconds(1f);
                yield return new WaitForSeconds(1f);
                introBlackScreen.gameObject.SetActive(false);
                introBlackScreen.enabled = false;
                AudioManager.Instance.PlayMusic(audioClipBattleTheme);
                AudioManager.Instance.PlaySound(audioClipSpotlight);
                cameraController.MoveToInitialPosition(0);

                yield return eventManager.StartEvent();

                SwitchToDoublage();
                SetPhrase();

            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Card =================================================================
        // Appelé par les boutons
        public void SelectCard(int emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard((Emotion) emotion, true);
            if (card != null)
            {
                AudioManager.Instance.PlaySound(audioClipAttack2);
                actorsManager.AddAttackDamage(battleParameter.CurrentActor(), roleManager.GetRoleAttack(), card.GetDamagePercentage());
                actorsManager.AddAttackPower(battleParameter.CurrentActor(), card.GetStat());

                battleParameter.CurrentAttackEmotion = emotionAttackManager.GetComboEmotion();
                skillManager.UpdateMovelist();
                roleManager.UpdateAttack(battleParameter.CurrentAttackEmotion);
            }
        }

        public void SelectCard(string emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard(emotion);
            if (card != null)
            {
                AudioManager.Instance.PlaySound(audioClipAttack2);
                actorsManager.AddAttackDamage(battleParameter.CurrentActor(), roleManager.GetRoleAttack(), card.GetDamagePercentage());
                actorsManager.AddAttackPower(battleParameter.CurrentActor(), card.GetStat());

                battleParameter.CurrentAttackEmotion = emotionAttackManager.GetComboEmotion();
                skillManager.UpdateMovelist();
                roleManager.UpdateAttack(battleParameter.CurrentAttackEmotion);
            }
        }

        public void RemoveCard()
        {
            EmotionCard card = emotionAttackManager.RemoveCard();
            if (card != null)
            {
                actorsManager.AddAttackDamage(battleParameter.CurrentActor(), -roleManager.GetRoleAttack(), card.GetDamagePercentage());
                actorsManager.AddAttackPower(battleParameter.CurrentActor(), -card.GetStat());

                battleParameter.CurrentAttackEmotion = emotionAttackManager.GetComboEmotion();
                skillManager.UpdateMovelist();
                roleManager.UpdateAttack(battleParameter.CurrentAttackEmotion);
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
                StartCoroutine(AttackCoroutine());
            }
        }

        // Phase où l'attaque part
        public void AttackFeedbackPhase()
        {
            ShakeCurrentCharacter();
            AudioManager.Instance.PlaySound(audioClipAttack, 0.5f);
            AudioManager.Instance.PlaySound(audioClipAttack2, 0.8f);
            soundEngineerManager.AddTrickery(1);

            HideUIButton();

            battleParameter.Turn = turnManager.AdvanceTimer(battleParameter.Turn);
            actorsManager.ActorTakeDamage(battleParameter.CurrentActor());
            textAppearManager.ExplodeLetter(enemyManager.DamagePhrase(actorsManager.GetCurrentAttackPower(battleParameter.CurrentActor()),
                                                                      battleParameter.CurrentAttackEmotion, 
                                                                      textAppearManager.GetWordSelected()),
                                            emotionAttackManager.GetComboCards());
            characterDoublageManager.GetCharacter(battleParameter.IndexCurrentCharacter).ChangeEmotion(battleParameter.CurrentAttackEmotion[0]);
            battleParameter.LastAttackEmotion = emotionAttackManager.GetComboEmotion();
            if (battleParameter.CurrentActor().Hp == 0)
            {
                AudioManager.Instance.StopMusic(300);
                textAppearManager.SetLetterSpeed(8);
            }

            emotionAttackManager.CardAttack();
            emotionAttackManager.SwitchCardTransformToRessource();

            skillManager.HideSkillWindow();
            actorsManager.DrawActorStat(battleParameter.CurrentActor(), battleParameter.Cards);
            roleManager.ShowHUDNextAttack(false, true);
        }



        private IEnumerator AttackCoroutine()
        {
            inputController.gameObject.SetActive(false);
            // Check Skill =======================================================================
            skillManager.ActivateMovelist();
            //actorsManager.DrawActorStat(battleParameter.CurrentActor(), battleParameter.Cards); // Update Card attack value
            // Attack Feedback ===================================================================
            AttackFeedbackPhase();
            if (battleParameter.Turn <= 0)
            {
                StartCoroutine(WaitGameOverLine());
                yield break;
            }
            // Wait ==============================================================================
            yield return CoroutineAttack(10);
            yield return WaitFrame(60);
            yield return null;

            // Wait End Damage ===================================================================
            while (textAppearManager.GetEndDamage() == false)
                yield return null;

            // Check Role Attack =================================================================
            roleManager.ActivateRoleSpot(); // Active le spot si le role va attaquer.

            // Wait End Line =====================================================================
            while (textAppearManager.GetEndLine() == false)
                yield return null;

            // Check Dead ========================================================================
            if (battleParameter.CurrentActor().Hp == 0 && contrat.StoryEventWhenGameOver != null)
            {
                yield return eventManager.ExecuteEvent(contrat.StoryEventWhenGameOver);
                //ChangeEventPhase();
                contrat.StoryEventWhenGameOver = null;
                textAppearManager.SetLetterSpeed(2);
                yield break;
            }
            else if (battleParameter.CurrentActor().Hp == 0)
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
                introText.NewPhrase(battleParameter.CurrentActor().VoiceActorName + " ?");
                yield return new WaitForSeconds(2f);
                introText.NewPhrase(battleParameter.CurrentActor().VoiceActorName + " !");
                yield return new WaitForSeconds(2f);
                introText.NewPhrase(battleParameter.CurrentActor().VoiceActorName.ToUpper() + " !");
                yield return new WaitForSeconds(2f);
                introText.NewPhrase(" ");
                yield return new WaitForSeconds(1f);
                if (CheckGameOver() == false)
                    resultScreenManager.ShowResultScreen(playerData, battleParameter);
                yield break;
            }

            /*emotionAttackManager.ResetCard();
            emotionAttackManager.SwitchCardTransformToBattle(true);*/
            // Check Role Attack =================================================================
            if (roleManager.IsAttacking() == true)
            {
                yield return new WaitForSeconds(0.1f);
                cameraController.EnemySkill();
                yield return roleManager.RoleAttackCoroutine();
            }

            // Check Producer Attack ==============================================================
            /*if (producerManager.ProducerDecision(contrat.ArtificialIntelligence, "ENDATTACK", contrat.CurrentLine, turnCount, enemyManager.GetHpPercentage()) == true)
            {
                producerManager.ProducerAttackActivation();
                endAttack = false;
                while(endAttack == false)
                {
                    yield return null;
                }
            }*/


            // Check Event ========================================================================
            if (eventManager.CheckEvent(contrat.CurrentLine, false, enemyManager.GetHpPercentage()) == true)
            {
                //ChangeEventPhase();
                yield return eventManager.StartEvent();
            }

            // Check Skill ========================================================================
            skillManager.CheckPassiveSkillCondition(battleParameter.IndexCurrentCharacter);
            yield return skillManager.ActivatePassiveSkills(battleParameter.VoiceActors);
            skillManager.CheckBuffs(battleParameter.VoiceActors, false);

            // Les skills se font tous check en même temps, du coup ça peut empêcher certains combos.
            // New turn ===========================================================================
            yield return null;
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
        }


        private IEnumerator WaitGameOverLine()
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
                battleParameter.KillCount += 1;
                if (CheckGameOver() == false)
                    yield return EndSessionCoroutine(50);
                yield break;
            }

            if (CheckGameOver() == false)
                yield return EndSessionCoroutine(50);
        }


        public void NewTurn(bool reprintText = false)
        {
            actorsManager.ResetAttackPower();
            emotionAttackManager.ResetCard();
            emotionAttackManager.SwitchCardTransformToBattle();
            inputController.gameObject.SetActive(true);
            actorsManager.DrawActorStat(battleParameter.CurrentActor(), battleParameter.Cards);         
            ShowUIButton(buttonUI);
            if (enemyManager.GetHpPercentage() == 0)
                ShowUIButton(buttonUIY);
            else
            {
                roleManager.DetermineCurrentAttack();
                roleManager.ShowHUDNextAttack(true);
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
                    StartCoroutine(CoroutineKillPhrase());              
                }
            }
        }

        public void ForceNextLine()
        {
            contrat.CurrentLine += 1;
            enemyManager.SetTextData(contrat.TextData[contrat.CurrentLine]);
        }

        private IEnumerator CoroutineKillPhrase()
        {
            inputController.gameObject.SetActive(false);           
            contrat.EmotionsUsed.Add(new EmotionUsed(battleParameter.LastAttackEmotion));
            contrat.CurrentLine += 1;
            battleParameter.KillCount += 1;
            lineManager.DrawLineNumber(contrat.CurrentLine);
            emotionAttackManager.ResetCard();
            skillManager.ResetMovelist();
            emotionAttackManager.StartTurnCardFeedback();
            toneManager.ModifyTone(battleParameter.LastAttackEmotion, battleParameter.IndexCurrentCharacter);
            sessionRecapManager.DrawSessionLines(contrat, contrat.CurrentLine);

            roleManager.DontAttack();
            roleManager.AddScorePerformance(enemyManager.GetLastAttackScore(), enemyManager.GetBestMultiplier());
            soundEngineerManager.ShowCharacterShadows();
            HideUIButton();
            roleManager.ShowHUDNextAttack(false);
            enemyManager.ShowHPEnemy(false);

            battleParameter.LastAttackEmotion = null;

            // Nouvelle Phrase
            if (contrat.CurrentLine < contrat.TextData.Count)
                enemyManager.SetTextData(contrat.TextData[contrat.CurrentLine]);

            // Check Event
            if (eventManager.CheckEvent(contrat.CurrentLine, true, enemyManager.GetHpPercentage()) == true)
            {
                yield return eventManager.StartEvent();
            }

            // Si pas d'event anim de destruction de phrase
            enemyManager.DamagePhrase();
            textAppearManager.TextPop();
            textAppearManager.SelectWord(0);
            AudioManager.Instance.PlaySound(audioClipKillPhrase);
            AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);

            if (contrat.CurrentLine == contrat.TextData.Count)
            {
                enemyManager.ResetHalo();
                lineManager.FeedbackNewLine(battleParameter.Contract.TotalLine - battleParameter.Contract.CurrentLine);
                yield return EndSessionCoroutine(50);
                yield break;
            }

            // Check si changement d'acteur
            if (enemyManager.CheckInterlocutor(battleParameter.IndexCurrentCharacter) == false)
            {
                enemyManager.ResetHalo();
                lineManager.FeedbackNewLine(battleParameter.Contract.TotalLine - battleParameter.Contract.CurrentLine);
                SwitchActors();
            }
            else
            {
                enemyManager.ResetHalo();
                lineManager.FeedbackNewLine(battleParameter.Contract.TotalLine - battleParameter.Contract.CurrentLine);
                if (cameraController != null)
                    cameraController.NotQuite();
                yield return new WaitForSeconds(1);
                SetPhrase();
            }
        }

        public void SwitchActors()
        {
            if (characterDoublageManager.SwitchActors(enemyManager.GetInterlocutor()) == true) // GoLeft
            {
                lineManager.FeedbackNewLineSwitch(true);
            }
            else
            {
                lineManager.FeedbackNewLineSwitch(false);
            }
            battleParameter.IndexCurrentCharacter = enemyManager.GetInterlocutor();
            //actorsManager.SetIndexActors(enemyManager.GetInterlocutor());
            //roleManager.SetIndexRole(enemyManager.GetInterlocutor());

            roleManager.SetIndexRole(battleParameter.IndexCurrentCharacter);
            emotionAttackManager.SwitchCardTransformToRessource();
            actorsManager.DrawActorStat(battleParameter.CurrentActor(), battleParameter.Cards);
            actorsManager.DrawBuffIcon(battleParameter.CurrentActor());
            textAppearManager.SetMouth(characterDoublageManager.GetCharacter(battleParameter.IndexCurrentCharacter));
            skillManager.SetMovelist(battleParameter.CurrentActor(), characterDoublageManager.GetCharacter(battleParameter.IndexCurrentCharacter));
            skillManager.UnbanSkills(false);
        }

        // Appelé par l'event Switch Actors
        // Affiche la phrase
        public virtual void SetPhrase()
        {
            StartCoroutine(SetPhraseCoroutine());
        }

        private IEnumerator SetPhraseCoroutine()
        {
            if (contrat.CurrentLine == contrat.TextData.Count)
            {
                yield return EndSessionCoroutine(50);
                yield break;
            }
            // Check Skill ========================================================================
            /*if (intro == true)
            {
                //lastAttack[0] = Emotion.Neutre;
                //skillManager.CheckSkillCondition(battleVoiceActors, new List<SkillActiveTiming> {SkillActiveTiming.AfterStart }, lastAttackEmotion, false);
                yield return skillManager.ActivateBigSkill();
                intro = false;
                yield return new WaitForSeconds(0.5f);
            }*/
            // Producteur Decision ======================================================================
            /*if (producerManager.ProducerDecision(contrat.ArtificialIntelligence, "STARTPHRASE", contrat.CurrentLine, turnCount, enemyManager.GetHpPercentage()) == true)
            {
                producerManager.ProducerAttackActivation();
                endAttack = false;
                while (endAttack == false)
                {
                    yield return null;
                }
            }*/
            skillManager.UnbanSkills(true);
            skillManager.CheckPassiveSkillCondition(battleParameter.IndexCurrentCharacter);
            yield return skillManager.ActivatePassiveSkills(battleParameter.VoiceActors);
            skillManager.CheckBuffs(battleParameter.VoiceActors, true);

            emotionAttackManager.SwitchCardTransformToBattle();
            inputController.gameObject.SetActive(true);
            recIcon.SetActive(true);
            textAppearManager.NewPhrase(contrat.TextData[contrat.CurrentLine].Text, Emotion.Neutre, true);
            ShowUIButton(buttonUI);
            roleManager.ShowHUDNextAttack(true);
            //roleManager.DetermineCurrentAttack();
            characterDoublageManager.DrawActorsOrder(contrat.TextData, contrat.VoiceActorsID, contrat.CurrentLine);
            toneManager.DrawTone(battleParameter.IndexCurrentCharacter);
            enemyManager.ShowHPEnemy(true);
            actorsManager.DrawActorStat(battleParameter.CurrentActor(), battleParameter.Cards);
            //actorsManager.DrawBuffIcon(battleParameter.CurrentActor());
        }











        // =========================================================================================================
        // END SESSION

        private IEnumerator EndSessionCoroutine(int time)
        {
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
            lineManager.FeedbackFinalLine(-10);
            AudioManager.Instance.PlaySound(audioClipKillPhrase);
            AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);

            endBlackScreen.SetBool("Appear", false);
            yield return new WaitForSeconds(1f);
            mcPanel.gameObject.SetActive(true);
            cameraController.EndSequence1(0.6f, -10f);
            enemyManager.DamagePhrase();
            textAppearManager.TextPop();
            lineManager.FeedbackFinalLine(10);
            AudioManager.Instance.PlaySound(audioClipKillPhrase);
            AudioManager.Instance.PlaySound(audioClipKillPhrase2, 0.75f);

            yield return new WaitForSeconds(1f);
            ingeSonPanel.SetTrigger("Feedback");
            mcPanel.SetTrigger("Feedback");
            enemyManager.DamagePhrase();
            textAppearManager.TextPop(210);
            lineManager.FeedbackFinalLine(0);
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
            resultScreenManager.ShowResultScreen(playerData, battleParameter);
            yield return new WaitForSeconds(1);
            introBlackScreen.gameObject.SetActive(true);
            textIntro.SetActive(true);
            yield return new WaitForSeconds(2);
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
            if (buttonUI == null)
                return;

            if (buttonUIY.gameObject.activeInHierarchy == true)
            {
                buttonUIY.SetTrigger("Disappear");
                buttonUIY.ResetTrigger("Appear");
            }
            if (buttonUI.enabled == true)
            {
                buttonUI.SetTrigger("Disappear");
                buttonUI.ResetTrigger("Appear");
            }
        }



        public void ShakeCurrentCharacter()
        {
            characterDoublageManager.ShakeCharacter(battleParameter.IndexCurrentCharacter);
        }


        public void ShowUI(bool b)
        {
            inputController.gameObject.SetActive(b);
            emotionAttackManager.ShowComboSlot(b);
            actorsManager.ShowHealthBar(b);
            enemyManager.ShowHPEnemy(b);
            if(b == false)
                emotionAttackManager.SwitchCardTransformToRessource();
            else
                emotionAttackManager.SwitchCardTransformToBattle();
        }

        public void SwitchToMixingTable()
        {
            if (soundEngineerManager.CanMixTable() == false)
                return;
            soundEngineerManager.SwitchToMixingTable();
            emotionAttackManager.ResetCard();
            //battleParameter.CurrentAttackEmotion = emotionAttackManager.GetComboEmotion();
            skillManager.UpdateMovelist();
            //emotionAttackManager.SwitchCardTransformToRessource();
            cameraController.IngeSon();
            inputController.gameObject.SetActive(false);
            emotionAttackManager.ShowComboSlot(false);
        }

        public void SwitchToDoublage()
        {
            soundEngineerManager.SwitchToBattle();
            if(cameraController.enabled == true)
                cameraController.IngeSon2Cancel();
            ShowUI(true);
        }

        /*public void SwitchToRecap()
        {
            emotionAttackManager.SwitchCardTransformToRessource();
            emotionAttackManager.ShowComboSlot(false);
            inputController.gameObject.SetActive(false);
            //recIcon.SetActive(false);
            actorsManager.ShowHealthBar(true);
            //sessionRecapManager.DrawContract(contrat, battleVoiceActors);
        }*/

        /*public void RecapToSession()
        {
            emotionAttackManager.SwitchCardTransformToBattle();
            emotionAttackManager.ShowComboSlot(true);
            inputController.gameObject.SetActive(true);
            //recIcon.SetActive(true);
            actorsManager.ShowHealthBar(true);
            //sessionRecapManager.MenuDisappear();
        }*/


        /*public void ModifyPlayerDeck(EmotionStat addDeck, int addComboMax)
        {
            playerData.Deck.Add(addDeck);
            playerData.ComboMax += addComboMax;
            if (playerData.ComboMax >= 4)
                playerData.ComboMax = 3;
            else if (playerData.ComboMax <= 0)
                playerData.ComboMax = 1;
            ModifyDeck(addDeck);
            ModifyComboMax(addComboMax);
        }*/

        /*public void ModifyDeck(EmotionStat addDeck)
        {
            emotionAttackManager.ResetCard();
            actorsManager.SetCards(emotionAttackManager.AddDeck(addDeck));
            actorsManager.DrawActorStat();
        }*/

        /*public void ModifyComboMax(int addComboMax)
        {
            emotionAttackManager.AddComboMax(addComboMax);
        }*/


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
            battleParameter.KillCount += 4;
            resultScreenManager.ShowResultScreen(playerData, battleParameter);
        }




        #endregion

    } // DoublageManager class

} // #PROJECTNAME# namespace