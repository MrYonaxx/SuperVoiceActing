/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace VoiceActing
{
    /// <summary>
    /// Definition of the CameraBureau class
    /// </summary>
    public class ButtonContratAccepted : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Panel")]
        [SerializeField]
        GameObject panelContract;
        [SerializeField]
        GameObject panelAddContract;

        [Header("Animator")]
        [SerializeField]
        Animator outlineSelection;
        [SerializeField]
        Animator contractButton;
        [SerializeField]
        Animator contractAddButton;

        [Header("Text & Info")]
        [SerializeField]
        TextMeshProUGUI contractTitle;
        [SerializeField]
        TextMeshProUGUI contractWeekRemaining;
        [SerializeField]
        TextMeshProUGUI contractWeekRemainingShadow;
        [SerializeField]
        TextMeshProUGUI contractLine;
        [SerializeField]
        TextMeshProUGUI contractMixage;
        [SerializeField]
        RectTransform gaugeLine;

        bool isButtonAddContract = false;

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

        public void DrawContract(Contract contract)
        {
            isButtonAddContract = false;
            panelContract.SetActive(true);
            panelAddContract.SetActive(false);

            contractTitle.text = contract.Name;
            contractLine.text = contract.CurrentLine + " / " + contract.TotalLine;
            contractMixage.text = contract.CurrentMixing + " / " + contract.TotalMixing;
            contractWeekRemaining.text = contract.WeekRemaining.ToString();
            contractWeekRemainingShadow.text = contract.WeekRemaining.ToString();
            gaugeLine.transform.localScale = new Vector3((contract.CurrentLine / (float)contract.TotalLine), gaugeLine.transform.localScale.y, gaugeLine.transform.localScale.z);
        }

        public void SelectContract()
        {
            if(!isButtonAddContract)
                contractButton.SetBool("Selected", true);
            else
                contractAddButton.SetBool("Selected", true);

            outlineSelection.gameObject.SetActive(true);
        }

        public void UnSelectContract()
        {
            if (!isButtonAddContract)
                contractButton.SetBool("Selected", false);
            else
                contractAddButton.SetBool("Selected", false);

            outlineSelection.gameObject.SetActive(false);
        }

        public void SetButtonToAddContract()
        {
            isButtonAddContract = true;
            panelContract.SetActive(false);
            panelAddContract.SetActive(true);
        }

        #endregion
    }
}
