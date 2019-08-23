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

namespace VoiceActing
{
	public class ButtonResearch : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        RectTransform rectTransform;

        [SerializeField]
        TextMeshProUGUI textResearchName;
        [SerializeField]
        TextMeshProUGUI textResearchPrice;
        [SerializeField]
        TextMeshProUGUI textResearchLevel;
        [SerializeField]
        RectTransform gaugeResearchLevel;
        [SerializeField]
        GameObject gaugeResearch;

        [SerializeField]
        Color colorNormal;
        [SerializeField]
        Color colorMax;
        [SerializeField]
        Color colorUnavailable;

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

        public void DrawResearch(ResearchData data, int playerLevel)
        {
            textResearchName.text = data.ResearchName;
            gaugeResearchLevel.transform.localScale = new Vector2((float)playerLevel / data.ResearchPrice.Length, gaugeResearchLevel.transform.localScale.y);

            if (playerLevel == data.ResearchPrice.Length)
            {
                textResearchPrice.text = " ";
                textResearchLevel.text = "Max";
                textResearchName.color = colorMax;
                textResearchLevel.color = colorMax;
                gaugeResearch.gameObject.SetActive(false);
            }
            else
            {
                textResearchPrice.text = data.ResearchPrice[playerLevel].ToString() + " PR";
                textResearchLevel.text = "Lv" + (playerLevel + 1).ToString();
                textResearchName.color = colorNormal;
                textResearchLevel.color = colorNormal;
                gaugeResearch.gameObject.SetActive(true);
            }
        }

        public RectTransform GetRectTransform()
        {
            return rectTransform;
        }
        
        #endregion
		
	} // ButtonResearch class
	
}// #PROJECTNAME# namespace
