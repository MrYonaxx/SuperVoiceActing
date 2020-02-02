/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the TimerDoublage class
    /// </summary>
    public class TurnManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        TextMeshProUGUI textTurnMainDigit = null;
        [SerializeField]
        TextMeshProUGUI textTurnSecondDigit = null;
        [SerializeField]
        Animator animatorText = null;


        [SerializeField]
        Color colorDanger;


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

        public float AdvanceTimer(float turnCount, float turnMinusValue = 1)
        {
            if (turnCount % 1 == 0)
            {
                turnCount -= 1;
            }
            else
            {
                turnCount -= turnMinusValue;
            }
            animatorText.Play("ANIM_TurnDigit");
            DrawTurn(turnCount);
            return turnCount;
        }


        public void DrawTurn(float turnCount)
        {
            if (turnCount <= 3)
            {
                textTurnMainDigit.color = colorDanger;
                textTurnSecondDigit.color = colorDanger;
            }

            if (turnCount < 10)
            {
                textTurnMainDigit.text = "0" + turnCount.ToString();
            }
            textTurnMainDigit.text = turnCount.ToString();

            float decimalPart = Mathf.FloorToInt(turnCount);
            decimalPart = turnCount - decimalPart;
            if (decimalPart == 0)
                textTurnSecondDigit.text = "00";
            else
                textTurnSecondDigit.text = (decimalPart * 10).ToString();

        }
 

        #endregion

    } // TimerDoublage class

} // #PROJECTNAME# namespace