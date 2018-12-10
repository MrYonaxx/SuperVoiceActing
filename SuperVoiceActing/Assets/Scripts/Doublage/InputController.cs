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
        UnityEvent eventBoutonY;
        [SerializeField]
        UnityEvent eventBoutonB;

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
        }

        private void CheckDirection()
        {
            /*if(Input.GetAxis())
            {

            }*/
        }

        #endregion

    } // InputController class

} // #PROJECTNAME# namespace