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
using UnityEngine.EventSystems;

namespace VoiceActing
{
	public class ButtonEquipement : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        RectTransform equipementRectTransform;
        [SerializeField]
        Image equipementCategoryImage;
        [SerializeField]
        TextMeshProUGUI equipementName;
        [SerializeField]
        TextMeshProUGUI equipementCost;

        [SerializeField]
        int buttonIndex = 0;
        [SerializeField]
        UnityEventInt eventOnEnter;
        [SerializeField]
        UnityEventInt eventOnClick;


        Equipement equipement;

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

        public void DrawEquipement(Equipement eqData, int money, int i)
        {
            if (eqData == null)
            {
                equipementName.text = "----------";
                equipementCost.text = "---";
            }
            else
            {
                equipementName.text = eqData.EquipmentName;
                equipementCost.text = money.ToString();
            }
            equipement = eqData;
            buttonIndex = i;
        }

        public RectTransform GetRectTransform()
        {
            return equipementRectTransform;
        }

        public Equipement GetEquipement()
        {
            return equipement;
        }



        // Je devrai faire un autre script mais pour une fonction nique
        public void DrawFormation(FormationData formData, int soundEngiID)
        {
            if (soundEngiID > formData.FormationSkills.Length)
                soundEngiID = 0;
            if (formData.FormationSkills[soundEngiID] == null)
            {
                equipementName.text = "---";
                equipementCost.text = "";
            }
            else
            {
                equipementName.text = formData.FormationSkills[soundEngiID].SkillName;
                equipementCost.text = formData.FormationTime.ToString();
            }
        }


        public void OnPointerEnter(PointerEventData data)
        {
            eventOnEnter.Invoke(buttonIndex);
        }
        public void OnPointerClick(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }

        #endregion

    } // ButtonEquipement class
	
}// #PROJECTNAME# namespace
