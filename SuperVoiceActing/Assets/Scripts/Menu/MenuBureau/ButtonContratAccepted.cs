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

        [Header("Animator")]
        [SerializeField]
        Animator outlineSelection;
        [SerializeField]
        Animator contractButton;

        [Header("Text & Info")]
        [SerializeField]
        TextMeshProUGUI contractTitle;
        [SerializeField]
        TextMeshProUGUI contractWeekRemaining;

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
            contractTitle.text = contract.Name;
        }

        public void SelectContract()
        {
            contractButton.SetBool("Selected", true);
            outlineSelection.gameObject.SetActive(true);
        }

        public void UnSelectContract()
        {
            contractButton.SetBool("Selected", false);
            outlineSelection.gameObject.SetActive(false);
        }

        #endregion
    }
}
