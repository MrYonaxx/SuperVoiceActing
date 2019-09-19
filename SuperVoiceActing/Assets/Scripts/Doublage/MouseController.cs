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
    /// Definition of the NewBehaviourScript class
    /// </summary>
    public class MouseController : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Buttons")]
        [SerializeField]
        UnityEvent eventLeftClick;
        [SerializeField]
        UnityEvent eventRightClick;
        [SerializeField]
        UnityEvent eventWheelUp;
        [SerializeField]
        UnityEvent eventWheelDown;

        [SerializeField]
        UnityEvent eventDoubleClick;



        float scrollValue = 0;
        int time = 0;

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
        protected void Start()
        {
            scrollValue = Input.GetAxis("Mouse ScrollWheel");
            if(scrollValue == 0)
            {
                scrollValue = 1;
            }
        }

        protected void Update()
        {
            CheckInput();
            if (time > 0)
            {
                time -= 1;
            }
        }


        private void CheckInput()
        {
            if (Input.GetMouseButtonDown(0) && time > 0)
            {
                eventDoubleClick.Invoke();
                time = 0;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                eventLeftClick.Invoke();
                time = 15;
            }
            if (Input.GetMouseButtonDown(1))
            {
                eventRightClick.Invoke();
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                scrollValue = Input.GetAxis("Mouse ScrollWheel");
                eventWheelUp.Invoke();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                scrollValue = Input.GetAxis("Mouse ScrollWheel");
                eventWheelDown.Invoke();
            }
        }


        #endregion

    } // NewBehaviourScript class

} // #PROJECTNAME# namespace