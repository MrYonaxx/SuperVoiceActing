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
        [InfoBox("Toujours un int multiple de 8")]
        int toneAddValue = 8;

        [Header("Transforms")]
        [SerializeField]
        Image[] toneTransform;
        
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
            for(int i = 0; i < toneTransform.Length; i++)
            {
                switch(i)
                {
                    case 0:
                        multiplier = toneMultiplier.Sadness;
                        break;
                    case 1:
                        multiplier = toneMultiplier.Joy;
                        break;
                    case 2:
                        multiplier = toneMultiplier.Disgust;
                        break;
                    case 3:
                        multiplier = toneMultiplier.Anger;
                        break;
                    case 4:
                        multiplier = toneMultiplier.Sweetness;
                        break;
                    case 5:
                        multiplier = toneMultiplier.Surprise;
                        break;
                    case 6:
                        multiplier = toneMultiplier.Fear;
                        break;
                    case 7:
                        multiplier = toneMultiplier.Trust;
                        break;

                }
                width = multiplier / toneMaxValue;
                toneTransform[i].rectTransform.anchorMin = new Vector2(startX, 0);
                toneTransform[i].rectTransform.anchorMax = new Vector2(startX + width, 1);
                startX += width;
            }
        }

        public void ModifyTone(Emotion[] emotions)
        {
            int addValue = toneAddValue / 8;
            EmotionStat stat = new EmotionStat(-addValue, -addValue, -addValue, -addValue, -addValue, -addValue, -addValue, -addValue);
            for(int i = 0; i < emotions.Length; i++)
            {
                switch(emotions[i])
                {
                    case Emotion.Joie:
                        toneMultiplier.Joy += toneAddValue;
                        break;
                    case Emotion.Tristesse:
                        toneMultiplier.Sadness += toneAddValue;
                        break;
                    case Emotion.Dégoût:
                        toneMultiplier.Disgust += toneAddValue;
                        break;
                    case Emotion.Colère:
                        toneMultiplier.Anger += toneAddValue;
                        break;
                    case Emotion.Surprise:
                        toneMultiplier.Surprise += toneAddValue;
                        break;
                    case Emotion.Douceur:
                        toneMultiplier.Sweetness += toneAddValue;
                        break;
                    case Emotion.Peur:
                        toneMultiplier.Fear += toneAddValue;
                        break;
                    case Emotion.Confiance:
                        toneMultiplier.Trust += toneAddValue;
                        break;
                }
                toneMultiplier.Add(stat);
            }
            DrawTone();
        }


        public void HighlightTone(Emotion emotion, bool isHighlight)
        {
            Image toneSelected = null;
            switch (emotion)
            {
                case Emotion.Joie:
                    break;
                case Emotion.Tristesse:
                    break;
                case Emotion.Dégoût:
                    break;
                case Emotion.Colère:
                    break;
                case Emotion.Surprise:
                    break;
                case Emotion.Douceur:
                    break;
                case Emotion.Peur:
                    break;
                case Emotion.Confiance:
                    break;
            }
            if(isHighlight == true)
            {
                toneSelected.rectTransform.offsetMax = new Vector2(0, 10);
                toneSelected.color = new Color(toneSelected.color.r, toneSelected.color.g, toneSelected.color.b, 1);
            }
            else
            {
                toneSelected.rectTransform.offsetMax = new Vector2(0, 0);
                toneSelected.color = new Color(toneSelected.color.r, toneSelected.color.g, toneSelected.color.b, 0);
            }
        }
        
        #endregion
		
	} // ToneManager class
	
}// #PROJECTNAME# namespace
