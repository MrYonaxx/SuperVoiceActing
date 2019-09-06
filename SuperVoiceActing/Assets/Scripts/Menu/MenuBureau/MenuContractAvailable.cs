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
using Sirenix.OdinInspector;


namespace VoiceActing
{
	public class MenuContractAvailable : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Data")]
        [SerializeField]
        ImageDictionnary typeContractData;

        [Header("Prefab")]
        [SerializeField]
        private ButtonContratAvailable buttonPrefab;
        [SerializeField]
        private RectTransform buttonListTransform;
        [SerializeField]
        private Animator animatorSelection;
        [SerializeField]
        private RectTransform rectTransformSelection;
        [SerializeField]
        private TextMeshProUGUI textMeshSelection;
        [SerializeField]
        private Image iconSelection;


        [Header("InfoPanel")]
        [SerializeField]
        TextMeshProUGUI textInfoTitle;
        [SerializeField]
        TextMeshProUGUI textInfoType;
        [SerializeField]
        Image iconInfoType;
        [SerializeField]
        TextMeshProUGUI textInfoDifficulty;
        [SerializeField]
        TextMeshProUGUI textInfoCharacterNumber;
        [SerializeField]
        TextMeshProUGUI textInfoMoney;
        [SerializeField]
        TextMeshProUGUI textInfoWeek;
        [SerializeField]
        TextMeshProUGUI textInfoLine;
        [SerializeField]
        TextMeshProUGUI textInfoMixage;
        [SerializeField]
        TextMeshProUGUI textInfoTotalLine;
        [SerializeField]
        TextMeshProUGUI textInfoTotalFan;
        [SerializeField]
        TextMeshProUGUI textInfoDescription;
        [SerializeField]
        Animator animatorInfoCharacter;
        [SerializeField]
        PanelContractCharacter[] infoRoles;

        [Header("Feedback")]
        [SerializeField]
        Animator animatorPanelContract;

        [Header("MenuManagers")]
        [SerializeField]
        Animator animatorMenu;
        [SerializeField]
        MenuContratManager menuContractManager;

        // ===========================
        // Menu Input (Sans doute mettre dans une classe parent mais flemme)
        [Header("Menu Input")]
        [SerializeField]
        private InputController inputController;
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
            SelectButton();
            if (listContractAvailable.Count == 0) {
                animatorSelection.gameObject.SetActive(false);
                DrawInfoZero();
            }
            else
            {
                animatorSelection.gameObject.SetActive(true);
            }
            inputController.gameObject.SetActive(true);
        }

        private void Start()
        {
            indexLimit = scrollSize;
        }

        public void SetContractAvailable(List<Contract> list)
        {
            listContractAvailable = list;
            CreateListButton();
        }

        public void DestroyButtonList()
        {
            for (int i = 0; i < buttonsContracts.Count; i++)
            {
                Destroy(buttonsContracts[i].gameObject);
            }
            buttonsContracts.Clear();
        }

        private void CreateListButton()
        {
            for(int i = 0; i < listContractAvailable.Count; i++)
            {
                buttonsContracts.Add(Instantiate(buttonPrefab, buttonListTransform));
                buttonsContracts[i].gameObject.SetActive(true);
                buttonsContracts[i].SetButtonIndex(i);
                buttonsContracts[i].DrawButton(listContractAvailable[i].Name, typeContractData.GetSprite((int)listContractAvailable[i].ContractType));
            }
        }


        private void RedrawListButton()
        {

        }

        public void SwitchToMenuContractManager()
        {
            menuContractManager.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            animatorMenu.SetBool("Appear", false);
        }


        private void SelectButton()
        {
            if(indexSelected >= buttonsContracts.Count)
            {
                return;
            }
            animatorSelection.transform.position = buttonsContracts[indexSelected].transform.position;
            textMeshSelection.text = listContractAvailable[indexSelected].Name;
            iconSelection.sprite = buttonsContracts[indexSelected].GetIconContract();

            animatorSelection.SetTrigger("Active");
            animatorPanelContract.SetTrigger("Feedback");

            DrawInfo();
        }


        private void DrawInfoZero()
        {
            textInfoCharacterNumber.text = " ";
            textInfoTitle.text = " ";
            textInfoWeek.text = " ";
            textInfoMoney.text = " ";
            textInfoLine.text = " ";
            textInfoMixage.text = " ";
            textInfoDescription.text = " ";
            textInfoTotalLine.text = " ";
            textInfoTotalFan.text = " ";
            for (int i = 0; i < infoRoles.Length; i++)
                infoRoles[i].gameObject.SetActive(false);

        }



        private void DrawInfo()
        {
            textInfoDifficulty.text = "Difficulty " + listContractAvailable[indexSelected].Level.ToString();
            textInfoCharacterNumber.text = listContractAvailable[indexSelected].Characters.Count.ToString();
            textInfoTitle.text = listContractAvailable[indexSelected].Name;
            textInfoWeek.text = listContractAvailable[indexSelected].WeekRemaining.ToString();
            textInfoMoney.text = listContractAvailable[indexSelected].Money.ToString();

            textInfoLine.text = listContractAvailable[indexSelected].TotalLine.ToString();
            textInfoLine.gameObject.SetActive(listContractAvailable[indexSelected].TotalLine != 0);
            textInfoMixage.text = listContractAvailable[indexSelected].TotalMixing.ToString();
            textInfoMixage.gameObject.SetActive(listContractAvailable[indexSelected].TotalMixing != 0);

            textInfoDescription.text = listContractAvailable[indexSelected].Description;

            textInfoType.text = typeContractData.GetName((int)listContractAvailable[indexSelected].ContractType);
            iconInfoType.sprite = typeContractData.GetSprite((int)listContractAvailable[indexSelected].ContractType);





            if (infoRoles.Length < listContractAvailable[indexSelected].Characters.Count)
            {
                return;
            }

            int sumLine = 0;
            int sumFan = 0;
            for(int i = 0; i < infoRoles.Length; i++)
            {
                if (i < listContractAvailable[indexSelected].Characters.Count)
                {
                    infoRoles[i].gameObject.SetActive(true);
                    infoRoles[i].DrawPanel(listContractAvailable[indexSelected].Characters[i]);
                    sumLine += listContractAvailable[indexSelected].Characters[i].Line;
                    sumFan += listContractAvailable[indexSelected].Characters[i].Fan;
                }
                else
                {
                    infoRoles[i].gameObject.SetActive(false);
                }
            }

            textInfoTotalLine.text = sumLine.ToString();
            textInfoTotalFan.text = sumFan.ToString();

        }

        public void Validate(int index)
        {
            indexSelected = index;
            Validate();
        }

        public void Validate()
        {
            if(listContractAvailable.Count == 0)
            {
                return;
            }
            if (menuContractManager.AddContractToList(listContractAvailable[indexSelected]) == true)
            {
                listContractAvailable.RemoveAt(indexSelected);
                Destroy(buttonsContracts[indexSelected].gameObject);
                buttonsContracts.RemoveAt(indexSelected);
                indexSelected -= 1;
                if(indexSelected < 0)
                {
                    indexSelected = 0;
                }
                animatorSelection.SetTrigger("Validate");
                StartCoroutine(WaitValidate(0.2f));
            }
        }

        private IEnumerator WaitValidate(float time)
        {
            inputController.gameObject.SetActive(false);
            yield return new WaitForSeconds(time);
            SwitchToMenuContractManager();
            menuContractManager.CheckPhoneEvent();
        }

        public void ShowCharacterInfo()
        {
            animatorInfoCharacter.SetBool("Appear", !animatorInfoCharacter.GetBool("Appear"));
        }












        public void SelectContract(int index)
        {
            indexSelected = index;
            SelectButton();
        }



        public void SelectContractUp()
        {
            if (listContractAvailable.Count == 0)
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

            indexSelected -= 1;
            if (indexSelected <= -1)
            {
                //StopRepeat();
                indexSelected = buttonsContracts.Count - 1;
            }
            MoveScrollRect();
            SelectButton();
        }

        public void SelectContractDown()
        {
            if (listContractAvailable.Count == 0)
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

            indexSelected += 1;
            if (indexSelected >= buttonsContracts.Count)
            {
                //StopRepeat();
                indexSelected = 0;
            }
            MoveScrollRect();
            SelectButton();

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
            if (indexSelected > indexLimit)
            {
                indexLimit = indexSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                if (coroutineScroll != null)
                {
                    StopCoroutine(coroutineScroll);
                }
                StartCoroutine(coroutineScroll);
            }
            else if (indexSelected < indexLimit - scrollSize+1)
            {
                indexLimit = indexSelected + scrollSize-1;
                coroutineScroll = MoveScrollRectCoroutine();
                if (coroutineScroll != null)
                {
                    StopCoroutine(coroutineScroll);
                }
                StartCoroutine(coroutineScroll);
            }

        }

        private IEnumerator MoveScrollRectCoroutine()
        {
            float time = 10f;
            int ratio = indexLimit - scrollSize;
            Vector2 speed = new Vector2(0,(buttonListTransform.anchoredPosition.y - ratio * buttonSize) / time);
            while (time != 0)
            {
                animatorSelection.gameObject.transform.position = buttonsContracts[indexSelected].transform.position;
                buttonListTransform.anchoredPosition -= speed;
                time -= 1;
                yield return null;
            }
            animatorSelection.gameObject.transform.position = buttonsContracts[indexSelected].transform.position;
            //animatorSelection.gameObject.transform.position = new Vector3(buttonsContracts[indexSelected].transform.position.x, buttonsContracts[indexSelected].transform.position.y - buttonListTransform.anchoredPosition.y, buttonsContracts[indexSelected].transform.position.z);

        }




        #endregion

    } // MenuContractAvailable class
	
}// #PROJECTNAME# namespace
