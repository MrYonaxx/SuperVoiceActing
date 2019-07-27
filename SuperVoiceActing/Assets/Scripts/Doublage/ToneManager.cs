/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class ToneManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        EmotionStat toneMultiplier;
        [SerializeField]
        float toneMaxValue = 800.0f;

        [SerializeField]
        EmotionStat toneValue;

        [SerializeField]
        [InfoBox("Toujours un int multiple de 8")]
        int toneAddValue = 8;

        [Header("Transforms")]
        [SerializeField]
        Image[] toneTransform;
        [SerializeField]
        TextMeshProUGUI[] textTone;

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
        [ContextMenu("DrawTone")]

        public void DrawTone()
        {
            float startX = 0f;
            float width = 0f;
            float multiplier = 0f;
            float maxValue = toneValue.Sadness + toneValue.Joy + toneValue.Disgust + toneValue.Anger + toneValue.Sweetness + toneValue.Surprise + toneValue.Fear + toneValue.Trust;
            for (int i = 0; i < toneTransform.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        multiplier = toneValue.Sadness;
                        break;
                    case 1:
                        multiplier = toneValue.Joy;
                        break;
                    case 2:
                        multiplier = toneValue.Disgust;
                        break;
                    case 3:
                        multiplier = toneValue.Anger;
                        break;
                    case 4:
                        multiplier = toneValue.Sweetness;
                        break;
                    case 5:
                        multiplier = toneValue.Surprise;
                        break;
                    case 6:
                        multiplier = toneValue.Fear;
                        break;
                    case 7:
                        multiplier = toneValue.Trust;
                        break;

                }
                if (maxValue == 0)
                    width = 0;
                else
                    width = multiplier / maxValue;
                if(width == 0)
                {
                    textTone[i].gameObject.SetActive(false);
                }
                else
                {
                    textTone[i].gameObject.SetActive(true);
                    textTone[i].text = multiplier.ToString();
                }
                toneTransform[i].rectTransform.anchorMin = new Vector2(startX, 0);
                toneTransform[i].rectTransform.anchorMax = new Vector2(startX + width, 1);
                startX += width;
            }
        }



        public void ModifyTone(EmotionCard[] emotions)
        {
            for (int i = 0; i < emotions.Length; i++)
            {
                if (emotions[i] == null)
                    continue;
                switch (emotions[i].GetEmotion())
                {
                   
                    case Emotion.Joie:
                        toneValue.Joy += 1;
                        break;
                    case Emotion.Tristesse:
                        toneValue.Sadness += 1;
                        break;
                    case Emotion.Dégoût:
                        toneValue.Disgust += 1;
                        break;
                    case Emotion.Colère:
                        toneValue.Anger += 1;
                        break;
                    case Emotion.Surprise:
                        toneValue.Surprise += 1;
                        break;
                    case Emotion.Douceur:
                        toneValue.Sweetness += 1;
                        break;
                    case Emotion.Peur:
                        toneValue.Fear += 1;
                        break;
                    case Emotion.Confiance:
                        toneValue.Trust += 1;
                        break;
                }
                //toneMultiplier.Add(stat);
            }
            DrawTone();
            StopHighlight();
        }


        public void HighlightTone(Emotion emotion, bool isHighlight)
        {
            Image toneSelected = toneTransform[(int) emotion];
            /*switch (emotion)
            {
                case Emotion.Joie:
                    toneSelected = toneTransform[1];
                    break;
                case Emotion.Tristesse:
                    toneSelected = toneTransform[0];
                    break;
                case Emotion.Dégoût:
                    toneSelected = toneTransform[2];
                    break;
                case Emotion.Colère:
                    toneSelected = toneTransform[3];
                    break;
                case Emotion.Surprise:
                    toneSelected = toneTransform[5];
                    break;
                case Emotion.Douceur:
                    toneSelected = toneTransform[4];
                    break;
                case Emotion.Peur:
                    toneSelected = toneTransform[6];
                    break;
                case Emotion.Confiance:
                    toneSelected = toneTransform[7];
                    break;
                default:
                    return;
            }*/
            if(isHighlight == true)
            {
                //toneSelected.rectTransform.offsetMax = new Vector2(0, 15);
                toneSelected.color = new Color(toneSelected.color.r, toneSelected.color.g, toneSelected.color.b, 1);
            }
            else
            {
                //toneSelected.rectTransform.offsetMax = new Vector2(0, 0);
                toneSelected.color = new Color(toneSelected.color.r, toneSelected.color.g, toneSelected.color.b, 0.6f);
            }
        }

        private void StopHighlight()
        {
            for(int i = 0; i < toneTransform.Length; i++)
                toneTransform[i].color = new Color(toneTransform[i].color.r, toneTransform[i].color.g, toneTransform[i].color.b, 0.6f);
        }
        
        #endregion
		
	} // ToneManager class
	
}// #PROJECTNAME# namespace
