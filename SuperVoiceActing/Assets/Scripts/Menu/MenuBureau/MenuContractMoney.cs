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
    public struct SalaryData
    {
        public string dataName;
        public int salary;

        public SalaryData(string name, int money)
        {
            dataName = name;
            salary = money;
        }

    }

    public class MenuContractMoney : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("MenuMoney")]
        [SerializeField]
        Animator moneyPanel;
        [SerializeField]
        TextMeshProUGUI textMoney;

        [SerializeField]
        TextMeshProUGUI textMoneyGain;
        [SerializeField]
        TextMeshProUGUI textMoneyGainLabel;

        [SerializeField]
        Color colorMoneyGain;
        [SerializeField]
        Color colorMoneyLoss;
        [SerializeField]
        Color colorMoneyEstimate;

        [Header("Clone")]
        [SerializeField]
        MenuContractMoney[] menuContractMoneys;

        List<SalaryData> salaryDatas = new List<SalaryData>();
        int currentMoney = 0;
        int moneyGain = 0;
        bool estimateMode = false;


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

        public void AddSalaryFast(int money)
        {
            moneyGain = money;
        }


        public void AddSalaryDatas(string label, int money)
        {
            if(money == 0)
            {
                return;
            }
            salaryDatas.Add(new SalaryData(label, money));
        }

        public void DrawMoneyOnce(int money)
        {
            currentMoney = money;
            textMoney.text = currentMoney.ToString();
        }

        public void DrawMoney(int money)
        {
            currentMoney = money;
            textMoney.text = currentMoney.ToString();
            for(int i = 0; i < menuContractMoneys.Length; i++)
            {
                menuContractMoneys[i].DrawMoneyOnce(currentMoney);
            }
        }

        public void UpdateMoney()
        {
            currentMoney += moneyGain;
            DrawMoney(currentMoney);
            DrawMoneyGain();
        }

        public void DrawMoneyGain()
        {
            if (salaryDatas.Count == 0)
                return;

            moneyPanel.SetTrigger("Feedback");
            textMoneyGainLabel.text = salaryDatas[0].dataName;
            textMoneyGain.text = salaryDatas[0].salary.ToString();
            moneyGain = salaryDatas[0].salary;
            if (moneyGain < 0)
                textMoneyGain.color = colorMoneyLoss;
            else
                textMoneyGain.color = colorMoneyGain;
            salaryDatas.RemoveAt(0);
        }











        public void ActivateEstimate()
        {
            if (estimateMode == false)
            {
                moneyPanel.SetTrigger("Estimate");
                textMoneyGainLabel.text = "Estimation";
                estimateMode = true;
            }
        }

        public void DrawEstimate(int cost)
        {
            if (cost < 0)
            {
                textMoneyGain.color = colorMoneyLoss;
                textMoneyGainLabel.text = "Prix";
            }
            else
            {
                textMoneyGain.color = colorMoneyEstimate;
                textMoneyGainLabel.text = "Estimation";
            }

            textMoneyGain.text = cost.ToString();
        }

        public void DesactivateEstimate()
        {
            if (estimateMode == true)
            {
                moneyPanel.SetTrigger("EstimateHide");
                estimateMode = false;
            }
        }

        public void AuditionFeedback()
        {
            moneyPanel.SetTrigger("Audition");
            estimateMode = false;
        }

        #endregion

    } // MenuContractMoney class
	
}// #PROJECTNAME# namespace
