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
	public class ActorsManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Debug")]
        [SerializeField]
        VoiceActorData debug;

        [SerializeField]
        private List<VoiceActor> actors = new List<VoiceActor>();

        [Header("Parameter")]
        [SerializeField]
        int defenseStack = 5;
        [SerializeField]
        float healthCriticalThreshold = 0.3f;
        [SerializeField]
        Color healthCriticalColor;


        [Header("Feedbacks Damage")]
        [SerializeField]
        Animator animatorHealthBar;
        [SerializeField]
        RectTransform healthBar;
        [SerializeField]
        Image healthContent;
        [SerializeField]
        Image healthContentProgression;
        [SerializeField]
        TextMeshProUGUI textActorName;
        [SerializeField]
        TextMeshProUGUI textCurrentHp;
        [SerializeField]
        TextMeshProUGUI textMaxHp;

        [Space]
        [SerializeField]
        Animator animatorDamage;
        [SerializeField]
        TextMeshProUGUI textDamage;
        [SerializeField]
        RectTransform damagePreviz;
        [SerializeField]
        RectTransform transformDamageLost;
        [SerializeField]
        Animator animatorDamageLost;

        [Space]
        [SerializeField]
        EffectManager effectManagerDeath;




        [Space]
        [SerializeField]
        Image[] currentActorBuff;
        [SerializeField]
        TextMeshProUGUI[] currentActorBuffTimer;








        EmotionCardTotal[] emotionCards = new EmotionCardTotal[9];



        private int indexCurrentActor = 0;
        private int[] actorsResistance = { 100, 100, 100 };
        private int[] actorsHealthRegain = { 0, 0, 0 };
        int attackDamage = 0;




        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetCurrentActorHP()
        {
            return actors[indexCurrentActor].Hp;
        }


        public int GetCurrentActorHPMax()
        {
            return actors[indexCurrentActor].HpMax;
        }

        public void SetIndexActors(int newIndex)
        {
            indexCurrentActor = newIndex;
        }

        public int GetCurrentActorIndex()
        {
            return indexCurrentActor;
        }

        public int GetCurrentActorDamageVariance()
        {
            return actors[indexCurrentActor].DamageVariance;
        }

        public List<Buff> GetBuffList()
        {
            return actors[indexCurrentActor].Buffs;
        }

        public Vector3 GetSkillPositionOffset()
        {
            return actors[indexCurrentActor].SkillOffset;
        }

        public int GetCurrentActorHPRegain()
        {
            return actorsHealthRegain[indexCurrentActor];
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public VoiceActor GetCurrentActor()
        {
            return actors[indexCurrentActor];
        }

        public void SetCards(EmotionCardTotal[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                emotionCards[i] = new EmotionCardTotal(cards[i].Cards);
            }
        }

        public void SetActors(List<VoiceActor> actorsContract)
        {
            if (actorsContract[0] == null)
                actors.Add(new VoiceActor(debug));
            else
                actors = actorsContract;
            DrawActorStat();
        }

        public void ResetStat()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                actors[indexCurrentActor].StatModifier = new EmotionStat();
                actors[indexCurrentActor].Buffs.Clear();
            }
        }

        public bool AddBuff(Buff buff)
        {
            for(int i = 0; i < actors[indexCurrentActor].Buffs.Count; i++)
            {
                if (actors[indexCurrentActor].Buffs[i].SkillEffectbuff == buff.SkillEffectbuff)
                {
                    if (buff.BuffData.CanAddMultiple == false)
                    {
                        if (buff.BuffData.Refresh == true)
                        {
                            actors[indexCurrentActor].Buffs[i].Turn = buff.BuffData.TurnActive;
                        }
                        else if (buff.BuffData.AddBuffTurn == true)
                        {
                            actors[indexCurrentActor].Buffs[i].Turn += buff.BuffData.TurnActive;
                        }
                        return false;
                    }
                }
            }
            actors[indexCurrentActor].Buffs.Add(buff);
            DrawBuffIcon();
            return true;
        }

        public void DrawBuffIcon()
        {
            for(int i = 0; i < currentActorBuff.Length; i++)
            {
                if(i < actors[indexCurrentActor].Buffs.Count)
                {
                    currentActorBuff[i].gameObject.SetActive(true);
                    if (actors[indexCurrentActor].Buffs[i].Turn < 0)
                    {
                        currentActorBuffTimer[i].text = "";
                    }
                    else
                    {
                        currentActorBuffTimer[i].text = actors[indexCurrentActor].Buffs[i].Turn.ToString();
                    }
                }
                else
                {
                    currentActorBuff[i].gameObject.SetActive(false);
                }
            }
        }



        public void CheckBuffsActors()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                for (int j = 0; j < actors[i].Buffs.Count; j++)
                {
                    actors[i].Buffs[j].Turn -= 1;
                    if (actors[i].Buffs[j].Turn == 0)
                    {
                        actors[i].Buffs[j].SkillEffectbuff.RemoveSkillEffectActor(actors[i]);
                        actors[i].Buffs.RemoveAt(j);
                        j -= 1;
                        DrawActorStat();
                    }
                }
            }
        }

        public void CheckBuffsCards()
        {
            for (int i = 0; i < 9; i++)
            {
                EmotionCard[] pack = null;
                pack = emotionCards[i].Cards;

                for (int j = 0; j < pack.Length; j++)
                {
                    if(pack[j] != null)
                        pack[j].CheckBuff();
                }
            }
        }








        public void DrawActorStat()
        {
            // Joie > Tristesse > Dégout > Colère > Surprise > Douceur > Peur > Confiance

            for(int i = 0; i < 9; i++)
            {
                if (emotionCards[i] == null)
                    continue;
                EmotionCard[] pack = emotionCards[i].Cards;//.GetCardPack(i);
                int newStatValue = actors[indexCurrentActor].Statistique.GetEmotion(i);
                int statBonus = actors[indexCurrentActor].StatModifier.GetEmotion(i);

                for (int j = 0; j < pack.Length; j++)
                {
                    if(pack[j] != null)
                        pack[j].DrawStat((int)(newStatValue * (1f - (0.25f * j))), statBonus);
                }
            }

            if(actors[indexCurrentActor].Hp < actors[indexCurrentActor].HpMax * healthCriticalThreshold)
            {
                textCurrentHp.color = healthCriticalColor;
                textMaxHp.color = healthCriticalColor;
            }

            textActorName.text = actors[indexCurrentActor].Name;
            textCurrentHp.text = actors[indexCurrentActor].Hp.ToString();
            textMaxHp.text = actors[indexCurrentActor].HpMax.ToString();

            float ratioHP = (float)actors[indexCurrentActor].Hp / actors[indexCurrentActor].HpMax;
            healthContent.transform.localScale = new Vector3(ratioHP, 1, 1);

            float ratioHPRegain = actors[indexCurrentActor].Hp + actorsHealthRegain[indexCurrentActor];
            ratioHPRegain = ratioHPRegain / actors[indexCurrentActor].HpMax;
            healthContentProgression.transform.localScale = new Vector3(ratioHPRegain, healthContentProgression.transform.localScale.y, healthContentProgression.transform.localScale.z);
        }










        // Tout ce qui est en rapport avec les HPs
        // =================================================================================================================
        public void ResetActorRegain()
        {
            actorsHealthRegain[indexCurrentActor] = 0;
        }

        public void AddActorResistance(int addValue)
        {
            actorsResistance[indexCurrentActor] += addValue;
        }


        public void ActorAttackDamage()
        {
            animatorDamage.SetTrigger("Attack");
            ActorTakeDamage(attackDamage);
            attackDamage = 0;
            textDamage.text = attackDamage.ToString();
            DrawDamagePrevisualization();
        }



        public void ActorTakeDamage(int damage)
        {
            float ratioHP;
            float ratioHPRegain;
            float damagePercentage;

            // Animation Damage Lost
            damagePercentage = (float)damage / actors[indexCurrentActor].Hp;
            transformDamageLost.anchorMax = new Vector2(1 + damagePercentage, 0.5f);
            animatorDamageLost.SetTrigger("Feedback");

            // Modification jauge de vie
            actors[indexCurrentActor].Hp -= damage;
            if (actors[indexCurrentActor].Hp < 0)
                actors[indexCurrentActor].Hp = 0;
            else if (actors[indexCurrentActor].Hp > actors[indexCurrentActor].HpMax)
                actors[indexCurrentActor].Hp = actors[indexCurrentActor].HpMax;
            textCurrentHp.text = actors[indexCurrentActor].Hp.ToString();

            ratioHP = (float) actors[indexCurrentActor].Hp / actors[indexCurrentActor].HpMax;
            healthContent.transform.localScale = new Vector3(ratioHP, 1, 1);
            if (actors[indexCurrentActor].Hp < actors[indexCurrentActor].HpMax * healthCriticalThreshold)
            {
                textCurrentHp.color = healthCriticalColor;
                textMaxHp.color = healthCriticalColor;
            }

            // HP Regain
            if(damage > 0)
                actorsHealthRegain[indexCurrentActor] += damage / 2;
            else
                actorsHealthRegain[indexCurrentActor] += damage;
            if (actorsHealthRegain[indexCurrentActor] <= 0)
                actorsHealthRegain[indexCurrentActor] = 0;
            ratioHPRegain = actors[indexCurrentActor].Hp + actorsHealthRegain[indexCurrentActor];
            if(ratioHPRegain > actors[indexCurrentActor].HpMax) { ratioHPRegain = 1; }
            ratioHPRegain = ratioHPRegain / actors[indexCurrentActor].HpMax;
            healthContentProgression.transform.localScale = new Vector3(ratioHPRegain, healthContentProgression.transform.localScale.y, healthContentProgression.transform.localScale.z);

            if (damage > 0)
                StartCoroutine(FeedbackHealthBar());
        }

        private IEnumerator FeedbackHealthBar(int timeShake = 30, int timeGauge = 180)
        {
            if (actors[indexCurrentActor].Hp == 0)
            {
                effectManagerDeath.NegativeScreen(true);
                effectManagerDeath.Flash();
            }
            float intensity = 30;
            Vector2 origin = healthBar.anchoredPosition;
            while (timeShake != 0)
            {
                timeShake -= 1;
                healthBar.anchoredPosition = new Vector2(origin.x + Random.Range(-intensity, intensity), origin.y + Random.Range(-intensity, intensity));
                intensity *= 0.9f;
                yield return null;
            }
            healthBar.anchoredPosition = origin;

            //Vector3 speed = new Vector3((healthContent.transform.localScale.x - healthContentProgression.transform.localScale.x) / timeGauge, 0, 0);
            while (timeGauge != 0)
            {
                timeGauge -= 1;
                //healthContentProgression.transform.localScale += speed;
                yield return null;
            }
            if (actors[indexCurrentActor].Hp == 0)
            {
                effectManagerDeath.NegativeScreen(false);
                effectManagerDeath.Flash();
            }
            //animatorHealthBar.SetBool("Appear", false);
        }

        public void HideHealthBar()
        {
            animatorHealthBar.SetBool("Appear", false);
        }
        public void ShowHealthBar()
        {
            animatorHealthBar.SetBool("Appear", true);
        }




        public void AddAttackDamage(int roleAttack, float emotionMultiplier)
        {
            int damageTmp = (int) ((roleAttack - (roleAttack * ((actors[indexCurrentActor].RoleDefense * defenseStack) / 100f))) * emotionMultiplier);
            damageTmp = (int)(damageTmp * (actorsResistance[indexCurrentActor] / 100f));
            attackDamage += damageTmp;
            textDamage.text = attackDamage.ToString();
            DrawDamagePrevisualization();
            animatorDamage.SetBool("Appear", true);
        }

        public void RemoveAttackDamage(int roleAttack, float emotionMultiplier)
        {
            int damageTmp = (int)((roleAttack - (roleAttack * ((actors[indexCurrentActor].RoleDefense * defenseStack) / 100f))) * emotionMultiplier);
            damageTmp = (int)(damageTmp * (actorsResistance[indexCurrentActor] / 100f));
            attackDamage -= damageTmp;
            textDamage.text = attackDamage.ToString();
            DrawDamagePrevisualization();
        }

        public void DrawDamagePrevisualization()
        {
            if (actors[indexCurrentActor].Hp <= 0)
                return;
            float damagePercentage = (float) attackDamage / actors[indexCurrentActor].Hp;
            damagePreviz.anchorMin = new Vector2(1 - damagePercentage, 0);
            damagePreviz.offsetMin = new Vector2(0, 0);
            textCurrentHp.text = (actors[indexCurrentActor].Hp - attackDamage).ToString();

            if (damagePercentage >= 1)
            {
                effectManagerDeath.BlurScreen(true);
            }
            /*else
            {
                effectManagerDeath.BlurScreen(false);
            }*/
        }



        public IEnumerator DeathCoroutine(Transform character)
        {
            int time = 100;
            while (time != 0)
            {
                character.eulerAngles += new Vector3(0, 0, 0.1f);
                time -= 1;
                yield return null;
            }
            time = 20;
            effectManagerDeath.TotalFade(true, time);
            while (time != 0)
            {
                character.eulerAngles += new Vector3(0, 0, 0.1f);
                time -= 1;
                yield return null;
            }
            effectManagerDeath.TotalFade(false, 120);
        }

















        #endregion

    } // ActorsManager class
	
}// #PROJECTNAME# namespace
