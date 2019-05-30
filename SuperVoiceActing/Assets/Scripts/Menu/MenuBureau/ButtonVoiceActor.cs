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

namespace VoiceActing
{
	public class ButtonVoiceActor : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        TextMeshProUGUI actorName;

        [SerializeField]
        TextMeshProUGUI actorLevel;

        [SerializeField]
        RectTransform actorHealth;
        [SerializeField]
        RectTransform rectTransform;

        [SerializeField]
        Image actorImage;

        [SerializeField]
        Image buttonImage;
        [SerializeField]
        Color colorSelected;
        [SerializeField]
        Color colorUnSelected;


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


        public void DrawActor(string name, int level, float hpPercentage)
        {
            actorName.text = name;
            actorLevel.text = "Lv." + level;
            actorHealth.transform.localScale = new Vector3(hpPercentage, 1, 1);
        }

        public void SelectButton()
        {
            actorImage.enabled = false;
            buttonImage.color = colorSelected;
        }

        public void UnSelectButton()
        {
            actorImage.enabled = true;
            buttonImage.color = colorUnSelected;
        }

        public void DrawAudition()
        {
            actorName.text = "Audition";
            actorImage.gameObject.SetActive(false);
            actorLevel.gameObject.SetActive(false);
        }

        public Vector2 GetAnchoredPosition()
        {
            return rectTransform.anchoredPosition;
        }

        #endregion

    } // ButtonVoiceActor class
	
}// #PROJECTNAME# namespace
