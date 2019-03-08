/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix;
using UnityEngine.SceneManagement;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the CameraBureau class
    /// </summary>
    public class CameraBureau : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        float speed = 1;

        [SerializeField]
        float joystickDeadZone = 0.9f;

        [SerializeField]
        SmoothShake smoothShakeCamera;

        [SerializeField]
        Transform[] transforms;
        [SerializeField]
        GameObject[] screenManager;

        [SerializeField]
        Animator animatorHUD;

        int position = 2;

        float speedX;
        float speedY;

        bool zoomed = false;
        bool freeCameraOn = false;
        bool inputRightStickEnter = false;
        bool inputLeftTrigger = false;
        private IEnumerator coroutine = null;
        private IEnumerator orthographicCoroutine = null;

        Camera camera;

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
        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            CheckRightDirection();
        }

        //!\
        // C'est un peu l'anarchie, à clarifier
        //!\
        private void CheckRightDirection()
        {
            // Zoom
            if (Input.GetAxis("ControllerTriggers") > 0.8f && inputLeftTrigger == false)
            {
                if (zoomed == false)
                {
                    animatorHUD.SetTrigger("Hide");
                    ModifyAnglePosition(0);
                    MoveToCamera();
                    ChangeOrthographicSize(-33);
                    smoothShakeCamera.enabled = false;
                    smoothShakeCamera.transform.position = Vector3.zero;
                    zoomed = true;
                }
                else
                {
                    animatorHUD.SetTrigger("Show");
                    ChangeOrthographicSize(0);
                    smoothShakeCamera.enabled = true;
                    zoomed = false;
                }

                inputLeftTrigger = true;
            }
            else if (Input.GetAxis("ControllerTriggers") < 0.8f)
            {
                inputLeftTrigger = false;
            }


            // Free Camera
            if (Input.GetAxis("ControllerTriggers") < -0.8f)
            {
                if (freeCameraOn == false && zoomed == false)
                {
                    animatorHUD.SetTrigger("Hide");
                }
                FreeCamera();
                return;
            }
            else if (freeCameraOn == true)
            {
                if(zoomed == false)
                    animatorHUD.SetTrigger("Show");
                freeCameraOn = false;
                ModifyAnglePosition(4);
                MoveToCamera();
                inputRightStickEnter = true;
                return;
            }


            // Change Computer Screen
            if (inputRightStickEnter == true)
            {
                if (Mathf.Abs(Input.GetAxis("ControllerRightHorizontal")) < joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerRightVertical")) < joystickDeadZone)
                    inputRightStickEnter = false;
                else
                    return;

            }

            if (Input.GetAxis("ControllerRightHorizontal") > joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerRightVertical")) < joystickDeadZone)
            {
                position += 1;
                if (position == transforms.Length)
                    position -= 1;

                if (zoomed == false)
                    ModifyAnglePosition(4);
                else
                    ModifyAnglePosition(0);
                MoveToCamera();

                inputRightStickEnter = true;
            }
            else if (Input.GetAxis("ControllerRightHorizontal") < -joystickDeadZone && Mathf.Abs(Input.GetAxis("ControllerRightVertical")) < joystickDeadZone)
            {
                position -= 1;
                if (position == -1)
                    position += 1;

                if (zoomed == false)
                    ModifyAnglePosition(4);
                else
                    ModifyAnglePosition(0);
                MoveToCamera();

                inputRightStickEnter = true;
            }
        }





        private void FreeCamera()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = null;

            freeCameraOn = true;

            if (Mathf.Abs(Input.GetAxis("ControllerRightHorizontal")) > 0.5f)
            {
                speedY = Input.GetAxis("ControllerRightHorizontal");
            }
            else
            {
                speedY = 0;
            }

            if (Mathf.Abs(Input.GetAxis("ControllerRightVertical")) > 0.5f)
            {
                speedX = Input.GetAxis("ControllerRightVertical");
            }
            else
            {
                speedX = 0;
            }
            this.transform.eulerAngles += new Vector3(speedX * speed, speedY * speed, 0);
            float angleX = this.transform.eulerAngles.x;
            angleX = (angleX > 180) ? angleX - 360 : angleX;
            float angleY = this.transform.eulerAngles.y;
            angleY = (angleY > 180) ? angleY - 360 : angleY;
            this.transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, -30f, 10f), Mathf.Clamp(angleY, -45f, 45f), this.transform.eulerAngles.z);
        }


        private void ModifyAnglePosition(float range)
        {
            transforms[position].eulerAngles = new Vector3(transforms[position].eulerAngles.x, transforms[position].eulerAngles.y, Random.Range(-range, range));
            this.transform.SetParent(transforms[position]);
        }





        public void MoveToCamera(int newPos = -1)
        {
            if(newPos != -1)
            {
                position = newPos;
                ModifyAnglePosition(0);
            }

            ActivateScreenManager(position);

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = MoveToOrigin(1.2f);
            StartCoroutine(coroutine);
        }

        private IEnumerator MoveToOrigin(float speedCoroutine)
        {
            float angleX = 0;
            float angleY = 0;
            float angleZ = 0;

            float ratioX = 0;
            float ratioY = 0;
            float ratioZ = 0;

            while (this.transform.localPosition != Vector3.zero || this.transform.localEulerAngles != Vector3.zero)
            {
                angleX = this.transform.localEulerAngles.x;
                if (angleX > 1 && angleX < 359)
                {
                    ratioX = 0;
                    if (angleX > 180)
                    {
                        angleX = 360 - angleX;
                        ratioX = angleX * speedCoroutine;
                        angleX = ratioX - angleX;
                    }
                    else
                    {
                        ratioX = angleX - (angleX / speedCoroutine);
                        angleX = -ratioX;
                    }
                }
                else
                {
                    angleX = 0;
                }

                angleY = this.transform.localEulerAngles.y;
                if (angleY > 1 && angleY < 359)
                {
                    ratioY = 0;
                    if (angleY > 180)
                    {
                        angleY = 360 - angleY;
                        ratioY = angleY * speedCoroutine;
                        angleY = ratioY - angleY;
                    }
                    else
                    {
                        ratioY = angleY - (angleY / speedCoroutine);
                        angleY = -ratioY;
                    }
                }
                else
                {
                    angleY = 0;
                }


                angleZ = this.transform.localEulerAngles.z;
                if (angleZ > 1 && angleZ < 359)
                {
                    ratioZ = 0;
                    if (angleZ > 180)
                    {
                        angleZ = 360 - angleZ;
                        ratioZ = angleZ * speedCoroutine;
                        angleZ = ratioZ - angleZ;
                    }
                    else
                    {
                        ratioZ = angleZ - (angleZ / speedCoroutine);
                        angleZ = -ratioZ;
                    }
                }
                else
                {
                    angleZ = 0;
                }
                this.transform.localEulerAngles += new Vector3(angleX, angleY, angleZ);
                this.transform.localPosition /= speedCoroutine;
                if(Mathf.Abs(this.transform.localPosition.x) < 0.01f &&
                   Mathf.Abs(this.transform.localPosition.y) < 0.01f &&
                   Mathf.Abs(this.transform.localPosition.z) < 0.01f)
                {
                    this.transform.localPosition = Vector3.zero;
                }


                yield return null;
            }
            coroutine = null;
        }





        public void ChangeOrthographicSize(float addValue)
        {
            if (orthographicCoroutine != null)
            {
                StopCoroutine(orthographicCoroutine);
            }
            orthographicCoroutine = OrthographicSizeTransition(addValue);
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
            camera.fieldOfView = 70 + addValue;
            orthographicCoroutine = null;
        }



        private void ActivateScreenManager(int index)
        {
            // active le screen Manager a l'index de la camera
            for(int i = 0; i < screenManager.Length; i++)
            {
                if(screenManager[i] != null)
                    screenManager[i].SetActive(false);

                if(i == index && screenManager[i] != null)
                {
                    screenManager[index].SetActive(true);
                }
            }

        }



        public void TransitionDoublage()
        {
            if (zoomed == false)
            {
                ChangeOrthographicSize(-30);
            }
            animatorHUD.SetTrigger("Hide");
        }

        public void GoToDoublage()
        {
            SceneManager.LoadScene("Doublage");
        }

        #endregion

    } // CameraBureau class

} // #PROJECTNAME# namespace