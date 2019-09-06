/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VoiceActing
{
    [System.Serializable]
	public class MenuContractPreparationAnnexe : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        GameObject panelContract;
        [SerializeField]
        protected Animator buttonAnimator;
        [SerializeField]
        GameObject outlineButton;

        int buttonIndex;

        [SerializeField]
        UnityEventInt eventOnClick;
        [SerializeField]
        UnityEventInt eventOnSelect;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetButtonIndex(int index)
        {
            buttonIndex = index;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public virtual bool CanAdd(Contract contract)
        {
            buttonAnimator.gameObject.SetActive(true);
            DrawContract(contract);
            return true;
        }

        public virtual void DrawContract(Contract contract)
        {

        }

        public virtual void UnselectButton()
        {
            outlineButton.gameObject.SetActive(false);
            panelContract.gameObject.SetActive(false);
        }

        public virtual void SelectButton()
        {
            buttonAnimator.SetTrigger("Feedback");
            outlineButton.gameObject.SetActive(true);
            panelContract.gameObject.SetActive(true);
        }

        public virtual void ValidateButton(Contract contract)
        {

        }



        public void OnPointerEnter(PointerEventData data)
        {
            eventOnSelect.Invoke(buttonIndex);
            SelectButton();
        }


        public void OnPointerClick(PointerEventData data)
        {
            eventOnClick.Invoke(buttonIndex);
        }

        #endregion

    } // MenuContractPreparationAnnexe class
	
}// #PROJECTNAME# namespace
