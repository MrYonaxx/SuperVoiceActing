/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuBattleManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        ContractData contractData;
        [SerializeField]
        TextAppearManager textAppearManager;
        [SerializeField]
        CameraController cameraController;

        [SerializeField]
        VoiceActorData voiceActorPlayer;
        [SerializeField]
        CharacterDialogueController player;

        [SerializeField]
        VoiceActorData voiceActorEnemy;
        [SerializeField]
        CharacterDialogueController enemy;

        [SerializeField]
        SeiyuuEnemyManager enemyManager;

        [SerializeField]
        CameraMovementData playerAttackCameraMovement;
        [SerializeField]
        CameraMovementData enemyAttackCameraMovement;

        [Title("ATB")]
        [SerializeField]
        SeiyuuHandManager playerHand;
        [SerializeField]
        SeiyuuHandManager enemyHand;
        [SerializeField]
        SeiyuuATBManager playerATB;
        [SerializeField]
        SeiyuuATBManager enemyATB;
        [SerializeField]
        float atbDefaultValue = 50f;


        [SerializeField]
        SeiyuuBattleIA enemyIA;

        [Title("Health")]
        [SerializeField]
        SeiyuuBattleHP timerPlayer;
        [SerializeField]
        SeiyuuBattleHP timerEnemy;
        [SerializeField]
        float playerMomentum = 50f;
        [SerializeField]
        float enemyMomentum = 50f;
        [SerializeField]
        Animator animatorMomentum;

        [Title("Screen")]
        [SerializeField]
        RawImage mainScreen;
        [SerializeField]
        Animator subScreen;
        [SerializeField]
        CameraMovementData camMovRoundPlayerWinner;
        [SerializeField]
        CameraMovementData camMovRoundEnemyWinner;
        [SerializeField]
        CameraMovementData camMovNewRound;
        [SerializeField]
        float mainScreenSpeedAppear = 20f;

        [Title("Sound")]
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

        Contract contract;
        int indexLine = 0;

        bool isAttacking = false;

        bool currentWinnerIsPlayer = true;
        int playerScore = 0;
        int enemyScore = 0;

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

        public void Start()
        {
            AudioManager.Instance.PlayMusic(audioClipBattleTheme);

            playerATB.StartATB(atbDefaultValue);
            enemyATB.StartATB(atbDefaultValue);

            contract = new Contract(contractData);

            player.SetStoryCharacterData(voiceActorPlayer.SpriteSheets);
            enemy.SetStoryCharacterData(voiceActorEnemy.SpriteSheets);

            NewPhrase();
        }


        public void NewPhrase()
        {
            timerPlayer.ReinitializeTimer();
            timerEnemy.ReinitializeTimer();
            enemyManager.SetTextData(contract.TextData[indexLine]);
            enemyIA.SetTextData(contract.TextData[indexLine]);
            textAppearManager.NewPhrase(contract.TextData[indexLine].Text, enemyManager.GetEmotionHint());
            textAppearManager.ApplyDamage(50);
            playerMomentum = 50f;
            enemyMomentum = 50f;
        }

        public void PlayerAttackPhrase()
        {
            StartCoroutine(WaitPhraseAppear(true));
        }

        public void EnemyAttackPhrase()
        {
            StartCoroutine(WaitPhraseAppear(false));
        }



        private void PlayerAttackFeedback()
        {
            timerPlayer.StopTimer();
            timerEnemy.StopTimer();
            playerATB.StartATB();
            AudioManager.Instance.PlaySound(audioClipAttack, 1f);
            AudioManager.Instance.PlaySound(audioClipAttack2, 0.8f);

            playerHand.DestroyComboCards();

            playerMomentum = enemyManager.DamagePhrase(playerHand.GetEmotions(), playerHand.GetDamage(), voiceActorPlayer.FourchetteMax);
            enemyMomentum = 100f - playerMomentum;

            CheckMomentumAnimator();

            textAppearManager.SetMouth(player);
            textAppearManager.ExplodeLetter(playerMomentum, playerHand.GetEmotions()[0]);
            if (playerMomentum > enemyMomentum)
            {
                cameraController.CameraDataMovement(playerAttackCameraMovement);
            }
        }

        private void EnemyAttackFeedback()
        {
            timerPlayer.StopTimer();
            timerEnemy.StopTimer();
            enemyATB.StartATB();
            AudioManager.Instance.PlaySound(audioClipAttack, 1f);
            AudioManager.Instance.PlaySound(audioClipAttack2, 0.8f);

            enemyHand.DestroyComboCards();

            playerMomentum = enemyManager.DamagePhrase(enemyHand.GetEmotions(), -enemyHand.GetDamage(), voiceActorEnemy.FourchetteMax);
            enemyMomentum = 100f - playerMomentum;

            CheckMomentumAnimator();

            textAppearManager.SetMouth(enemy);
            textAppearManager.ExplodeLetter(playerMomentum, enemyHand.GetEmotions()[0]);

            if (enemyMomentum > playerMomentum)
            {
                cameraController.CameraDataMovement(enemyAttackCameraMovement);
            }
        }


        private IEnumerator WaitPhraseAppear(bool isPlayer)
        {
            while (isAttacking == true)
            {
                yield return null;
            }
            isAttacking = true;

            if (isPlayer == true)
                PlayerAttackFeedback();
            else
                EnemyAttackFeedback();

            while (textAppearManager.GetEndDamage() == false)
                yield return null;
            while (textAppearManager.GetEndLine() == false)
                yield return null;

            isAttacking = false;
            CheckMomentumWinner();
        }

        private void CheckMomentumWinner()
        {
            if (playerMomentum > enemyMomentum)
            {
                timerPlayer.ActiveTimer(playerMomentum);
            }
            else if (enemyMomentum > playerMomentum)
            {
                timerEnemy.ActiveTimer(enemyMomentum);
            }
        }

        private void CheckMomentumAnimator()
        {
            if (playerMomentum > enemyMomentum)
            {
                animatorMomentum.enabled = true;
                animatorMomentum.SetBool("PlayerMomentum", true);
            }
            else if (enemyMomentum > playerMomentum)
            {
                animatorMomentum.enabled = true;
                animatorMomentum.SetBool("PlayerMomentum", false);
            }
        }





        public void EndRound(bool playerWinner)
        {
            AudioManager.Instance.PlaySound(audioClipAttack, 1f);
            AudioManager.Instance.PlaySound(audioClipAttack2, 0.8f);

            timerPlayer.StopTimer();
            timerEnemy.StopTimer();

            playerATB.StopATB();
            enemyATB.StopATB();

            playerHand.StopRound();
            enemyHand.StopRound();

            currentWinnerIsPlayer = playerWinner;
            mainScreen.gameObject.SetActive(false);
            subScreen.gameObject.SetActive(true);
            subScreen.SetBool("WinnerPlayer", playerWinner);

            textAppearManager.TextPop();
        }

        public void CameraWinnerFeedback()
        {
            if(currentWinnerIsPlayer == true)
                cameraController.CameraDataMovement(camMovRoundPlayerWinner);
            else
                cameraController.CameraDataMovement(camMovRoundEnemyWinner);
        }


        public void SetNextPhrase()
        {
            StartCoroutine(SetNextPhraseCoroutine());
        }

        private IEnumerator SetNextPhraseCoroutine()
        {
            cameraController.CameraDataMovement(camMovNewRound);
            indexLine += 1;
            NewPhrase();

            mainScreen.gameObject.SetActive(true);
            float t = 0f;
            Color colorStart = new Color(1, 1, 1, 0);
            Color colorEnd = new Color(1, 1, 1, 1);
            while (t < 1f)
            {
                t += Time.deltaTime / (mainScreenSpeedAppear / 60f);
                mainScreen.color = Color.Lerp(colorStart, colorEnd, t);
                yield return null;
            }

            subScreen.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            playerATB.AddATB(1);
            enemyATB.AddATB(1);
            playerATB.StartATB();
            enemyATB.StartATB();

        }


        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace