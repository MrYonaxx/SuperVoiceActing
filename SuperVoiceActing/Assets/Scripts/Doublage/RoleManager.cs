/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class RoleManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("FeedbacksAttack")]
        [SerializeField]
        Image imageEnemy;
        [SerializeField]
        Image imageEnemyLower;
        [SerializeField]
        Image imageEnemyEffect;
        [SerializeField]
        Animator spotEnemy;
        [SerializeField]
        Animator enemyAttack;
        [SerializeField]
        Animator enemyAttackFace;

        [Title("InfoAttack")]
        [SerializeField]
        TextMeshProUGUI textSkillName;
        [SerializeField]
        TextMeshProUGUI textSkillDescription;
        [SerializeField]
        TextMeshProUGUI textSkillDescriptionBattle;
        [SerializeField]
        TextMeshProUGUI textSkillInfluenceValue;

        [SerializeField]
        InputController input;
        [SerializeField]
        EmotionAttackManager emotionAttackManager;
        [SerializeField]
        CameraController cameraController;

        [Title("Best Stat Marker")]
        [SerializeField]
        RectTransform[] positionEmotionBestStat;
        [SerializeField]
        RectTransform bestStatIcon;
        [SerializeField]
        RectTransform secondBestStatIcon;


        bool firstTime = false;
        bool readyToAttack = false;

        List<Role> roles;

        int indexCurrentRole = 0;
        int currentAttackInfluence;
        private SkillRoleData currentAttack = null;
        private SkillManager skillManager = null;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetIndexRole(int newIndex)
        {
            indexCurrentRole = newIndex;
            SetBestStatIcon();
        }

        public int GetRoleAttack()
        {
            return roles[indexCurrentRole].Attack;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void SetManagers(SkillManager manager)
        {
            skillManager = manager;
        }

        public void SetRoles(List<Role> contractRoles)
        {
            roles = contractRoles;
            if(bestStatIcon != null)
                SetBestStatIcon();
        }

        public void AddScorePerformance(int lastAttackScore, int lastBestScore)
        {
            roles[indexCurrentRole].RolePerformance += lastAttackScore;
            roles[indexCurrentRole].RoleBestScore += lastBestScore;
        }








        public bool IsAttacking()
        {
            return readyToAttack;
        }


        public bool RoleDecision(string phase, int line, int turn, float enemyHP)
        {
            for (int i = 0; i < roles[indexCurrentRole].RoleAI.Length; i++)
            {
                if (roles[indexCurrentRole].RoleAI[i].CheckEnemyAI(phase, line, turn, enemyHP))
                {
                    currentAttack = (SkillRoleData) roles[indexCurrentRole].RoleAI[i].Skills[Random.Range(0, roles[indexCurrentRole].RoleAI[i].Skills.Length)];
                    if (currentAttack != null)
                    {
                        readyToAttack = true;
                        ActivateSpot();
                    }
                    else
                    {
                        readyToAttack = false;
                    }
                    return readyToAttack;
                }
            }
            return false;
        }











        private void ActivateSpot()
        {
            if (firstTime == false)
            {
                spotEnemy.gameObject.SetActive(true);
            }
            spotEnemy.SetBool("Appear", true);
        }

        public void EnemyAttackActivation()
        {
            readyToAttack = false;
            if (firstTime == false)
            {
                enemyAttack.gameObject.SetActive(true);
                enemyAttackFace.gameObject.SetActive(true);
                firstTime = true;
            }
            enemyAttack.SetBool("Appear", true);
            enemyAttackFace.SetTrigger("Appear");
            imageEnemy.sprite = roles[indexCurrentRole].RoleSprite;
            imageEnemyLower.sprite = roles[indexCurrentRole].RoleSprite;
            imageEnemyEffect.sprite = roles[indexCurrentRole].RoleSprite;
            textSkillName.text = currentAttack.SkillName;
            textSkillDescription.text = currentAttack.Description;
            textSkillDescriptionBattle.text = currentAttack.DescriptionBattle;
            if(currentAttack.InfluenceValue > 0)
            {
                currentAttackInfluence = currentAttack.InfluenceValue;
                currentAttackInfluence += Random.Range(-currentAttack.InfluenceRandom, currentAttack.InfluenceRandom);
            }
            else
            {
                currentAttackInfluence = roles[indexCurrentRole].Defense * currentAttack.InfluenceMultiplier;
                currentAttackInfluence += Random.Range(-currentAttack.InfluenceRandom, currentAttack.InfluenceRandom);
            }
            textSkillInfluenceValue.text = currentAttackInfluence.ToString();
        }


        // Appelé via l'animator enemyAttackFace
        public void ActivateInput()
        {
            input.gameObject.SetActive(true);
        }


        public void StopEnemyActivation()
        {
            cameraController.EnemySkillCancel();
            skillManager.ApplySkill(currentAttack);
            input.gameObject.SetActive(false);
            enemyAttack.SetBool("Appear", false);
            enemyAttackFace.SetTrigger("Disappear");
            spotEnemy.SetBool("Appear", false);
        }


        public void EnemyAttackCounter()
        {
            cameraController.EnemySkillCounter();

            input.gameObject.SetActive(false);
            enemyAttackFace.SetTrigger("Counter");

            enemyAttack.SetBool("Appear", false);
            spotEnemy.SetBool("Appear", false);
        }








        public void SelectEmotion(string emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard(emotion, false);
            if (card != null)
            {
                currentAttackInfluence -= card.GetStat();
                if (currentAttackInfluence <= 0)
                {
                    //currentAttackInfluence = 0;
                    enemyAttackFace.ResetTrigger("UnSlice");
                    enemyAttackFace.SetTrigger("Slice");
                }
                textSkillInfluenceValue.text = currentAttackInfluence.ToString();
            }
        }

        public void RemoveCard()
        {
            EmotionCard card = emotionAttackManager.RemoveCard();
            if (card != null)
            {
                currentAttackInfluence += card.GetStat();
                if (currentAttackInfluence > 0)
                {
                    enemyAttackFace.ResetTrigger("Slice");
                    enemyAttackFace.SetTrigger("UnSlice");
                }
            }
            textSkillInfluenceValue.text = currentAttackInfluence.ToString();
        }

        public void Defence()
        {
            if (currentAttackInfluence > 0)
            {
                StopEnemyActivation();
            }
            else
            {
                EnemyAttackCounter();
            }
        }












        public void SetBestStatIcon()
        {
            bestStatIcon.SetParent(positionEmotionBestStat[roles[indexCurrentRole].BestStatEmotion]);
            bestStatIcon.anchoredPosition = new Vector2(0, 0);
            secondBestStatIcon.SetParent(positionEmotionBestStat[roles[indexCurrentRole].SecondBestStatEmotion]);
            secondBestStatIcon.anchoredPosition = new Vector2(0,0);
        }







        #endregion

    } // RoleManager class
	
}// #PROJECTNAME# namespace
