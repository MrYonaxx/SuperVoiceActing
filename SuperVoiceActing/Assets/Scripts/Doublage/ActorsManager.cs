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
        [Title("Debug")]
        /*[SerializeField]
        private List<VoiceActor> actors = new List<VoiceActor>();*/

        [Title("Parameter")]
        [SerializeField]
        int defenseStack = 5;
        [SerializeField]
        float healthCriticalThreshold = 0.3f;
        [SerializeField]
        Color healthCriticalColor;


        [Title("Feedbacks Damage")]
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



        [Title("Buff")]
        [SerializeField]
        RectTransform buffWindowsTransform;
        [SerializeField]
        BuffWindow buffWindow;







        List<BuffWindow> listBuffWindows = new List<BuffWindow>();

        int attackDamage = 0;




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
        /*public VoiceActor GetCurrentActor()
        {
            return actors[indexCurrentActor];
        }*/

        /*public void SetCards(EmotionCardTotal[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                emotionCards[i] = new EmotionCardTotal(cards[i].Cards);
            }
        }*/

        /*public void SetActors(List<VoiceActor> actorsContract)
        {
            actors = actorsContract;
            DrawActorStat();
        }*/

        public void ResetStat()
        {
            /*for (int i = 0; i < actors.Count; i++)
            {
                actors[indexCurrentActor].StatModifier = new EmotionStat();
                actors[indexCurrentActor].Buffs.Clear();
            }*/
        }

        public bool AddBuff(Buff buff)
        {
            /*for(int i = 0; i < actors[indexCurrentActor].Buffs.Count; i++)
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
            DrawBuffIcon();*/
            return true;
        }

        public void DrawBuffIcon(VoiceActor voiceActor)
        {
            for (int i = 0; i < voiceActor.Buffs.Count; i++)
            {
                if(i >= listBuffWindows.Count)
                {
                    listBuffWindows.Add(Instantiate(buffWindow, buffWindowsTransform));
                }
                listBuffWindows[i].gameObject.SetActive(true);
                listBuffWindows[i].DrawBuff(voiceActor.Buffs[i]);
            }
            for(int i = voiceActor.Buffs.Count; i < listBuffWindows.Count; i++)
            {
                listBuffWindows[i].gameObject.SetActive(false);
            }
        }



        public void CheckBuffsActors()
        {
            /*for (int i = 0; i < actors.Count; i++)
            {
                for (int j = 0; j < actors[i].Buffs.Count; j++)
                {
                    actors[i].Buffs[j].Turn -= 1;
                    if (actors[i].Buffs[j].Turn == 0)
                    {
                        //actors[i].Buffs[j].SkillEffectbuff.RemoveSkillEffectActor(actors[i]);
                        actors[i].Buffs.RemoveAt(j);
                        j -= 1;
                        DrawActorStat();
                    }
                }
            }*/
        }

        public void CheckBuffsCards()
        {
            /*for (int i = 0; i < 9; i++)
            {
                EmotionCard[] pack = null;
                pack = emotionCards[i].Cards;

                for (int j = 0; j < pack.Length; j++)
                {
                    if(pack[j] != null)
                        pack[j].CheckBuff();
                }
            }*/
        }








        public void DrawActorStat(VoiceActor voiceActor, EmotionCardTotal[] emotionCards)
        {
            if(voiceActor.Hp < voiceActor.HpMax * healthCriticalThreshold)
            {
                textCurrentHp.color = healthCriticalColor;
                textMaxHp.color = healthCriticalColor;
            }

            textActorName.text = voiceActor.VoiceActorName;
            textCurrentHp.text = voiceActor.Hp.ToString();
            textMaxHp.text = voiceActor.HpMax.ToString();

            float ratioHP = (float)voiceActor.Hp / voiceActor.HpMax;
            healthContent.transform.localScale = new Vector3(ratioHP, 1, 1);
            if (voiceActor.Hp < voiceActor.HpMax * healthCriticalThreshold)
            {
                textCurrentHp.color = healthCriticalColor;
                textMaxHp.color = healthCriticalColor;
            }

            float ratioHPRegain = voiceActor.Hp + voiceActor.ChipDamage;
            ratioHPRegain = ratioHPRegain / voiceActor.HpMax;
            healthContentProgression.transform.localScale = new Vector3(ratioHPRegain, healthContentProgression.transform.localScale.y, healthContentProgression.transform.localScale.z);

            DrawCardStat(voiceActor, emotionCards);
        }


        private void DrawCardStat(VoiceActor voiceActor, EmotionCardTotal[] emotionCards)
        {
            // Joie > Tristesse > Dégout > Colère > Surprise > Douceur > Peur > Confiance
            for (int i = 0; i < 9; i++)
            {
                if (emotionCards[i] == null)
                    continue;
                EmotionCard[] pack = emotionCards[i].Cards;
                int newStatValue = voiceActor.Statistique.GetEmotion(i);
                int statBonus = voiceActor.StatModifier.GetEmotion(i);

                for (int j = 0; j < pack.Length; j++)
                {
                    if (pack[j] != null)
                        pack[j].DrawStat((int)(newStatValue * (1f - (0.25f * j))), statBonus);
                }
            }
        }








        // Tout ce qui est en rapport avec les HPs
        // =================================================================================================================
        /*public void ResetActorRegain()
        {
            actorsHealthRegain[indexCurrentActor] = 0;
        }

        public void AddActorResistance(int addValue)
        {
            actorsResistance[indexCurrentActor] += addValue;
        }*/


        public void ActorTakeDamage(VoiceActor voiceActor)
        {
            animatorDamage.SetTrigger("Attack");
            ActorTakeDamage(voiceActor, attackDamage);
            attackDamage = 0;
            textDamage.text = attackDamage.ToString();
            DrawDamagePrevisualization(voiceActor);
        }



        public void ActorTakeDamage(VoiceActor voiceActor, int damage)
        {
            float ratioHP;
            float ratioHPRegain;
            float damagePercentage;

            // Animation Damage Lost
            damagePercentage = (float)damage / voiceActor.Hp;
            transformDamageLost.anchorMax = new Vector2(1 + damagePercentage, 0.5f);
            animatorDamageLost.SetTrigger("Feedback");

            // Modification jauge de vie
            voiceActor.Hp -= damage;
            voiceActor.Hp = Mathf.Clamp(voiceActor.Hp, 0, voiceActor.HpMax);

            // HP Regain
            if(damage > 0)
                voiceActor.ChipDamage += damage / 2;
            else
                voiceActor.ChipDamage += damage;
            voiceActor.ChipDamage = Mathf.Max(0, voiceActor.ChipDamage);
            ratioHPRegain = voiceActor.Hp + voiceActor.ChipDamage;
            if(ratioHPRegain > voiceActor.HpMax)
                ratioHPRegain = 1;
            ratioHPRegain = ratioHPRegain / voiceActor.HpMax;
            healthContentProgression.transform.localScale = new Vector3(ratioHPRegain, healthContentProgression.transform.localScale.y, healthContentProgression.transform.localScale.z);

            if (damage > 0)
                StartCoroutine(FeedbackHealthBar());
        }

        private IEnumerator FeedbackHealthBar(int timeShake = 30, int timeGauge = 180)
        {
            /*if (actors[indexCurrentActor].Hp == 0)
            {
                effectManagerDeath.NegativeScreen(true);
                effectManagerDeath.Flash();
            }*/
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
            /*if (actors[indexCurrentActor].Hp == 0)
            {
                effectManagerDeath.NegativeScreen(false);
                effectManagerDeath.Flash();
            }*/
            //animatorHealthBar.SetBool("Appear", false);
        }

        public void ShowHealthBar(bool b)
        {
            animatorHealthBar.SetBool("Appear", b);
        }




        public void AddAttackDamage(VoiceActor voiceActor, int roleAttack, float emotionMultiplier)
        {
            //int damageTmp = (int) ((roleAttack - (roleAttack * ((actors[indexCurrentActor].RoleDefense * defenseStack) / 100f))) * emotionMultiplier);
            //damageTmp = (int)(damageTmp * (actors[indexCurrentActor].BonusResistance / 100f));
            int damageTmp = (int)(roleAttack * emotionMultiplier);
            damageTmp = (int)(damageTmp * (voiceActor.BonusResistance / 100f));
            attackDamage += damageTmp;
            textDamage.text = attackDamage.ToString();
            DrawDamagePrevisualization(voiceActor);
            animatorDamage.SetBool("Appear", true);
        }

        public void RemoveAttackDamage(VoiceActor voiceActor, int roleAttack, float emotionMultiplier)
        {
            //int damageTmp = (int)((roleAttack - (roleAttack * ((actors[indexCurrentActor].RoleDefense * defenseStack) / 100f))) * emotionMultiplier);
            //damageTmp = (int)(damageTmp * (actors[indexCurrentActor].BonusResistance / 100f));
            int damageTmp = (int)(roleAttack * emotionMultiplier);
            damageTmp = (int)(damageTmp * (voiceActor.BonusResistance / 100f));
            attackDamage -= damageTmp;
            textDamage.text = attackDamage.ToString();
            DrawDamagePrevisualization(voiceActor);
        }

        public void DrawDamagePrevisualization(VoiceActor voiceActor)
        {
            if (voiceActor.Hp <= 0)
                return;
            float damagePercentage = (float) attackDamage / voiceActor.Hp;
            damagePreviz.anchorMin = new Vector2(1 - damagePercentage, 0);
            damagePreviz.offsetMin = new Vector2(0, 0);
            textCurrentHp.text = (voiceActor.Hp - attackDamage).ToString();
        }



        public IEnumerator DeathCoroutine(Transform character)
        {
            /*int time = 100;
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
            effectManagerDeath.TotalFade(false, 120);*/
            yield return null;
        }

















        #endregion

    } // ActorsManager class
	
}// #PROJECTNAME# namespace
