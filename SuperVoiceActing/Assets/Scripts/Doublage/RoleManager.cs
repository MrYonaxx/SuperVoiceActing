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


        [Space]
        [SerializeField]
        Image[] currentRoleBuff;
        [SerializeField]
        TextMeshProUGUI[] currentRoleBuffTimer;

        bool firstTime = false;
        bool readyToAttack = false;

        List<Role> roles;

        int indexCurrentRole = 0;
        int currentAttackInfluence;
        private SkillRoleData currentAttack = null;
        private SkillManager skillManager = null;

        int[] roleInfluenceBonus = { 0, 0, 0 };

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

        public List<Buff> GetBuffList()
        {
            return roles[indexCurrentRole].Buffs;
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


        public int GetCurrentRoleDefense()
        {
            return roles[indexCurrentRole].Defense;
        }

        public void AddRoleBonus(int value)
        {
            roleInfluenceBonus[indexCurrentRole] += value;
        }





        public bool AddBuff(Buff buff)
        {
            for (int i = 0; i < roles[indexCurrentRole].Buffs.Count; i++)
            {
                if (roles[indexCurrentRole].Buffs[i].SkillEffectbuff == buff.SkillEffectbuff)
                {
                    if (buff.BuffData.CanAddMultiple == false)
                    {
                        if (buff.BuffData.Refresh == true)
                        {
                            roles[indexCurrentRole].Buffs[i].Turn = buff.BuffData.TurnActive;
                        }
                        else if (buff.BuffData.AddBuffTurn == true)
                        {
                            roles[indexCurrentRole].Buffs[i].Turn += buff.BuffData.TurnActive;
                        }
                        return false;
                    }
                }
            }
            roles[indexCurrentRole].Buffs.Add(buff);
            DrawBuffIcon();
            return true;
        }

        public void DrawBuffIcon()
        {
            for (int i = 0; i < currentRoleBuff.Length; i++)
            {
                if (i < roles[indexCurrentRole].Buffs.Count)
                {
                    currentRoleBuff[i].gameObject.SetActive(true);
                    if (roles[indexCurrentRole].Buffs[i].Turn < 0)
                    {
                        currentRoleBuffTimer[i].text = "";
                    }
                    else
                    {
                        currentRoleBuffTimer[i].text = roles[indexCurrentRole].Buffs[i].Turn.ToString();
                    }
                }
                else
                {
                    currentRoleBuff[i].gameObject.SetActive(false);
                }
            }
        }



        public void CheckBuffsRoles()
        {
            for (int i = 0; i < roles.Count; i++)
            {
                for (int j = 0; j < roles[i].Buffs.Count; j++)
                {
                    roles[i].Buffs[j].Turn -= 1;
                    if (roles[i].Buffs[j].Turn == 0)
                    {
                        skillManager.RemoveSkillEffect(roles[i].Buffs[j].SkillEffectbuff);
                        roles[i].Buffs.RemoveAt(j);
                        j -= 1;
                    }
                }
            }
            DrawBuffIcon();
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
            if(currentAttack.InfluenceValue > 0)
            {
                currentAttackInfluence = currentAttack.InfluenceValue + roleInfluenceBonus[indexCurrentRole];
                currentAttackInfluence += Random.Range(-currentAttack.InfluenceRandom, currentAttack.InfluenceRandom);
            }
            else
            {
                currentAttackInfluence = (roles[indexCurrentRole].Defense + roleInfluenceBonus[indexCurrentRole]) * currentAttack.InfluenceMultiplier;
                currentAttackInfluence += Random.Range(-currentAttack.InfluenceRandom, currentAttack.InfluenceRandom);
            }
            if (currentAttackInfluence <= 0)
                currentAttackInfluence = 1;
            textSkillInfluenceValue.text = currentAttackInfluence.ToString();
            emotionAttackManager.ComboAnimationRoleDescription(false);
        }


        // Appelé via l'animator enemyAttackFace
        public void ActivateInput()
        {
            input.gameObject.SetActive(true);
            skillManager.PreviewSkill(currentAttack);
        }


        public void StopEnemyActivation()
        {
            cameraController.EnemySkillCancel();
            skillManager.StopPreview(currentAttack);
            skillManager.ApplySkill(currentAttack);
            input.gameObject.SetActive(false);
            enemyAttack.SetBool("Appear", false);
            enemyAttackFace.SetTrigger("Disappear");
            spotEnemy.SetBool("Appear", false);
            emotionAttackManager.ComboAnimationRoleDescription(true);
            enemyAttack.ResetTrigger("HideDescription");
            enemyAttack.ResetTrigger("ShowDescription");
        }


        public void EnemyAttackCounter()
        {
            skillManager.StopPreview(currentAttack);
            cameraController.EnemySkillCounter();
            emotionAttackManager.ResetCard();
            emotionAttackManager.SwitchCardTransformToRessource();

            input.gameObject.SetActive(false);
            enemyAttackFace.SetTrigger("Counter");

            enemyAttack.SetBool("Appear", false);
            spotEnemy.SetBool("Appear", false);
            enemyAttack.ResetTrigger("HideDescription");
            enemyAttack.ResetTrigger("ShowDescription");
        }








        public void SelectEmotion(string emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard(emotion, false);
            if (card != null)
            {
                card.HidePreview();
                currentAttackInfluence -= card.GetStat();
                if (currentAttackInfluence <= 0)
                {
                    //currentAttackInfluence = 0;
                    enemyAttackFace.ResetTrigger("UnSlice");
                    enemyAttackFace.SetTrigger("Slice");
                }
                textSkillInfluenceValue.text = currentAttackInfluence.ToString();
            }
            if (emotionAttackManager.GetComboCount() == 0)
            {
                enemyAttack.SetTrigger("HideDescription");
                emotionAttackManager.ComboAnimationRoleDescription(true);
            }
        }

        public void RemoveCard()
        {
            EmotionCard card = emotionAttackManager.RemoveCard();
            if (card != null)
            {
                card.ShowPreview();
                currentAttackInfluence += card.GetStat();
                if (currentAttackInfluence > 0)
                {
                    enemyAttackFace.ResetTrigger("Slice");
                    enemyAttackFace.SetTrigger("UnSlice");
                }
                textSkillInfluenceValue.text = currentAttackInfluence.ToString();
            }
            if (emotionAttackManager.GetComboCount() == -1)
            {
                enemyAttack.SetTrigger("ShowDescription");
                emotionAttackManager.ComboAnimationRoleDescription(false);
            }
        }

        public void Defence()
        {
            if (emotionAttackManager.GetComboCount() == -1)
            {
                StopEnemyActivation();
            }
            else if (currentAttackInfluence <= 0)
            {
                emotionAttackManager.DestroyCombo();
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
