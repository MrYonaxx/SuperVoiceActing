/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the InputController class
    /// </summary>
    public class InputController : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        float joystickDeadZone = 0.25f;

        [Header("Buttons")]
        [SerializeField]
        UnityEvent eventBoutonY;
        [SerializeField]
        UnityEvent eventBoutonB;
        [SerializeField]
        UnityEvent eventBoutonA;

        [Header("Joystick Left")]
        [SerializeField]
        UnityEvent eventLeft;
        [SerializeField]
        UnityEvent eventUp;
        [SerializeField]
        UnityEvent eventRight;
        [SerializeField]
        UnityEvent eventDown;

        //[Header("Joystick Right")]
        /*[SerializeField]
        UnityEvent eventLeft;
        [SerializeField]
        UnityEvent eventUp;
        [SerializeField]
        UnityEvent eventRight;
        [SerializeField]
        UnityEvent eventDown;*/


        bool inputEnter = false;

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
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            CheckInput();
            CheckDirection();
        }

        private void CheckInput()
        {
            if(Input.GetButtonDown("ControllerY"))
            {
                eventBoutonY.Invoke();
            }
            if (Input.GetButtonDown("ControllerB"))
            {
                eventBoutonB.Invoke();
            }
            if (Input.GetButtonDown("ControllerA"))
            {
                eventBoutonA.Invoke();
            }
        }

        private void CheckDirection()
        {
            if(inputEnter == true)
            {
                if (Mathf.Abs(Input.GetAxis("ControllerHorizontal")) < joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerVertical")) < joystickDeadZone)
                    inputEnter = false;
                else
                    return;

            }

            if(Input.GetAxis("ControllerHorizontal") > joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerVertical")) < joystickDeadZone)
            {
                eventRight.Invoke();
                inputEnter = true;
            }
            else if (Input.GetAxis("ControllerHorizontal") < -joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerVertical")) < joystickDeadZone)
            {
                eventLeft.Invoke();
                inputEnter = true;
            }
            else if (Input.GetAxis("ControllerVertical") < -joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerHorizontal")) < joystickDeadZone)
            {
                eventUp.Invoke();
                inputEnter = true;
            }
            else if (Input.GetAxis("ControllerVertical") > joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerHorizontal")) < joystickDeadZone)
            {
                eventDown.Invoke();
                inputEnter = true;
            }
        }

        #endregion

    } // InputController class

} // #PROJECTNAME# namespace