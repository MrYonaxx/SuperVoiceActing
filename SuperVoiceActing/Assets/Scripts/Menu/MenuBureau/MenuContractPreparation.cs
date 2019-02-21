/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VoiceActing
{
	public class MenuContractPreparation : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Info")]
        [SerializeField]
        TextMeshProUGUI textContractTitle;
        [SerializeField]
        TextMeshProUGUI textContractSalaire;
        [SerializeField]
        TextMeshProUGUI textContractLine;
        [SerializeField]
        TextMeshProUGUI textContractLineMax;
        [SerializeField]
        TextMeshProUGUI textContractMixage;
        [SerializeField]
        TextMeshProUGUI textContractMixageMax;

        [Header("InfoRole")]
        [SerializeField]
        TextMeshProUGUI textRoleName;
        [SerializeField]
        TextMeshProUGUI textRoleFan;
        [SerializeField]
        TextMeshProUGUI textRoleLine;

        [Header("Prefabs")]
        [SerializeField]
        ButtonPreparationRole[] listButtonRoles;

        [Header("MenuManagers")]
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuContratManager menuContractManager;


        // ===========================
        // Menu Input (Sans doute mettre dans une classe parent mais flemme)
        [Header("Menu Input")]
        [SerializeField]
        private int scrollSize = 8;
        [SerializeField]
        private int buttonSize = 85; // à voir si on peut pas automatiser
        [SerializeField]
        private int timeBeforeRepeat = 10;
        [SerializeField]
        private int repeatInterval = 3;

        private int currentTimeBeforeRepeat = -1;
        private int currentRepeatInterval = -1;
        private int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        private int indexLimit = 0;

        private IEnumerator coroutineScroll = null;
        // ===========================


        private Contract currentContract = null;
        private int indexSelected = 0;

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

        private void OnEnable()
        {
            animatorMenu.SetBool("Appear", true);
            animatorMenu.gameObject.SetActive(true);
        }

        public void SwitchToMenuContractManager()
        {
            menuContractManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            animatorMenu.SetBool("Appear", false);
        }

        public void SetContract(Contract contract)
        {
            currentContract = contract;
            DrawContractInfo();
            DrawButtons();
        }

        private void DrawContractInfo()
        {
            textContractTitle.text = currentContract.Name;
            textContractSalaire.text = currentContract.Money.ToString();
            textContractLine.text = currentContract.CurrentLine.ToString();
            textContractLineMax.text = currentContract.TotalLine.ToString();
            textContractMixage.text = currentContract.CurrentMixing.ToString();
            textContractMixageMax.text = currentContract.TotalMixing.ToString();
        }

        private void DrawButtons()
        {
            //VoiceActor tmp = null;
            for(int i = 0; i < listButtonRoles.Length; i++)
            {
                if(i < currentContract.Characters.Count)
                {
                    listButtonRoles[i].gameObject.SetActive(true);
                    listButtonRoles[i].DrawButton(currentContract.Characters[i], currentContract.VoiceActors[i]);
                    listButtonRoles[i].UnselectButton();
                }
                else
                {
                    listButtonRoles[i].gameObject.SetActive(false);
                }
            }

            listButtonRoles[0].SelectButton();

        }

        private void DrawRoleInfo()
        {
            textRoleName.text = currentContract.Characters[indexSelected].Name;
            textRoleFan.text = currentContract.Characters[indexSelected].Fan.ToString();
            textRoleLine.text = currentContract.Characters[indexSelected].Line.ToString();
        }


        // =================================================================
        public void SelectRoleUp()
        {
            if (currentContract.Characters.Count == 0)
            {
                return;
            }
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }

            if (CheckRepeat() == false)
                return;

            listButtonRoles[indexSelected].UnselectButton();

            indexSelected -= 1;
            if (indexSelected <= -1)
            {
                indexSelected = currentContract.Characters.Count - 1;
            }
            MoveScrollRect();
            DrawRoleInfo();
            listButtonRoles[indexSelected].SelectButton();
        }

        public void SelectRoleDown()
        {
            if (currentContract.Characters.Count == 0)
            {
                return;
            }
            if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }
            if (CheckRepeat() == false)
                return;

            listButtonRoles[indexSelected].UnselectButton();

            indexSelected += 1;
            if (indexSelected >= currentContract.Characters.Count)
            {
                indexSelected = 0;
            }
            MoveScrollRect();
            DrawRoleInfo();
            listButtonRoles[indexSelected].SelectButton();

        }


        // Check si on peut repeter l'input
        private bool CheckRepeat()
        {
            if (currentRepeatInterval == -1)
            {
                if (currentTimeBeforeRepeat == -1)
                {
                    currentTimeBeforeRepeat = timeBeforeRepeat;
                    return true;
                }
                else if (currentTimeBeforeRepeat == 0)
                {
                    currentRepeatInterval = repeatInterval;
                }
                else
                {
                    currentTimeBeforeRepeat -= 1;
                }
            }
            else if (currentRepeatInterval == 0)
            {
                currentRepeatInterval = repeatInterval;
                return true;
            }
            else
            {
                currentRepeatInterval -= 1;
            }
            return false;
        }

        public void StopRepeat()
        {
            currentRepeatInterval = -1;
            currentTimeBeforeRepeat = -1;
        }


        private void MoveScrollRect()
        {
            /*if (coroutineScroll != null)
            {
                StopCoroutine(coroutineScroll);
            }
            if (indexSelected > indexLimit)
            {
                indexLimit = indexSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }
            else if (indexSelected < indexLimit - scrollSize)
            {
                indexLimit = indexSelected + scrollSize;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }*/

        }

        /*private IEnumerator MoveScrollRectCoroutine()
        {
            int time = 10;
            int ratio = indexLimit - scrollSize;
            float speed = (buttonListTransform.anchoredPosition.y - ratio * buttonSize) / time;
            while (time != 0)
            {
                buttonListTransform.anchoredPosition -= new Vector2(0, speed);
                time -= 1;
                yield return null;
            }

        }*/




        #endregion

    } // MenuContractPreparation class
	
}// #PROJECTNAME# namespace
