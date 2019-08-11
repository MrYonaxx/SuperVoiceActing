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
using UnityEngine.UI;


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
        [SerializeField]
        Animator contractWeek;

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
        Image contractIcon;
        [SerializeField]
        RectTransform gaugeLine;
        [SerializeField]
        RectTransform gaugeMixing;
        [SerializeField]
        GameObject gameObjectLine;
        [SerializeField]
        GameObject gameObjectMixing;

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
            contractIcon.sprite = contract.IconSprite;

            if (contract.TotalLine <= 0)
            {
                gameObjectLine.SetActive(false);
            }
            else
            {
                gameObjectLine.SetActive(true);
                gaugeLine.transform.localScale = new Vector2((contract.CurrentLine / (float)contract.TotalLine), gaugeLine.transform.localScale.y);
            }

            if(contract.TotalMixing <= 0)
            {
                gameObjectMixing.SetActive(false);
            }
            else
            {
                gameObjectMixing.SetActive(true);
                gaugeMixing.transform.localScale = new Vector2((contract.CurrentMixing / (float)contract.TotalMixing), gaugeLine.transform.localScale.y);
            }
        }

        public void SelectContract()
        {
            if(!isButtonAddContract)
                contractButton.SetBool("Selected", true);
            else
                contractAddButton.SetBool("Selected", true);

            outlineSelection.gameObject.SetActive(true);
            contractWeek.enabled = true;
        }

        public void UnSelectContract()
        {
            if (!isButtonAddContract)
                contractButton.SetBool("Selected", false);
            else
                contractAddButton.SetBool("Selected", false);

            outlineSelection.gameObject.SetActive(false);
            contractWeek.enabled = false;
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
