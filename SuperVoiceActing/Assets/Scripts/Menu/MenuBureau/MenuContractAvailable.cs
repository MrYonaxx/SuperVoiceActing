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
	public class MenuContractAvailable : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Database")]
        [SerializeField]
        private ContractData[] contractDatabase;

        [Header("Prefab")]
        [SerializeField]
        private ButtonContratAvailable buttonPrefab;
        [SerializeField]
        private RectTransform buttonListTransform;
        [SerializeField]
        private Animator animatorSelection;
        [SerializeField]
        private TextMeshProUGUI textMeshSelection;

        [Header("InfoPanel")]
        [SerializeField]
        TextMeshProUGUI textInfoTitle;
        [SerializeField]
        TextMeshProUGUI textInfoType;
        [SerializeField]
        TextMeshProUGUI textInfoMoney;
        [SerializeField]
        TextMeshProUGUI textInfoWeek;

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

        private IEnumerator coroutineScroll = null;
        // ===========================


        private List<Contract> listContractAvailable = new List<Contract>();
        private List<ButtonContratAvailable> buttonsContracts = new List<ButtonContratAvailable>();

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

        private void Start()
        {
            listContractAvailable.Add(new Contract("Hello"));
            listContractAvailable.Add(new Contract("Hello2"));
            listContractAvailable.Add(new Contract("Hello3"));
            listContractAvailable.Add(new Contract(contractDatabase[0]));
            listContractAvailable.Add(new Contract(contractDatabase[0]));
            listContractAvailable.Add(new Contract(contractDatabase[0]));
            CreateListButton();
        }

        private void CreateListButton()
        {
            for(int i = 0; i < listContractAvailable.Count; i++)
            {
                buttonsContracts.Add(Instantiate(buttonPrefab, buttonListTransform));
                buttonsContracts[i].DrawButton(listContractAvailable[i].Name);
            }
        }

        public void SwitchToMenuContractManager()
        {
            menuContractManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            animatorMenu.SetBool("Appear", false);
            //animatorMenu.gameObject.SetActive(false);
        }

        private void SelectButton()
        {
            animatorSelection.transform.position = buttonsContracts[indexSelected].transform.position;
            textMeshSelection.text = listContractAvailable[indexSelected].Name;
            animatorSelection.SetTrigger("Active");
            DrawInfo();
        }

        private void DrawInfo()
        {
            textInfoTitle.text = listContractAvailable[indexSelected].Name;
            textInfoWeek.text = listContractAvailable[indexSelected].WeekRemaining.ToString();
            textInfoMoney.text = listContractAvailable[indexSelected].Money.ToString();
            // textInfoType.text = listContractAvailable[indexSelected].;
        }




        public void SelectContractUp()
        {
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }

            if (CheckRepeat() == false)
                return;

            indexSelected -= 1;
            if (indexSelected <= -1)
            {
                indexSelected = buttonsContracts.Count - 1;
            }
            SelectButton();
            MoveScrollRect();
        }

        public void SelectContractDown()
        {
            if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }
            if (CheckRepeat() == false)
                return;

            indexSelected += 1;
            if (indexSelected >= buttonsContracts.Count)
            {
                indexSelected = 0;
            }
            SelectButton();
            MoveScrollRect();

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
            if (coroutineScroll != null)
            {
                StopCoroutine(coroutineScroll);
            }
            /*if (indexActorSelected > indexActorLimit)
            {
                indexActorLimit = indexActorSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }
            else if (indexActorSelected < indexActorLimit - scrollSize)
            {
                indexActorLimit = indexActorSelected + scrollSize;
                coroutineScroll = MoveScrollRectCoroutine();
                StartCoroutine(coroutineScroll);
            }*/

        }

        /*private IEnumerator MoveScrollRectCoroutine()
        {
            int time = 10;
            int ratio = indexActorLimit - scrollSize;
            float speed = (buttonListTransform.anchoredPosition.y - ratio * buttonSize) / time;
            while (time != 0)
            {
                buttonListTransform.anchoredPosition -= new Vector2(0, speed);
                time -= 1;
                yield return null;
            }

        }*/




        #endregion

    } // MenuContractAvailable class
	
}// #PROJECTNAME# namespace
