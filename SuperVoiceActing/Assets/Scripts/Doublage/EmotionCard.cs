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
    /// <summary>
    /// Definition of the EmotionCard class
    /// </summary>
    public class EmotionCard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        private IEnumerator coroutine = null;
        [SerializeField]
        private RectTransform rectTransform;
        [SerializeField]
        Image imageCard;

        [Space]
        [SerializeField]
        TextMeshProUGUI textStat;
        [SerializeField]
        TextMeshProUGUI textEmotionDamage;
        [SerializeField]
        Image[] statBonus;
        [SerializeField]
        TextMeshProUGUI[] textBuffTurn;


        [SerializeField]
        Color colorBonusStat;
        [SerializeField]
        Color colorMalusStat;
        [SerializeField]
        Coffee.UIExtensions.UIDissolve uiDissolve;

        [Title("Preview")]
        [SerializeField]
        GameObject previewPanel;
        [SerializeField]
        TextMeshProUGUI textPreview;
        [SerializeField]
        TextMeshProUGUI textPreviewStat;

        private Emotion emotion;

        private int value = 0;
        private int valueBonus = 0;
        private int valueActorBonus = 0;

        private float damagePercentage = 1;

        private bool inPreview = false;

        private List<Buff> buffs = new List<Buff>();

        [SerializeField]
        UnityEventInt clickEvent;


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
            return value + valueBonus + valueActorBonus;
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
            speed = 0.1f;
            float time = 1f;
            float t = 0f;
            float blend = 0f;
            Vector2 finalPosition = Vector2.zero;

            while (t < 1f)
            {
                t += Time.deltaTime / time;
                blend = 1f - Mathf.Pow(1f - speed, Time.deltaTime * 60);
                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, finalPosition, blend);
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
                feedback.sprite = imageCard.sprite;
                feedback.color = imageCard.color;
                //StartCoroutine(FeedbackCoroutine(feedback, 20));
            }
        }

        /*private IEnumerator FeedbackCoroutine(Image feedback, int time)
        {
            while (time != 0)
            {
                feedback.color -= new Color(0, 0, 0, 0.05f);
                feedback.rectTransform.localScale += new Vector3(0.075f, 0.075f, 0);
                time -= 1;
                yield return null;
            }
        }*/







        //    S E C T I O N    S T A T

        public void AddStat(int statToAdd)
        {
            valueBonus += statToAdd;
            DrawStat();
        }

        public void AddStatPercentage(int statToAdd)
        {
            statToAdd = (int)(value * (statToAdd / 100f));
            valueBonus += statToAdd;
            DrawStat();
        }


        public void AddDamagePercentage(int damageModifier)
        {
            damagePercentage += damageModifier / 100f;
            if (textEmotionDamage != null)
            {
                textEmotionDamage.text = "x" + damagePercentage;
                textEmotionDamage.gameObject.SetActive((damagePercentage != 1));
            }
        }






        //    S E C T I O N    D R A W

        public void DrawStat()
        {
            if (textStat == null)
                return;

            if ((valueBonus + valueActorBonus) > 0)
                textStat.color = colorBonusStat;
            else if ((valueBonus + valueActorBonus) < 0)
                textStat.color = colorMalusStat;
            else
                textStat.color = Color.white;

            int finalValue = (value + valueBonus + valueActorBonus);
            if (finalValue < 0)
                textStat.text = "0";
            else
                textStat.text = finalValue.ToString();
        }

        public void DrawStat(int baseStat)
        {
            value = baseStat;

            DrawStat();

        }

        public void DrawStat(int baseStat, int actorBonusStat)
        {
            value = baseStat;
            valueActorBonus = actorBonusStat;

            DrawStat();
        }














        //    S E C T I O N    B U F F


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
                    //buffs[i].SkillEffectbuff.RemoveSkillEffectCard(this);
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










        //    P R E V I E W    S T A T

        public void DrawPreview()
        {
            previewPanel.SetActive(true);
            inPreview = true;
        }

        public void DrawPreviewStat(int previewValue, bool inPercentage = false)
        {
            DrawPreview();
            textPreviewStat.gameObject.SetActive(true);
            if(inPercentage == true)
                previewValue = (int)(value * (previewValue / 100f));
            textPreviewStat.text = previewValue.ToString();
        }

        public void DrawPreviewText(string text)
        {
            DrawPreview();
            textPreview.gameObject.SetActive(true);
            textPreview.text = text;
        }

        public void StopPreview()
        {
            previewPanel.SetActive(false);
            textPreview.gameObject.SetActive(false);
            textPreviewStat.gameObject.SetActive(false);
            inPreview = false;
        }

        public void ShowPreview()
        {
            if(inPreview == true)
                previewPanel.SetActive(true);
        }

        public void HidePreview()
        {
            previewPanel.SetActive(false);
        }






        public void DestroyCard()
        {
            uiDissolve.Play();
            StartCoroutine(DestroyCoroutine(uiDissolve.duration));
        }

        private IEnumerator DestroyCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(this.gameObject);
        }





        public void OnPointerClick(PointerEventData pointer)
        {
            clickEvent.Invoke((int)emotion);
        }

        public void OnPointerEnter(PointerEventData pointer)
        {
            imageCard.color = Color.black;
        }

        public void OnPointerExit(PointerEventData pointer)
        {
            imageCard.color = Color.white;
        }

        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace