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
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace VoiceActing
{
    /// <summary>
    /// Definition of the CameraBureau class
    /// </summary>
    public class ButtonContratAccepted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        RectTransform gaugeMixingPreview;
        [SerializeField]
        GameObject gameObjectLine;
        [SerializeField]
        GameObject gameObjectMixing;

        [SerializeField]
        GameObject gameObjectClear;

        bool isButtonAddContract = false;

        int buttonIndex;

        [SerializeField]
        UnityEventInt unityEventOnClick;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public Sprite GetIconContract()
        {
            return contractIcon.sprite;
        }

        public void SetButtonIndex(int index)
        {
            buttonIndex = index;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void DrawContract(Contract contract, Sprite iconContract = null)
        {
            isButtonAddContract = false;
            panelContract.SetActive(true);
            panelAddContract.SetActive(false);

            contractTitle.text = contract.Name;
            contractLine.text = contract.CurrentLine + " / " + contract.TotalLine;
            contractMixage.text = contract.CurrentMixing + " / " + contract.TotalMixing;
            contractWeekRemaining.text = contract.WeekRemaining.ToString();
            if (contract.WeekRemaining <= 2)
                contractWeekRemaining.color = Color.yellow;
            contractWeekRemainingShadow.text = contract.WeekRemaining.ToString();
            contractIcon.sprite = iconContract;

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
                gaugeMixing.transform.localScale = new Vector2((contract.CurrentMixing / (float)contract.TotalMixing), gaugeMixing.transform.localScale.y);
                // Draw preview
                /*if(contract.SoundEngineerID != null)
                {
                    float size = (contract.CurrentMixing + contract.SoundEngineer.GetMixingPower(contract) / (float)contract.TotalMixing);
                    if (size >= 1)
                        size = 1;
                    gaugeMixingPreview.transform.localScale = new Vector2(size, gaugeMixingPreview.transform.localScale.y);
                }*/
            }
            gameObjectClear.SetActive(false);
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
            gameObjectClear.SetActive(false);
        }




        public IEnumerator CoroutineProgress(int newCurrentMixing, int totalMixing, int time = 90)
        {
            if (totalMixing == 0)
            {
                if (gaugeLine.transform.localScale.x == 1)
                {
                    gameObjectClear.SetActive(true);
                }
                yield break;
            }
            Vector3 speed = new Vector3((((float)newCurrentMixing / totalMixing) - gaugeMixing.transform.localScale.x) / time, 0, 0);
            while (time != 0)
            {
                gaugeMixing.transform.localScale += speed;
                time -= 1;
                yield return null;
            }
            contractMixage.text = newCurrentMixing + " / " + totalMixing;

            if(gaugeLine.transform.localScale.x >= 1 && gaugeMixing.transform.localScale.x >= 1)
            {
                gameObjectClear.SetActive(true);
            }
        }







        public void OnPointerEnter(PointerEventData data)
        {
            SelectContract();
        }
        public void OnPointerExit(PointerEventData data)
        {
            UnSelectContract();
        }
        public void OnPointerClick(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Left)
                unityEventOnClick.Invoke(buttonIndex);
        }



        #endregion
    }
}
