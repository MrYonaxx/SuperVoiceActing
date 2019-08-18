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
	public class ButtonEquipement : MonoBehaviour
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

        EquipementData equipement;

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

        public void DrawEquipement(EquipementData eqData)
        {
            if (eqData == null)
            {
                equipementName.text = "----------";
                equipementCost.text = "---";
            }
            else
            {
                equipementName.text = eqData.EquipmentName;
                equipementCost.text = eqData.Maintenance.ToString();
            }
            equipement = eqData;
        }

        public RectTransform GetRectTransform()
        {
            return equipementRectTransform;
        }

        public EquipementData GetEquipement()
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




        #endregion

    } // ButtonEquipement class
	
}// #PROJECTNAME# namespace
