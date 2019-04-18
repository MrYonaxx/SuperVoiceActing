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
    public class TimerDoublage : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        TextMeshProUGUI textMeshPro = null;
        [SerializeField]
        TextMeshProUGUI textMeshTurnCount = null;
        [SerializeField]
        Animator animTextMesh = null;
        [SerializeField]
        bool active = false;


        [SerializeField]
        Color colorDanger;


        int turnCount = 0;
        int seconds = 0;
        int minute = 0;

        string turnDraw = "";
        string secondDraw = "";
        string minuteDraw = "";

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




        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            if (active == false)
                return;

            seconds -= Random.Range(10, 50);
            if (seconds < 0)
            {
                seconds += 100;
            }
            minute -= 1;
            if (minute < 0)
                minute = 99;

            if (minute / 10 < 1)
                minuteDraw = "0" + minute.ToString();
            else
                minuteDraw = minute.ToString();

            if (seconds / 10 < 1)
                secondDraw = "0" + seconds.ToString();
            else
                secondDraw = seconds.ToString();

            textMeshPro.text = "TURN       :" + minuteDraw + ":" + secondDraw;
        }
        
        public void SetTurn(int newTurn)
        {
            turnCount = newTurn;
            if (turnCount / 10 < 1)
            {
                turnDraw = "0" + turnCount.ToString();
                if(turnCount <= 3)
                {
                    textMeshTurnCount.color = colorDanger;
                    textMeshPro.color = colorDanger;
                }
                if (turnCount < 0)
                {
                    turnDraw = "00";
                }
            }
            else
            {
                turnDraw = turnCount.ToString();

            }
            textMeshTurnCount.text = turnDraw;
            animTextMesh.Play("ANIM_TurnDigit");
        }

        public void ActiveTimer(bool b)
        {
            active = b;
        }

        #endregion

    } // TimerDoublage class

} // #PROJECTNAME# namespace