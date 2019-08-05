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
	public class MenuContractPreparationContract : MenuContractPreparationAnnexe
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Space]
        [SerializeField]
        TextMeshProUGUI textContractWeek;
        [SerializeField]
        TextMeshProUGUI textContractSalary;
        [SerializeField]
        TextMeshProUGUI textContractFan;
        [SerializeField]
        TextMeshProUGUI textContractLine;
        [SerializeField]
        TextMeshProUGUI textContractDescription;

        [Space]
        [SerializeField]
        TextMeshProUGUI textContractCurrentLine;
        [SerializeField]
        TextMeshProUGUI textContractTotalLine;
        [SerializeField]
        RectTransform rectTransformLineProgress;


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

        public override void DrawContract(Contract contract)
        {
            textContractWeek.text = contract.WeekRemaining.ToString();
            textContractSalary.text = contract.Money.ToString();
            int sumLine = 0;
            int sumFan = 0;
            for (int i = 0; i < contract.Characters.Count; i++)
            {
                sumLine += contract.Characters[i].Line;
                sumFan += contract.Characters[i].Fan;
            }
            textContractFan.text = sumFan.ToString();
            textContractLine.text = sumLine.ToString();
            textContractDescription.text = contract.Description;

            textContractCurrentLine.text = contract.CurrentLine.ToString();
            textContractTotalLine.text = contract.TotalLine.ToString();
            rectTransformLineProgress.localScale = new Vector3((float)contract.CurrentLine / contract.TotalLine, rectTransformLineProgress.localScale.y, rectTransformLineProgress.localScale.z);
        }


        #endregion

    } // MenuContractPreparationContract class
	
}// #PROJECTNAME# namespace
