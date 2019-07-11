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
        Image equipementCategoryImage;
        [SerializeField]
        TextMeshProUGUI equipementName;
        [SerializeField]
        TextMeshProUGUI equipementCost;

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
        }
        
        #endregion
		
	} // ButtonEquipement class
	
}// #PROJECTNAME# namespace
