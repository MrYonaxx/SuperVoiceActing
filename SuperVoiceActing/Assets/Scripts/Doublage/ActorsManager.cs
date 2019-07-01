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
        EffectManager effectManagerDeath;


        // Joie > Tristesse > Dégout > Colère > Surprise > Douceur > Peur > Confiance
        [Title("Stat Card Drawing")]
        [SerializeField]
        Color colorStatBonus;
        [SerializeField]
        Color colorStatMalus;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardJoy;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardSadness;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardDisgust;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardAnger;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardSurprise;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardSweetness;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardFear;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardTrust;
        [FoldoutGroup("Cards")]
        [SerializeField]
        EmotionCard[] cardNeutral;

        [Space]
        [SerializeField]
        Image[] currentActorBuff;
        [SerializeField]
        TextMeshProUGUI[] currentActorBuffTimer;

        private int indexCurrentActor = 0;
        private int[] actorsResistance = { 0, 0, 0 };
        int attackDamage = 0;



        [Title("SwitchActorFeedback")]
        [SerializeField]
        Animator animatorSwitchActors;
        [SerializeField]
        GameObject spriteSwitchPanel;
        [SerializeField]
        SpriteRenderer spriteCurrentActor;
        [SerializeField]
        SpriteRenderer spriteNextActor;






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


        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public VoiceActor GetCurrentActor()
        {
            return actors[indexCurrentActor];
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

        public bool ApplyBuff(SkillEffectData buff, BuffData buffData)
        {
            for(int i = 0; i < actors[indexCurrentActor].Buffs.Count; i++)
            {
                if (actors[indexCurrentActor].Buffs[i].SkillEffectbuff == buff)
                {
                    if (buffData.CanAddMultiple == false)
                    {
                        if (buffData.Refresh == true)
                        {
                            actors[indexCurrentActor].Buffs[i].Turn = buffData.TurnActive;
                        }
                        else if (buffData.AddBuffTurn == true)
                        {
                            actors[indexCurrentActor].Buffs[i].Turn += buffData.TurnActive;
                        }
                        return false;
                    }
                }
            }
            actors[indexCurrentActor].Buffs.Add(new Buff(buff, buffData));
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
                    currentActorBuffTimer[i].text = actors[indexCurrentActor].Buffs[i].Turn.ToString();
                }
                else
                {
                    currentActorBuff[i].gameObject.SetActive(false);
                }
            }
        }

        public void CheckBuffs()
        {
            for (int i = 0; i < 9; i++)
            {
                EmotionCard[] pack = null;
                switch (i)
                {
                    case 0: // Joie
                        pack = cardJoy;
                        break;
                    case 1: // Tristesse
                        pack = cardSadness;
                        break;
                    case 2: // Dégout
                        pack = cardDisgust;
                        break;
                    case 3: // Colère
                        pack = cardAnger;
                        break;
                    case 4: // Surprise
                        pack = cardSurprise;
                        break;
                    case 5: // Douceur
                        pack = cardSweetness;
                        break;
                    case 6: // Peur
                        pack = cardFear;
                        break;
                    case 7: // Confiance
                        pack = cardTrust;
                        break;
                    case 8: // Neutral
                        pack = cardNeutral;
                        break;
                }

                for (int j = 0; j < pack.Length; j++)
                {
                    pack[j].CheckBuff();
                }
            }
        }





        public EmotionCard GetCardTarget(Vector3Int cardTarget)
        {
            switch (cardTarget.x)
            {
                case 0: // Neutral
                    return cardNeutral[cardTarget.y];
                case 1: // Joie
                    return cardJoy[cardTarget.y];
                case 2: // Tristesse
                    return cardSadness[cardTarget.y];
                case 3: // Dégout
                    return cardDisgust[cardTarget.y];
                case 4: // Colère
                    return cardAnger[cardTarget.y];
                case 5: // Surprise
                    return cardSurprise[cardTarget.y];
                case 6: // Douceur
                    return cardSweetness[cardTarget.y];
                case 7: // Peur
                    return cardFear[cardTarget.y];
                case 8: // Confiance
                    return cardTrust[cardTarget.y];
            }
            return null;
        }



        public void InvertActorStat(Emotion emotionA, Emotion emotionB)
        {
            // A refaire
            EmotionCard[] packA = null;
            EmotionCard[] packB = null;
            switch (emotionA)
            {
                case Emotion.Joie: // Joie
                    packA = cardJoy;
                    break;
                case Emotion.Tristesse: // Tristesse
                    packA = cardSadness;
                    break;
                case Emotion.Dégoût: // Dégout
                    packA = cardDisgust;
                    break;
                case Emotion.Colère: // Colère
                    packA = cardAnger;
                    break;
                case Emotion.Surprise: // Surprise
                    packA = cardSurprise;
                    break;
                case Emotion.Douceur: // Douceur
                    packA = cardSweetness;
                    break;
                case Emotion.Peur: // Peur
                    packA = cardFear;
                    break;
                case Emotion.Confiance: // Confiance
                    packA = cardTrust;
                    break;
                case Emotion.Neutre: // Neutral
                    packA = cardNeutral;
                    break;
            }
            switch (emotionB)
            {
                case Emotion.Joie: // Joie
                    packB = cardJoy;
                    break;
                case Emotion.Tristesse: // Tristesse
                    packB = cardSadness;
                    break;
                case Emotion.Dégoût: // Dégout
                    packB = cardDisgust;
                    break;
                case Emotion.Colère: // Colère
                    packB = cardAnger;
                    break;
                case Emotion.Surprise: // Surprise
                    packB = cardSurprise;
                    break;
                case Emotion.Douceur: // Douceur
                    packB = cardSweetness;
                    break;
                case Emotion.Peur: // Peur
                    packB = cardFear;
                    break;
                case Emotion.Confiance: // Confiance
                    packB = cardTrust;
                    break;
                case Emotion.Neutre: // Neutral
                    packB = cardNeutral;
                    break;
            }

            int[] tmpBaseValue = new int[3];
            int[] tmpBaseBonusValue = new int[3];
            //Buff[] buffs

            for (int i = 0; i < packB.Length; i++)
            {
                if (packA[i] == null)
                {
                    tmpBaseValue[i] = tmpBaseValue[i - 1];
                    tmpBaseBonusValue[i] = tmpBaseBonusValue[i - 1];
                    packB[i].DrawStat(packA[i].GetBaseValue(), packA[i].GetBonusValue(), colorStatBonus, colorStatMalus);
                }
                else if (packB[i] != null)
                {
                    tmpBaseValue[i] = packB[i].GetBaseValue();
                    tmpBaseBonusValue[i] = packB[i].GetBonusValue();
                    packB[i].DrawStat(packA[i].GetBaseValue(), packA[i].GetBonusValue(), colorStatBonus, colorStatMalus);
                }
            }
            for (int i = 0; i < packA.Length; i++)
            {
                if (packB[i] != null)
                {
                    packA[i].DrawStat(tmpBaseValue[i], tmpBaseBonusValue[i], colorStatBonus, colorStatMalus);
                }
            }
        }



        public void AddActorStat(List<Vector3Int> cardTargetsData, Buff buff = null)
        {
            for(int i = 0; i < cardTargetsData.Count; i++)
            {
                GetCardTarget(cardTargetsData[i]).AddStat(cardTargetsData[i].z, buff);
            }
        }

        public void RemoveActorStat(List<Vector3Int> cardTargetsData)
        {
            for (int i = 0; i < cardTargetsData.Count; i++)
            {
                GetCardTarget(cardTargetsData[i]).AddStat(-cardTargetsData[i].z);
            }
        }



        public void AddActorStatPercentage(List<Vector3Int> cardTargetsData)
        {
            for (int i = 0; i < cardTargetsData.Count; i++)
            {
                GetCardTarget(cardTargetsData[i]).AddStatPercentage(cardTargetsData[i].z);
            }
        }

        public void RemoveActorStatPercentage(List<Vector3Int> cardTargetsData)
        {
            for (int i = 0; i < cardTargetsData.Count; i++)
            {
                GetCardTarget(cardTargetsData[i]).AddStatPercentage(-cardTargetsData[i].z);
            }
        }



        public void AddActorEmotionDamage(List<Vector3Int> cardTargetsData, Buff buff = null)
        {
            for (int i = 0; i < cardTargetsData.Count; i++)
            {
                GetCardTarget(cardTargetsData[i]).AddDamagePercentage(cardTargetsData[i].z, buff);
            }
        }

        public void RemoveActorEmotionDamage(List<Vector3Int> cardTargetsData)
        {
            for (int i = 0; i < cardTargetsData.Count; i++)
            {
                GetCardTarget(cardTargetsData[i]).AddDamagePercentage(-cardTargetsData[i].z);
            }
        }




        private void DrawActorStat()
        {
            // Joie > Tristesse > Dégout > Colère > Surprise > Douceur > Peur > Confiance

            for(int i = 0; i < 9; i++)
            {
                EmotionCard[] pack = null;
                int newStatValue = 0;
                int newStatModifier = 0;
                switch (i)
                {
                    case 0: // Joie
                        pack = cardJoy;
                        newStatValue = actors[indexCurrentActor].Statistique.Joy;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Joy;
                        break;
                    case 1: // Tristesse
                        pack = cardSadness;
                        newStatValue = actors[indexCurrentActor].Statistique.Sadness;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Sadness;
                        break;
                    case 2: // Dégout
                        pack = cardDisgust;
                        newStatValue = actors[indexCurrentActor].Statistique.Disgust;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Disgust;
                        break;
                    case 3: // Colère
                        pack = cardAnger;
                        newStatValue = actors[indexCurrentActor].Statistique.Anger;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Anger;
                        break;
                    case 4: // Surprise
                        pack = cardSurprise;
                        newStatValue = actors[indexCurrentActor].Statistique.Surprise;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Surprise;
                        break;
                    case 5: // Douceur
                        pack = cardSweetness;
                        newStatValue = actors[indexCurrentActor].Statistique.Sweetness;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Sweetness;
                        break;
                    case 6: // Peur
                        pack = cardFear;
                        newStatValue = actors[indexCurrentActor].Statistique.Fear;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Fear;
                        break;
                    case 7: // Confiance
                        pack = cardTrust;
                        newStatValue = actors[indexCurrentActor].Statistique.Trust;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Trust;
                        break;
                    case 8: // Neutral
                        pack = cardNeutral;
                        newStatValue = actors[indexCurrentActor].Statistique.Neutral;
                        newStatModifier = actors[indexCurrentActor].StatModifier.Neutral;
                        break;
                }


                for(int j = 0; j < pack.Length; j++)
                {
                    pack[j].DrawStat(newStatValue, newStatModifier, colorStatBonus, colorStatMalus);
                }
            }

            if(actors[indexCurrentActor].Hp < actors[indexCurrentActor].HpMax * healthCriticalThreshold)
            {
                textCurrentHp.color = healthCriticalColor;
                textMaxHp.color = healthCriticalColor;
            }

            textCurrentHp.text = actors[indexCurrentActor].Hp.ToString();
            textMaxHp.text = actors[indexCurrentActor].HpMax.ToString();

        }










        // Tout ce qui est en rapport avec les HPs
        // =================================================================================================================

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
            
            actors[indexCurrentActor].Hp -= damage;
            if (actors[indexCurrentActor].Hp < 0)
                actors[indexCurrentActor].Hp = 0;
            else if (actors[indexCurrentActor].Hp > actors[indexCurrentActor].HpMax)
                actors[indexCurrentActor].Hp = actors[indexCurrentActor].HpMax;
            textCurrentHp.text = actors[indexCurrentActor].Hp.ToString();

            float ratioHP = (float) actors[indexCurrentActor].Hp / actors[indexCurrentActor].HpMax;
            healthContent.transform.localScale = new Vector3(ratioHP, 1, 1);
            if (actors[indexCurrentActor].Hp < actors[indexCurrentActor].HpMax * healthCriticalThreshold)
            {
                textCurrentHp.color = healthCriticalColor;
                textMaxHp.color = healthCriticalColor;
            }
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
            float intensity = 100;
            Vector2 origin = healthBar.anchoredPosition;
            while (timeShake != 0)
            {
                timeShake -= 1;
                healthBar.anchoredPosition = new Vector2(origin.x + Random.Range(-intensity, intensity), origin.y);
                intensity *= 0.9f;
                yield return null;
            }
            healthBar.anchoredPosition = origin;

            Vector3 speed = new Vector3((healthContent.transform.localScale.x - healthContentProgression.transform.localScale.x) / timeGauge, 0, 0);
            while (timeGauge != 0)
            {
                timeGauge -= 1;
                healthContentProgression.transform.localScale += speed;
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
            attackDamage += (int) ((roleAttack - (roleAttack * ((actors[indexCurrentActor].RoleDefense * defenseStack) / 100f))) * emotionMultiplier);
            attackDamage -= (int)(attackDamage * (actorsResistance[indexCurrentActor] / 100f));
            textDamage.text = attackDamage.ToString();
            DrawDamagePrevisualization();
            animatorDamage.SetBool("Appear", true);
        }

        public void RemoveAttackDamage(int roleAttack, float emotionMultiplier)
        {
            attackDamage -= (int)((roleAttack - (roleAttack * ((actors[indexCurrentActor].RoleDefense * defenseStack) / 100f))) * emotionMultiplier);
            attackDamage -= (int)(attackDamage * (actorsResistance[indexCurrentActor] / 100f));
            //textDamage.enabled = true;
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

            if (damagePercentage >= 1)
            {
                effectManagerDeath.BlurScreen(true);
            }
            else
            {
                effectManagerDeath.BlurScreen(false);
            }
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











        public void SwitchActors(bool switchDirectionRight)
        {
            spriteSwitchPanel.SetActive(true);
            spriteCurrentActor.sprite = actors[indexCurrentActor].ActorSprite;
            if (switchDirectionRight == true)
            {
                spriteNextActor.sprite = actors[2].ActorSprite;
                animatorSwitchActors.SetTrigger("Right");
            }
            else
            {
                spriteNextActor.sprite = actors[1].ActorSprite;
                animatorSwitchActors.SetTrigger("Right");
            }
        }





        #endregion

    } // ActorsManager class
	
}// #PROJECTNAME# namespace
