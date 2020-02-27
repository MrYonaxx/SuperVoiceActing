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
        float emotionStackPercentage = 0.1f;
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

        [Title("Attack")]
        [SerializeField]
        TextMeshProUGUI textActorAttackPower;
        [SerializeField]
        Color colorAttackBuff;
        [SerializeField]
        Color colorAttackDebuff;

        [SerializeField]
        Color colorDamageBuff;
        [SerializeField]
        Color colorDamageDebuff;


        [Title("Buff")]
        [SerializeField]
        RectTransform buffWindowsTransform;
        [SerializeField]
        BuffWindow buffWindow;



        [Title("Feedback")]
        [SerializeField]
        Shake shakeFeedbackHealth;



        List<BuffWindow> listBuffWindows = new List<BuffWindow>();

        int attackPower = 0;
        int attackPowerBonus = 0;

        int attackDamage = 0;
        int attackDamageBonus = 0;




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

        public void ResetStat(List<VoiceActor> voiceActors)
        {
            for (int i = 0; i < voiceActors.Count; i++)
            {
                voiceActors[i].BonusDamage = 1;
                voiceActors[i].BonusResistance = 1;
                voiceActors[i].ChipDamage = 0;
                voiceActors[i].StatModifier = new EmotionStat();
                voiceActors[i].Buffs.Clear();
            }
        }


        public void AddBonusStat(List<VoiceActor> voiceActors, Emotion[] emotions)
        {
            if (emotions == null)
                return;
            EmotionStat toneAddValue = new EmotionStat(0, 0, 0, 0, 0, 0, 0, 0);
            for (int i = 0; i < emotions.Length; i++)
            {
                toneAddValue.Add((int)emotions[i], 1);
            }
            for(int i = 0; i < voiceActors.Count; i++)
                voiceActors[i].StatModifier.Add(toneAddValue);
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

            UpdateCardStat(voiceActor, emotionCards);
            DrawBuffIcon(voiceActor);
        }


        private void UpdateCardStat(VoiceActor voiceActor, EmotionCardTotal[] emotionCards)
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
                    {
                        int value = (int)(newStatValue * (1f - (0.25f * j)));
                        pack[j].DrawStat(value, statBonus, (int)(value * (statBonus * emotionStackPercentage)));
                    }
                }
            }
        }









        public int GetCurrentAttackPower(VoiceActor voiceActor)
        {           
            return (int)(attackPower * voiceActor.BonusDamage) + Random.Range(-voiceActor.DamageVariance, voiceActor.DamageVariance+1);
        }

        public void AddAttackPower(VoiceActor voiceActor, int attackValue)
        {
            attackPower += attackValue;
            DrawAttackParameter(voiceActor);
        }

        public void ResetAttackPower()
        {
            attackPower = 0;
            textActorAttackPower.text = attackPower.ToString();
        }



        public void AddAttackDamageBonus(VoiceActor voiceActor, int value)
        {
            attackDamageBonus += value;
            DrawAttackParameter(voiceActor);
            DrawDamagePrevisualization(voiceActor);
        }
        public void AddAttackDamage(VoiceActor voiceActor, int roleAttack, float emotionMultiplier)
        {
            int damageTmp = (int)(roleAttack * emotionMultiplier);
            attackDamage += damageTmp;
            DrawAttackParameter(voiceActor);
            DrawDamagePrevisualization(voiceActor);
            //animatorDamage.SetBool("Appear", true);
        }

        private void DrawAttackParameter(VoiceActor voiceActor)
        {
            textActorAttackPower.gameObject.SetActive(true);
            textActorAttackPower.text = ((int)(attackPower * voiceActor.BonusDamage)).ToString();
            textDamage.text = ((int)(attackDamage * voiceActor.BonusResistance) + attackDamageBonus).ToString();

            if(attackPower != 0 && voiceActor.BonusDamage > 1)
                textActorAttackPower.color = colorAttackBuff;
            else if (attackPower != 0 && voiceActor.BonusDamage < 1)
                textActorAttackPower.color = colorAttackDebuff;
            else
                textActorAttackPower.color = Color.white;

            if (attackDamage != 0 && voiceActor.BonusResistance > 1)
                textDamage.color = colorDamageDebuff;
            else if (attackDamage != 0 && voiceActor.BonusResistance < 1)
                textDamage.color = colorDamageBuff;
            else
                textDamage.color = Color.white;
        }





        // Tout ce qui est en rapport avec les HPs
        // =================================================================================================================


        public void ActorTakeDamage(VoiceActor voiceActor)
        {
            //animatorDamage.SetTrigger("Attack");
            ActorTakeDamage(voiceActor, (int) (attackDamage * voiceActor.BonusResistance));
            attackDamage = 0;
            textDamage.text = attackDamage.ToString();
            textActorAttackPower.gameObject.SetActive(false);
            DrawDamagePrevisualization(voiceActor);
        }



        public void ActorTakeDamage(VoiceActor voiceActor, int damage)
        {
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
                shakeFeedbackHealth.ShakeRectEffect();
        }













        public void ShowHealthBar(bool b)
        {
            animatorHealthBar.SetBool("Appear", b);
            if (b == false)
                textActorAttackPower.gameObject.SetActive(false);
        }


        public void DrawDamagePrevisualization(VoiceActor voiceActor)
        {
            if (voiceActor.Hp <= 0)
                return;
            float damagePercentage = ((attackDamage * voiceActor.BonusResistance) + attackDamageBonus) / voiceActor.Hp;
            damagePreviz.anchorMin = new Vector2(1 - damagePercentage, 0);
            damagePreviz.offsetMin = new Vector2(0, 0);
            textCurrentHp.text = (voiceActor.Hp - ((attackDamage * voiceActor.BonusResistance) + attackDamageBonus)).ToString();
        }








        public IEnumerator DeathCoroutine(Transform character)
        {
            yield return null;
        }

















        #endregion

    } // ActorsManager class
	
}// #PROJECTNAME# namespace
