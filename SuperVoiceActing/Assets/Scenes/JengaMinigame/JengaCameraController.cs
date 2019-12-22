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
    public class JengaCameraController: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        float speed = 10f;
        [SerializeField]
        float lookSpeed = 10f;
        [SerializeField]
        float joystickThreshold = 0.4f;

        bool isActive = true;
        JengaPiece pieceSelected = null;

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
        public void Update()
        {
            if (isActive == true)
            {
                Move();
                MoveCamera();
            }
            if(Input.GetButtonDown("ControllerA"))
            {
                SelectPiece();
            }
            if (Input.GetButtonDown("ControllerB"))
            {
                CancelPiece();
            }
        }


        public void Move()
        {
            float speedX = 0;
            float speedZ = 0;
            if(Mathf.Abs(Input.GetAxis("ControllerLeftHorizontal")) > joystickThreshold)
            {
                speedX = Input.GetAxis("ControllerLeftHorizontal") * speed;
            }
            if (Mathf.Abs(Input.GetAxis("ControllerLeftVertical")) > joystickThreshold)
            {
                speedZ = Input.GetAxis("ControllerLeftVertical") * speed;
            }
            Vector3 move = transform.right * speedX + transform.forward * -speedZ;
            this.gameObject.transform.position += move * Time.deltaTime;
        }

        public void MoveCamera()
        {

            float speedX = 0;
            float speedY = 0;
            if (Mathf.Abs(Input.GetAxis("ControllerRightVertical")) > joystickThreshold)
            {
                speedX = Input.GetAxis("ControllerRightVertical") * lookSpeed;
            }

            if (Mathf.Abs(Input.GetAxis("ControllerRightHorizontal")) > joystickThreshold)
            {
                speedY = Input.GetAxis("ControllerRightHorizontal") * lookSpeed;
            }
            this.transform.Rotate(new Vector3(speedX, speedY, 0) * Time.deltaTime);
            this.gameObject.transform.eulerAngles = new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, 0);

        }

        public void SelectPiece()
        {
            int layerMask = 1 << 10;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            //layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                Debug.Log(hit.collider.gameObject.name);
                JengaPiece piece = hit.transform.GetComponent<JengaPiece>();
                if (piece != null)
                {
                    pieceSelected = piece;
                    pieceSelected.Select(true);
                    isActive = false;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }

        public void CancelPiece()
        {
            if(pieceSelected != null)
            {
                pieceSelected.Select(false);
                isActive = true;
                pieceSelected = null;
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace