/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the CameraController class
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Camera Center")]
        [SerializeField]
        Camera camera;

        [Header("Camera Center")]
        [SerializeField]
        Transform initialPosition;
        [SerializeField]
        Transform notQuitePosition;
        [SerializeField]
        float offsetZ = 0;
        [SerializeField]
        float offsetX = 0;

        [Header("Text")]
        [SerializeField]
        Transform text;

        bool moving = false;
        bool rotating = false;

        float offset = 0;

        private IEnumerator movementCoroutine;
        private IEnumerator rotatingCoroutine;

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

        ////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected void Awake()
        {
            if (QualitySettings.vSyncCount > 0)
                Application.targetFrameRate = 60;
            else
                Application.targetFrameRate = -1;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        protected virtual void Start()
        {
            MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, 180);
            //RotateCamera(0f, 45f, 0f, 600f);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            if(moving == false && rotating == false)
            {
                InitializeCameraEffect();
            }
        }



        private void InitializeCameraEffect()
        {
            SetText(-6,2.7f,0);
            SetCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z);
            SetCameraRotation(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z);
            switch (Random.Range(1,10))
            {
                case 1:
                    MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z + 0.5f, 900);
                    RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y + 15f, initialPosition.eulerAngles.z, 900);
                    break;
                case 2:
                    MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z - 0.5f, 900);
                    RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y - 15f, initialPosition.eulerAngles.z, 900);
                    break;
                case 3:
                    MoveCamera(initialPosition.position.x + 0.2f, initialPosition.position.y, initialPosition.position.z, 900);
                    RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z - 10f, 900);
                    break;
                case 4:
                    MoveCamera(initialPosition.position.x + 0.2f, initialPosition.position.y, initialPosition.position.z, 900);
                    RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + 10f, 900);
                    break;
                case 5:
                    MoveCamera(initialPosition.position.x - 0.2f, initialPosition.position.y, initialPosition.position.z, 900);
                    RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z - 10f, 900);
                    break;
                case 6:
                    MoveCamera(initialPosition.position.x - 0.2f, initialPosition.position.y, initialPosition.position.z, 900);
                    RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + 10f, 900);
                    break;
                case 7:
                    SetText(-5.2f, 3f, 0.5f);
                    SetCamera(initialPosition.position.x + 1, initialPosition.position.y + 0.5f, initialPosition.position.z + 0.6f);
                    MoveCamera(initialPosition.position.x + 1, initialPosition.position.y + 0.1f, initialPosition.position.z + 0.6f, 900);
                    //RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + 10f, 900);
                    break;
                case 8:
                    SetText(-5.2f, 3.1f, -0.25f);
                    SetCamera(initialPosition.position.x + 1, initialPosition.position.y + 0.1f, initialPosition.position.z - 0.2f);
                    MoveCamera(initialPosition.position.x + 1, initialPosition.position.y + 0.5f, initialPosition.position.z - 0.2f, 900);
                    //RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + 10f, 900);
                    break;
                case 9:
                    SetText(-5.2f, 3f, 0);
                    SetCamera(initialPosition.position.x + 1.2f, initialPosition.position.y + 0.3f, initialPosition.position.z);
                    MoveCamera(initialPosition.position.x + 0.8f, initialPosition.position.y + 0.2f, initialPosition.position.z, 900);
                    //RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + 10f, 900);
                    break;
                case 10:
                    SetCameraRotation(initialPosition.eulerAngles.x - 10, initialPosition.eulerAngles.y + 1, initialPosition.eulerAngles.z);
                    MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, 900);
                    RotateCamera(initialPosition.eulerAngles.x + 10, initialPosition.eulerAngles.y - 1, initialPosition.eulerAngles.z, 900);
                    break;
            }
        }



        [ContextMenu("camera")]
        public void ActivateOffset()
        {
            StartCoroutine(OffsetCoroutine(20));
        }

        private IEnumerator OffsetCoroutine(float time)
        {
            float speedZ = offsetZ / time;
            while (time != 0)
            {
                this.transform.position += new Vector3(0, 0, speedZ);
                offset += speedZ;
                time -= 1;
                yield return null;
            }
            offset = offsetZ;
        }

        [ContextMenu("cameraStop")]
        public void StopOffset()
        {
            StartCoroutine(StopOffsetCoroutine(20));
        }

        private IEnumerator StopOffsetCoroutine(float time)
        {
            float speedZ = offsetZ / time;
            while (time != 0)
            {
                this.transform.position -= new Vector3(0, 0, speedZ);
                offset -= speedZ;
                time -= 1;
                yield return null;
            }
            offset = 0;
        }



        private void SetText(float x, float y, float z)
        {
            text.position = new Vector3(x, y, z);
        }


        public void SetCamera(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z + offset);
        }

        public void SetCameraRotation(float x, float y, float z)
        {
            transform.eulerAngles = new Vector3(x, y, z);
        }





        public void MoveCamera(float x, float y, float z, float time)
        {
            movementCoroutine = MoveCameraCoroutine(x, y, z, time);
            StartCoroutine(movementCoroutine);
        }

        private IEnumerator MoveCameraCoroutine(float x, float y, float z, float time)
        {
            float speedX = (x - this.transform.position.x) / time;
            float speedY = (y - this.transform.position.y) / time;
            float speedZ = (z - this.transform.position.z + offset) / time;
            moving = true;
            while (time != 0)
            {
                this.transform.position += new Vector3(speedX, speedY, speedZ);
                time -= 1;
                yield return null;
            }
            moving = false;
            this.transform.position = new Vector3(x, y, z + offset);
        }



        // Note : ne pas mettre d'angle négatif
        public void RotateCamera(float x, float y, float z, float time)
        {
            rotatingCoroutine = RotateCameraCoroutine(x, y, z, time);
            StartCoroutine(rotatingCoroutine);
        }

        private IEnumerator RotateCameraCoroutine(float x, float y, float z, float time)
        {  
            float speedX = (x - this.transform.eulerAngles.x) / time;
            float speedY = (y - this.transform.eulerAngles.y) / time;
            float speedZ = (z - this.transform.eulerAngles.z) / time;
            rotating = true;
            while (time != 0)
            {
                this.transform.eulerAngles += new Vector3(speedX, speedY, speedZ);
                time -= 1;
                yield return null;
            }
            rotating = false;
            this.transform.eulerAngles = new Vector3(x, y, z);
        }




        public void ChangeOrthographicSize(float addValue)
        {
            /*if (orthographicCoroutine != null)
            {
                StopCoroutine(orthographicCoroutine);
            }
            orthographicCoroutine = OrthographicSizeTransition(addValue);*/
            StartCoroutine(OrthographicSizeTransition(addValue));
        }

        private IEnumerator OrthographicSizeTransition(float addValue)
        {
            float time = 20;
            float rate = ((70 + addValue) - camera.fieldOfView) / 20;
            while (time != 0)
            {
                camera.fieldOfView += rate;
                time -= 1;
                yield return null;
            }

            camera.orthographicSize = 70 + addValue;
            //orthographicCoroutine = null;
        }



        public void NotQuite()
        {
            SetText(-6, 2.7f, 0);
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if(rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);
            SetCamera(notQuitePosition.position.x, notQuitePosition.position.y, notQuitePosition.position.z);
            SetCameraRotation(notQuitePosition.eulerAngles.x, notQuitePosition.eulerAngles.y, notQuitePosition.eulerAngles.z);
            MoveCamera(notQuitePosition.position.x + 3, notQuitePosition.position.y, notQuitePosition.position.z, 120);
            RotateCamera(0, 90, 0, 120);
        }


        #endregion

    } // CameraController class

} // #PROJECTNAME# namespace