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
    public class SessionRecapManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Animator animatorRecap;
        [SerializeField]
        InputController inputController;

        [Space]
        [SerializeField]
        TextMeshProUGUI textContractName;
        [SerializeField]
        TextMeshProUGUI textContractWeek;
        [SerializeField]
        TextMeshProUGUI textContractHype;
        [SerializeField]
        TextMeshProUGUI textContractMoney;
        [SerializeField]
        TextMeshProUGUI textContractDescription;

        [Space]
        [SerializeField]
        SessionRecapLineData sessionPrefab;
        [SerializeField]
        RectTransform sessionScrollList;

        List<SessionRecapLineData> listSessionLines = new List<SessionRecapLineData>();

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

        public void MenuDisappear()
        {
            animatorRecap.SetTrigger("Disappear");
            inputController.gameObject.SetActive(false);
        }

        public void DrawContract(Contract contract)
        {
            inputController.gameObject.SetActive(true);
            animatorRecap.gameObject.SetActive(true);
            animatorRecap.SetTrigger("Appear");
            textContractName.text = contract.Name;
            textContractWeek.text = contract.WeekRemaining.ToString();
            textContractHype.text = "0";// contract.fan.ToString();
            textContractMoney.text = contract.Money.ToString();
            textContractDescription.text = contract.Description;
            DrawSessionLines(contract);
            MoveScrollList(9999);

        }

        public void DrawSessionLines(Contract contract)
        {
            for (int i = listSessionLines.Count; i < contract.CurrentLine; i++)
            {
                listSessionLines.Add(Instantiate(sessionPrefab, sessionScrollList));
                listSessionLines[i].DrawRecapLine(contract.Characters[contract.TextData[i].Interlocuteur].Name,
                                                  contract.Characters[contract.TextData[i].Interlocuteur].RoleSprite,
                                                  contract.TextData[i].Text,
                                                  contract.EmotionsUsed[i].emotions);
            }
        }


        public void MoveScrollList(int amount)
        {
            sessionScrollList.anchoredPosition += new Vector2(0,amount);
        }
        
        #endregion
		
	} // SessionRecapManager class
	
}// #PROJECTNAME# namespace
