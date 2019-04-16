/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        RectTransform[] toneTransform;
        
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
                toneTransform[i].anchorMin = new Vector2(startX, 0);
                toneTransform[i].anchorMax = new Vector2(startX + width, 1);
                startX += width;
            }
        }

        public void ModifyTone(Emotion[] emotions)
        {

        }
        
        #endregion
		
	} // ToneManager class
	
}// #PROJECTNAME# namespace
