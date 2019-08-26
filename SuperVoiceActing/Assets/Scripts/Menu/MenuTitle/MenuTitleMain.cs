﻿/*****************************************************************
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
using Sirenix.OdinInspector;

namespace VoiceActing
{
	public class MenuTitleMain : MenuScrollList
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Title("Menu Title")]
        [SerializeField]
        RectTransform scrollRythmo;
        [SerializeField]
        TextMeshProUGUI[] textOptions;
        [SerializeField]
        Animator animatorMainMenu;
        [SerializeField]
        Animator animatorMainMenuDescription;


        [Title("Menu New game")]
        [SerializeField]
        int indexNewGame = 0;
        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        InitialPlayerData newGamePlayerData;
        [SerializeField]
        GameObject menuNewGame;

        [Title("Menu Load")]
        [SerializeField]
        int indexLoadGame = 1;
        [SerializeField]
        MenuSaveLoadManager menuSaveLoadManager;

        private IEnumerator scrollCoroutine = null;



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






        public void Validate()
        {
            animatorMainMenu.SetTrigger("MenuDisappear");
            animatorMainMenuDescription.SetTrigger("SubMenuAppear");
        }



        public void DrawSubMenu()
        {
            if (indexSelected == indexNewGame)
            {
                menuNewGame.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
                playerData.CreateList(newGamePlayerData);
            }
            else if (indexSelected == indexLoadGame)
            {
                menuSaveLoadManager.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }














        protected override void BeforeSelection()
        {
            base.BeforeSelection();

            textOptions[indexSelected].fontStyle = FontStyles.Normal;
        }

        protected override void AfterSelection()
        {
            base.AfterSelection();

            textOptions[indexSelected].fontStyle = FontStyles.Bold;

            if (scrollCoroutine != null)
                StopCoroutine(scrollCoroutine);
            scrollCoroutine = MoveScrollRythmoCoroutine();
            StartCoroutine(scrollCoroutine);
        }

        private IEnumerator MoveScrollRythmoCoroutine()
        {
            float time = 10f;
            //int ratio = indexLimit - scrollSize;
            Vector2 speed = new Vector2(0, (scrollRythmo.anchoredPosition.y - indexSelected * buttonSize) / time);
            while (time != 0)
            {
                scrollRythmo.anchoredPosition -= speed;
                time -= 1;
                yield return null;
            }
        }
        #endregion

    } // MenuTitleMain class
	
}// #PROJECTNAME# namespace
