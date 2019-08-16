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
	public class MenuDoor : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        Animator[] buttonsDoor;

        [SerializeField]
        int indexShop = 0;
        [SerializeField]
        int indexSkipWeek = 1;

        [SerializeField]
        Animator animatorShopTransition;
        [SerializeField]
        Animator animatorsSkipWeek;
        [SerializeField]
        CameraBureau cameraBureau;
        [SerializeField]
        InputController inputController;
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
        protected override void Start()
        {
            base.Start();
            indexSelected = 0;
            buttonsDoor[indexSelected].SetTrigger("Selected");
        }

        private void OnEnable()
        {
            inputController.gameObject.SetActive(true);
            cameraBureau.ZoomCameraOff();
        }

        protected override void BeforeSelection()
        {
            buttonsDoor[indexSelected].SetTrigger("Unselected");
        }

        protected override void AfterSelection()
        {
            buttonsDoor[indexSelected].SetTrigger("Selected");
        }

        public void Validate()
        {
            inputController.gameObject.SetActive(false);
            if (indexSelected == indexShop)
            {
                animatorShopTransition.gameObject.SetActive(true);
                animatorShopTransition.SetTrigger("Appear");
            }
            else if (indexSelected == indexSkipWeek)
            {
                cameraBureau.MoveToCameraInstant(2);
                cameraBureau.HideHUD();
                animatorsSkipWeek.SetTrigger("SkipWeek");
                AudioManager.Instance.StopMusic(360);

            }
        }


        #endregion

    } // MenuDoor class
	
}// #PROJECTNAME# namespace
