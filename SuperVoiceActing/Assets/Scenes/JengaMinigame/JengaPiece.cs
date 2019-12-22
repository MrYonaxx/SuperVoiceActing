/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class JengaPiece: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        bool isSelected = false;


        [SerializeField]
        Camera camera = null;
        [SerializeField]
        Rigidbody rigidbody = null;

        [SerializeField]
        float speed = 2f;
        [SerializeField]
        float joystickThreshold = 0.4f;
        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void Select(bool b)
        {
            isSelected = b;
        }

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public void Update()
        {
            if(isSelected == true)
            {
                MovePiece();
            }
        }

        public void MovePiece()
        {
            float speedX = 0;
            float speedZ = 0;
            if (Mathf.Abs(Input.GetAxis("ControllerLeftHorizontal")) > joystickThreshold)
            {
                speedX = Input.GetAxis("ControllerLeftHorizontal") * speed;
            }
            if (Mathf.Abs(Input.GetAxis("ControllerLeftVertical")) > joystickThreshold)
            {
                speedZ = Input.GetAxis("ControllerLeftVertical") * speed;
            }
            Vector3 force = camera.transform.right * speedX + camera.transform.forward * -speedZ;
            rigidbody.velocity = (force);

            //this.gameObject.transform.position += move * Time.deltaTime;
        }

        #endregion

    } 

} // #PROJECTNAME# namespace