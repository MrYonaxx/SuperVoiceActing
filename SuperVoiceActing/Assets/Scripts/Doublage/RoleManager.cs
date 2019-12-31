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

        [SerializeField]
        TextMeshProUGUI textRoleName;

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
        [SerializeField]
        Shake shakeFeedback;

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



        [Space]
        [SerializeField]
        Image[] currentRoleBuff;
        [SerializeField]
        TextMeshProUGUI[] currentRoleBuffTimer;



        [Title("RoleAttackTimeline")]
        [SerializeField]
        Animator[] animatorSkillTimeline;
        [SerializeField]
        TextMeshProUGUI[] textSkillTimelineName;


        bool firstTime = false;
        bool readyToAttack = false;
        bool selectingCounter = false;

        List<Role> roles;
        List<SkillRoleData> skillRoles = new List<SkillRoleData>();

        int indexCurrentRole = 0;
        int indexCurrentSkill = 0;
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
            textRoleName.text = roles[indexCurrentRole].Name;

            indexCurrentSkill = 0;
            currentAttack = null;
            skillRoles.Clear();
            if (roles[indexCurrentRole].SkillRoles != null)
            {
                for (int i = 0; i < roles[indexCurrentRole].SkillRoles.Length; i++)
                {
                    skillRoles.Add((SkillRoleData)skillManager.GetSkill(roles[indexCurrentRole].SkillRoles[i]));
                    if (skillRoles[i] == null)
                        textSkillTimelineName[i].text = "";
                    else
                        textSkillTimelineName[i].text = skillRoles[i].SkillName;
                }
            }

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

        public void SetRoles(List<Role> contractRoles, int currentRole)
        {
            roles = contractRoles;
            SetIndexRole(currentRole);
        }

        public void AddScorePerformance(int lastAttackScore, int lastBestScore)
        {
            roles[indexCurrentRole].RolePerformance += lastAttackScore;
            roles[indexCurrentRole].RoleBestScore += lastBestScore;
        }




        public void ShowHUDNextAttack(bool show, bool nextAttackStay = false)
        {
            if (skillRoles.Count == 0)
                return;
            if (nextAttackStay == true && currentAttack == null)
                ShowHUDNextAttack(show);
            else 
            {
                for (int i = 0; i < animatorSkillTimeline.Length; i++)
                {
                    if (nextAttackStay == true && i == indexCurrentSkill)
                        continue;
                    else
                        animatorSkillTimeline[i].SetBool("Appear", show);
                }
            }
        }

        public void DetermineCurrentAttack()
        {
            if (skillRoles.Count == 0)
                return;
            indexCurrentSkill = Random.Range(0, skillRoles.Count);
            currentAttack = skillRoles[indexCurrentSkill];
            animatorSkillTimeline[indexCurrentSkill].SetTrigger("isNextAttack");
        }


        public int GetCurrentRoleDefense()
        {
            return roles[indexCurrentRole].Defense;
        }

        public void AddRoleBonus(int value)
        {
            roleInfluenceBonus[indexCurrentRole] += value;
        }





        public bool IsAttacking()
        {
            return (currentAttack != null);
        }


        public bool RoleDecision(string phase, int line, int turn, float enemyHP)
        {
            /*for (int i = 0; i < roles[indexCurrentRole].RoleAI.Length; i++)
            {
                if (roles[indexCurrentRole].RoleAI[i].CheckEnemyAI(phase, line, turn, enemyHP))
                {
                    currentAttack = (SkillRoleData) roles[indexCurrentRole].RoleAI[i].Skills[Random.Range(0, roles[indexCurrentRole].RoleAI[i].Skills.Length)];
                    if (currentAttack != null)
                    {
                        readyToAttack = true;
                        ActivateRoleSpot();
                    }
                    else
                    {
                        readyToAttack = false;
                    }
                    return readyToAttack;
                }
            }
            return false;*/
            return false;
        }







        public void ActivateRoleSpot()
        {
            if (currentAttack == null)
                return;
            spotEnemy.gameObject.SetActive(true);
            spotEnemy.SetBool("Appear", true);
        }

        /*public void ActivateRoleSpot()
        {
            if (firstTime == false)
            {
                spotEnemy.gameObject.SetActive(true);
            }
            spotEnemy.SetBool("Appear", true);
        }*/

        public IEnumerator RoleAttackCoroutine()
        {
            enemyAttackFace.gameObject.SetActive(true);
            enemyAttackFace.SetTrigger("Appear");
            animatorSkillTimeline[indexCurrentSkill].SetBool("Appear", false);
            animatorSkillTimeline[indexCurrentSkill].SetTrigger("Feedback");
            skillManager.PreviewSkill(currentAttack);
            yield return new WaitForSeconds(2.5f);
            cameraController.EnemySkillCancel();
            skillManager.StopPreview(currentAttack);
            skillManager.ApplySkill(currentAttack);
            enemyAttack.SetBool("Appear", false);
            enemyAttackFace.SetTrigger("Disappear");
            spotEnemy.SetBool("Appear", false);
            //shakeFeedback.ShakeRectEffect();
        }

        // ==================================================================================================================================

        public void EnemyAttackActivation()
        {
            /*readyToAttack = false;
            if (firstTime == false)
            {
                enemyAttack.gameObject.SetActive(true);
                enemyAttackFace.gameObject.SetActive(true);
                firstTime = true;
            }
            enemyAttack.SetBool("Appear", true);
            enemyAttack.ResetTrigger("HideDescription");
            enemyAttack.ResetTrigger("ShowDescription");

            enemyAttackFace.ResetTrigger("Appear");
            enemyAttackFace.ResetTrigger("Disappear");
            enemyAttackFace.ResetTrigger("Slice");
            enemyAttackFace.ResetTrigger("UnSlice");
            enemyAttackFace.SetTrigger("Appear");

            imageEnemy.sprite = roles[indexCurrentRole].RoleSprite;
            imageEnemyLower.sprite = roles[indexCurrentRole].RoleSprite;
            imageEnemyEffect.sprite = roles[indexCurrentRole].RoleSprite;
            textSkillName.text = currentAttack.SkillName;
            textSkillDescription.text = currentAttack.Description;
            textSkillDescriptionBattle.text = "[ " + currentAttack.DescriptionBattle + " ]";
            if (currentAttack.InfluenceValue > 0)
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
            emotionAttackManager.ComboAnimationRoleDescription(false);*/
        }


        // Appelé via l'animator enemyAttackFace
        /*public void ActivateInput()
        {
            input.gameObject.SetActive(true);
            skillManager.PreviewSkill(currentAttack);
        }*/


        public void StopEnemyActivation()
        {
            /*selectingCounter = false;
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
            shakeFeedback.ShakeRectEffect();*/
        }


        public void EnemyAttackCounter()
        {
            /*selectingCounter = false;
            skillManager.StopPreview(currentAttack);
            cameraController.EnemySkillCounter();
            emotionAttackManager.ResetCard();
            emotionAttackManager.SwitchCardTransformToRessource();

            input.gameObject.SetActive(false);
            enemyAttackFace.SetTrigger("Counter");

            enemyAttack.SetBool("Appear", false);
            spotEnemy.SetBool("Appear", false);
            enemyAttack.ResetTrigger("HideDescription");
            enemyAttack.ResetTrigger("ShowDescription");*/
        }





        /*public void SelectEmotion(int emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard((Emotion)emotion, false);
            SelectEmotion(card);
        }


        public void SelectEmotion(string emotion)
        {
            EmotionCard card = emotionAttackManager.SelectCard(emotion, false);
            SelectEmotion(card);
        }

        public void SelectEmotion(EmotionCard card)
        {
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
                selectingCounter = true;
                enemyAttack.SetTrigger("HideDescription");
                enemyAttack.ResetTrigger("ShowDescription");
                emotionAttackManager.ComboAnimationRoleDescription(true);
            }
        }

        public void RemoveCard()
        {
            if (selectingCounter == false)
            {
                StopEnemyActivation();
                return;
            }
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
                selectingCounter = false;
                enemyAttack.SetTrigger("ShowDescription");
                enemyAttack.ResetTrigger("HideDescription");
                emotionAttackManager.ComboAnimationRoleDescription(false);
            }
        }

        public void Defence()
        {
            if (emotionAttackManager.GetComboCount() == -1)
            {
                selectingCounter = true;
                enemyAttack.SetTrigger("HideDescription");
                enemyAttack.ResetTrigger("ShowDescription");
                emotionAttackManager.ComboAnimationRoleDescription(true);
            }
            else if (currentAttackInfluence <= 0)
            {
                emotionAttackManager.DestroyCombo();
                EnemyAttackCounter();
            }
        }*/










        #endregion

    } // RoleManager class
	
}// #PROJECTNAME# namespace
