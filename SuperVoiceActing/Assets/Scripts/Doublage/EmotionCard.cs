/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the EmotionCard class
    /// </summary>
    public class EmotionCard : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        private IEnumerator coroutine = null;
        private RectTransform rectTransform;
        private Image image;

        [SerializeField]
        Image imageCard;
        [SerializeField]
        TextMeshProUGUI textStat;
        [SerializeField]
        TextMeshProUGUI textEmotionDamage;
        [SerializeField]
        Image[] statBonus;
        [SerializeField]
        TextMeshProUGUI[] textBuffTurn;



        Color bonusStat;
        Color malusStat;

        private Emotion emotion;

        private int value = 0;
        private int valueBonus = 0;

        private float damagePercentage = 1;

        private List<Buff> buffs = new List<Buff>();


        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public int GetBaseValue()
        {
            return value;
        }

        public int GetBonusValue()
        {
            return valueBonus;
        }

        public int GetStat()
        {
            return value + valueBonus;
        }

        public float GetDamagePercentage()
        {
            return damagePercentage;
        }

        public Emotion GetEmotion()
        {
            return emotion;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }

        public void SetEmotion(Emotion emo)
        {
            emotion = emo;
        }
        public void SetSprite(Sprite sprite)
        {
            imageCard.sprite = sprite;
        }

        public void MoveCard(RectTransform newTransform, float speed)
        {
            this.transform.SetParent(newTransform);
            rectTransform.localScale = new Vector3(1, 1, 1);
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = MoveToOrigin(speed);
            StartCoroutine(coroutine);
        }

        private IEnumerator MoveToOrigin(float speed)
        {
            while (rectTransform.anchoredPosition != Vector2.zero)
            {
                rectTransform.anchoredPosition /= speed;
                yield return null;
            }
            rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
            coroutine = null;
        }

        public void FeedbackCardSelected(Image feedback)
        {
            if (feedback != null)
            {
                feedback.transform.position = this.transform.position;//.anchoredPosition;
                feedback.rectTransform.localScale = this.rectTransform.localScale;
                feedback.sprite = image.sprite;
                feedback.color = image.color;
                StartCoroutine(FeedbackCoroutine(feedback, 20));
            }
        }

        private IEnumerator FeedbackCoroutine(Image feedback, int time)
        {
            //feedback.color = new Color(1, 1, 1, 1);
            while (time != 0)
            {
                feedback.color -= new Color(0, 0, 0, 0.05f);
                feedback.rectTransform.localScale += new Vector3(0.075f, 0.075f, 0);
                time -= 1;
                yield return null;
            }
        }



        /*public void SetStat(int newBaseValue, int newBonusValue)
        {
                        value = baseStat;
            valueBonus = newStat;
        }*/

        public void AddStat(int statToAdd, Buff buff = null)
        {
            if (buff != null)
            {
                if(AddBuff(buff) == false)
                    return;
            }
            valueBonus += statToAdd;
            DrawStat(value, valueBonus, bonusStat, malusStat);
        }

        public void AddStatPercentage(int statToAdd, Buff buff = null)
        {
            if (buff != null)
                AddBuff(buff);
            statToAdd = value * (statToAdd / 100);
            valueBonus += statToAdd;
            DrawStat(value, valueBonus, bonusStat, malusStat);
        }

        public void DrawStat(int baseStat, int newStat, Color colorBonus, Color colorMalus)
        {
            bonusStat = colorBonus;
            malusStat = colorMalus;
            value = baseStat;
            valueBonus = newStat;
            if (textStat == null)
                return;
            if(newStat > 0)
                textStat.color = bonusStat;
            else if (newStat < 0)
                textStat.color = malusStat;
            else
                textStat.color = Color.white;
            textStat.text = (baseStat + newStat).ToString();
        }







        public void AddDamagePercentage(int damageModifier, Buff buff = null)
        {
            if (buff != null)
            {
                if (AddBuff(buff) == false)
                    return;
            }
            damagePercentage += damageModifier / 100f;
            if (textEmotionDamage != null)
            {
                textEmotionDamage.text = "x"+damagePercentage;
                textEmotionDamage.gameObject.SetActive((damagePercentage != 1));
            }
        }






        public bool AddBuff(Buff buff)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                if (buffs[i].SkillEffectbuff == buff.SkillEffectbuff)
                {
                    if (buff.BuffData.CanAddMultiple == false)
                    {
                        if (buff.BuffData.Refresh == true)
                        {
                            buffs[i].Turn = buff.BuffData.TurnActive;
                        }
                        else if (buff.BuffData.AddBuffTurn == true)
                        {
                            buffs[i].Turn += buff.BuffData.TurnActive;
                        }
                        return false;
                    }
                }
            }
            buffs.Add(new Buff(buff.SkillEffectbuff, buff.BuffData));
            DrawBuffs();
            return true;
        }

        public void CheckBuff()
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                buffs[i].Turn -= 1;
                if (buffs[i].Turn == 0)
                {
                    buffs[i].SkillEffectbuff.RemoveSkillEffectCard(this);
                    buffs.RemoveAt(i);                   
                }
            }
            DrawBuffs();
        }

        public void DrawBuffs()
        {
            for(int i = 0; i < statBonus.Length; i++)
            {
                if (i < buffs.Count)
                {
                    statBonus[i].gameObject.SetActive(true);
                    textBuffTurn[i].text = buffs[i].Turn.ToString();
                }
                else
                {
                    statBonus[i].gameObject.SetActive(false);
                }
            }
        }

        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace