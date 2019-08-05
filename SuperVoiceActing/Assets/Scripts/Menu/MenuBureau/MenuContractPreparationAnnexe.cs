/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
    [System.Serializable]
	public class MenuContractPreparationAnnexe : MonoBehaviour
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

        #endregion

    } // MenuContractPreparationAnnexe class
	
}// #PROJECTNAME# namespace
