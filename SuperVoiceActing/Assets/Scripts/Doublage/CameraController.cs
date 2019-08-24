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
        /*[Header("Camera Center")]
        [SerializeField]*/
        Camera cameraComponent;

        [Header("Camera Center")]
        [SerializeField]
        Transform initialPosition;
        [SerializeField]
        Transform notQuitePosition;
        [SerializeField]
        Transform ingeSonPosition;
        [SerializeField]
        Transform enemyPosition;
        [SerializeField]
        Transform actorSwitchPosition;

        [Header("Text")]
        [SerializeField]
        Transform text;
        [SerializeField]
        Transform textInitialPosition;
        [SerializeField]
        Transform textAttackPosition;


        [Header("Camera Center")]
        [SerializeField]
        CameraMovementData[] cameraMovementSet1;
        [SerializeField]
        CameraMovementData[] cameraMovementSet2;

        CameraMovementData currentCamMovement;

        bool moving = true;
        bool rotating = false;
        public bool noCameraEffect = false;

        float offset = 0;

        private bool pauseCoroutine = false;
        private int cameraPlacement = 0;

        private IEnumerator movementCoroutine;
        private IEnumerator rotatingCoroutine;

        private IEnumerator cinematicCoroutine;
        private IEnumerator cinematicRotatingCoroutine;

        private IEnumerator soundEngineerCoroutine;

        float speedMovement = 0.5f;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        public void SetNoCameraEffect(bool b)
        {
            noCameraEffect = b;
        }

        public void SetCameraPause(bool b)
        {
            pauseCoroutine = b;
        }

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

        protected void Start()
        {
            cameraComponent = GetComponent<Camera>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            if (noCameraEffect == true)
            {
                return;
            }
            if (moving == false && rotating == false)
            {
                InitializeCameraEffect();
            }
        }





        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        // I N I T I A L    C A M E R A    P O S I T I O N
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        ///
        public void MoveToInitialPosition(int time = 180)
        {
            MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, time);
        }

        public void SetInitialPosition(Vector3 newPos, Vector3 newRot, int time)
        {
            StartCoroutine(RotateInitialPositionCoroutine(newRot.x, newRot.y, newRot.z, time));
            StartCoroutine(MoveInitialPositionCoroutine(newPos.x, newPos.y, newPos.z, time));
        }

        private IEnumerator MoveInitialPositionCoroutine(float x, float y, float z, float time)
        {
            float speedX = (x - initialPosition.localPosition.x) / time;
            float speedY = (y - initialPosition.localPosition.y) / time;
            float speedZ = (z - initialPosition.localPosition.z) / time;
            while (time != 0)
            {
                if (pauseCoroutine == false)
                {
                    initialPosition.localPosition += new Vector3(speedX, speedY, speedZ);
                    time -= 1;
                }
                yield return null;
            }
            initialPosition.localPosition = new Vector3(x, y, z);
        }

        private IEnumerator RotateInitialPositionCoroutine(float x, float y, float z, float time)
        {
            float angleX = initialPosition.eulerAngles.x;
            if (angleX > 180)
                angleX = -(360 - angleX);
            float speedX = (x - angleX) / time;

            float angleY = initialPosition.eulerAngles.y;
            if (angleY > 180)
                angleY = -(360 - angleY);
            float speedY = (y - angleY) / time;

            float angleZ = initialPosition.eulerAngles.z;
            if (angleZ > 180)
                angleZ = -(360 - angleZ);
            if (z > 180)
                z = -(360 - z);
            float speedZ = (z - angleZ) / time;

            while (time != 0)
            {
                if (pauseCoroutine == false)
                {
                    initialPosition.eulerAngles += new Vector3(speedX, speedY, speedZ);
                    time -= 1;
                }
                yield return null;
            }
            initialPosition.eulerAngles = new Vector3(x, y, z);
        }





        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        // B A T T L E    C A M E R A    M O V E M E N T
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        ///
        private void InitializeCameraEffect()
        {
            // Set Initial position (just to be sure its in place)
            SetText(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z);
            SetCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z);
            SetCameraRotation(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z);

            if(cameraPlacement == 0)
            {
                if (cameraMovementSet1.Length == 0)
                    return;
                currentCamMovement = cameraMovementSet1[Random.Range(0, cameraMovementSet1.Length)];
                cameraPlacement = 1;
            }
            else if (cameraPlacement == 1)
            {
                if (cameraMovementSet2.Length == 0)
                    return;
                currentCamMovement = cameraMovementSet2[Random.Range(0, cameraMovementSet2.Length)];
                /*switch (Random.Range(1, 5))
                {
                    case 1:
                        SetText(textInitialPosition.x + 0.65f, textInitialPosition.y + 0.4f, textInitialPosition.z + 0.7f);
                        //SetText(-5.2f, 3f, 0.4f);
                        SetCamera(initialPosition.position.x + 0.8f, initialPosition.position.y + 0.5f, initialPosition.position.z + 0.4f);
                        MoveCamera(initialPosition.position.x + 0.8f, initialPosition.position.y + 0.1f, initialPosition.position.z + 0.4f, 900 * speedMovement);
                        break;
                    case 2:

                        SetText(textInitialPosition.x + 0.65f, textInitialPosition.y + 0.4f, textInitialPosition.z - 0.5f);
                        //SetText(-5.2f, 3.1f, -0.2f);
                        SetCamera(initialPosition.position.x + 0.8f, initialPosition.position.y + 0.1f, initialPosition.position.z - 0.2f);
                        MoveCamera(initialPosition.position.x + 0.8f, initialPosition.position.y + 0.5f, initialPosition.position.z - 0.2f, 900 * speedMovement);
                        break;
                    case 3:
                        SetText(textInitialPosition.x + 0.3f, textInitialPosition.y + 0.1f, textInitialPosition.z);
                        //SetText(-5.2f, 3f, 0);
                        SetCamera(initialPosition.position.x + 0.8f, initialPosition.position.y + 0.2f, initialPosition.position.z);
                        MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, 900 * speedMovement);
                        break;
                    case 4:
                        SetCameraRotation(initialPosition.eulerAngles.x - 15, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z);
                        SetCamera(initialPosition.position.x, initialPosition.position.y - 0.25f, initialPosition.position.z);
                        MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, 900 * speedMovement);
                        RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z, 900 * speedMovement);
                        break;
                }*/
                cameraPlacement = 0;
            }
            CameraDataMovement(currentCamMovement);

        }

        public void CameraDataMovement(CameraMovementData info)
        {
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);
            if (cinematicCoroutine != null)
                StopCoroutine(cinematicCoroutine);
            if (cinematicRotatingCoroutine != null)
                StopCoroutine(cinematicRotatingCoroutine);

            rotating = true;
            moving = true;

            movementCoroutine = CameraMovementCoroutine(info.CamStart.x, info.CamStart.y, info.CamStart.z, (int)(info.TimeStart * speedMovement),
                                                  info.CameraEnd.x, info.CameraEnd.y, info.CameraEnd.z, (int)(info.TimeEnd * speedMovement),
                                                  info.CameraEnd2.x, info.CameraEnd2.y, info.CameraEnd2.z, (int)(info.TimeEnd2 * speedMovement));

            rotatingCoroutine = CameraRotationCoroutine(info.CamStartRotation.x, info.CamStartRotation.y, info.CamStartRotation.z, (int)(info.TimeStart * speedMovement),
                                                  info.CameraEndRotation.x, info.CameraEndRotation.y, info.CameraEndRotation.z, (int)(info.TimeEnd * speedMovement),
                                                  info.CameraEnd2Rotation.x, info.CameraEnd2Rotation.y, info.CameraEnd2Rotation.z, (int)(info.TimeEnd2 * speedMovement));

            StartCoroutine(movementCoroutine);
            StartCoroutine(rotatingCoroutine);

            if(info.ChangeTextMovement == true)
            {
                StartCoroutine(TextMovementCoroutine(info.TextStart.x, info.TextStart.y, info.TextStart.z, (int)(info.TextTimeStart * speedMovement),
                                                     info.TextEnd.x, info.TextEnd.y, info.TextEnd.z, (int)(info.TextTimeEnd * speedMovement),
                                                     info.TextEnd2.x, info.TextEnd2.y, info.TextEnd2.z, (int)(info.TextTimeEnd2 * speedMovement)));

                StartCoroutine(TextRotationCoroutine(info.TextStartRotation.x, info.TextStartRotation.y, info.TextStartRotation.z, (int)(info.TextTimeStart * speedMovement),
                                                     info.TextEndRotation.x, info.TextEndRotation.y, info.TextEndRotation.z, (int)(info.TextTimeEnd * speedMovement),
                                                     info.TextEnd2Rotation.x, info.TextEnd2Rotation.y, info.TextEnd2Rotation.z, (int)(info.TextTimeEnd2 * speedMovement)));
            }

        }



        public void CinematicCamera(DoublageEventText info)
        {
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);

            rotating = false;
            moving = false;
            cameraPlacement = 0;
            noCameraEffect = true;

            // Movement
            if (cinematicCoroutine != null)
                StopCoroutine(cinematicCoroutine);
            cinematicCoroutine = CameraMovementCoroutine(info.CamStart.x, info.CamStart.y, info.CamStart.z, info.TimeStart,
                                                  info.CameraEnd.x, info.CameraEnd.y, info.CameraEnd.z, info.TimeEnd,
                                                  info.CameraEnd2.x, info.CameraEnd2.y, info.CameraEnd2.z, info.TimeEnd2);
            StartCoroutine(cinematicCoroutine);


            // Rotation
            if (cinematicRotatingCoroutine != null)
                StopCoroutine(cinematicRotatingCoroutine);
            cinematicRotatingCoroutine = CameraRotationCoroutine(info.CamStartRotation.x, info.CamStartRotation.y, info.CamStartRotation.z, info.TimeStart,
                                      info.CameraEndRotation.x, info.CameraEndRotation.y, info.CameraEndRotation.z, info.TimeEnd,
                                      info.CameraEnd2Rotation.x, info.CameraEnd2Rotation.y, info.CameraEnd2Rotation.z, info.TimeEnd2);
            StartCoroutine(cinematicRotatingCoroutine);

        }


        private IEnumerator CameraMovementCoroutine(float x, float y, float z, int time, 
                                                     float x2, float y2, float z2, int time2,
                                                     float x3, float y3, float z3, int time3)
        {
            if(time != -1)
                SetCamera(initialPosition.position.x + x, initialPosition.position.y + y, initialPosition.position.z + z);
            yield return null;
            yield return MoveCameraCoroutine(initialPosition.position.x + x2, initialPosition.position.y + y2, initialPosition.position.z + z2, time2);
            if(time3 != 0)
                yield return MoveCameraCoroutine(initialPosition.position.x + x3, initialPosition.position.y + y3, initialPosition.position.z + z3, time3);
            cinematicCoroutine = null;
        }

        
        private IEnumerator CameraRotationCoroutine(float x, float y, float z, int time,
                                                             float x2, float y2, float z2, int time2,
                                                             float x3, float y3, float z3, int time3)
        {
            if (time != -1)
                SetCameraRotation(initialPosition.eulerAngles.x + x, initialPosition.eulerAngles.y + y, initialPosition.eulerAngles.z + z);
            yield return null;
            yield return RotateCameraCoroutine(initialPosition.eulerAngles.x + x2, initialPosition.eulerAngles.y + y2, initialPosition.eulerAngles.z + z2, time2);
            if (time3 != 0)
                yield return RotateCameraCoroutine(initialPosition.eulerAngles.x + x3, initialPosition.eulerAngles.y + y3, initialPosition.eulerAngles.z + z3, time3);
            cinematicCoroutine = null;
        }




        private IEnumerator TextMovementCoroutine(float x, float y, float z, int time,
                                                     float x2, float y2, float z2, int time2,
                                                     float x3, float y3, float z3, int time3)
        {
            if (time != -1)
                SetText(textInitialPosition.position.x + x, textInitialPosition.position.y + y, textInitialPosition.position.z + z);
            yield return null;
            yield return MoveTextCoroutine(textInitialPosition.position.x + x2, textInitialPosition.position.y + y2, textInitialPosition.position.z + z2, time2);
            if (time3 != 0)
                yield return MoveTextCoroutine(textInitialPosition.position.x + x3, textInitialPosition.position.y + y3, textInitialPosition.position.z + z3, time3);
        }


        private IEnumerator TextRotationCoroutine(float x, float y, float z, int time,
                                             float x2, float y2, float z2, int time2,
                                             float x3, float y3, float z3, int time3)
        {
            if (time != -1)
                SetTextRotation(textInitialPosition.eulerAngles.x + x, textInitialPosition.eulerAngles.y + y, textInitialPosition.eulerAngles.z + z);
            yield return null;
            yield return RotateTextCoroutine(textInitialPosition.eulerAngles.x + x2, textInitialPosition.eulerAngles.y + y2, textInitialPosition.eulerAngles.z + z2, time2);
            if (time3 != 0)
                yield return RotateTextCoroutine(textInitialPosition.eulerAngles.x + x3, textInitialPosition.eulerAngles.y + y3, textInitialPosition.eulerAngles.z + z3, time3);
        }





        public void SetCamera(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z + offset);
        }

        public void SetCameraRotation(float x, float y, float z)
        {
            transform.eulerAngles = new Vector3(x, y, z);
        }

        private void SetText(float x, float y, float z)
        {
            text.position = new Vector3(x, y, z);
        }
        private void SetTextRotation(float x, float y, float z)
        {
            text.eulerAngles = new Vector3(x, y, z);
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
                if (pauseCoroutine == false)
                {
                    this.transform.position += new Vector3(speedX, speedY, speedZ);
                    time -= 1;
                }
                yield return null;
            }
            moving = false;
            this.transform.position = new Vector3(x, y, z + offset);
        }




        public void RotateCamera(float x, float y, float z, float time)
        {
            rotatingCoroutine = RotateCameraCoroutine(x, y, z, time);
            StartCoroutine(rotatingCoroutine);
        }

        private IEnumerator RotateCameraCoroutine(float x, float y, float z, float time)
        {  
            float angleX = this.transform.eulerAngles.x;
            if (angleX > 180)
            {
                angleX = -(360 - angleX);
            }
            if (x > 180)
                x = -(360 - x);
            float speedX = (x - angleX) / time;

            float angleY = this.transform.eulerAngles.y;
            if (angleY > 180)
                angleY = -(360 - angleY);
            if (y > 180)
                y = -(360 - y);
            float speedY = (y - angleY) / time;

            float angleZ = this.transform.eulerAngles.z;
            if (angleZ > 180)
                angleZ = -(360 - angleZ);
            if (z > 180)
                z = -(360 - z);

            float speedZ = (z - angleZ) / time;

            rotating = true;
            while (time != 0)
            {
                if (pauseCoroutine == false)
                {
                    this.transform.eulerAngles += new Vector3(speedX, speedY, speedZ);
                    time -= 1;
                }
                yield return null;
            }
            rotating = false;
            this.transform.eulerAngles = new Vector3(x, y, z);
        }




        public void ChangeOrthographicSize(float addValue, int time = 20)
        {
            /*if (orthographicCoroutine != null)
            {
                StopCoroutine(orthographicCoroutine);
            }
            orthographicCoroutine = OrthographicSizeTransition(addValue);*/
            StartCoroutine(OrthographicSizeTransition(addValue, time));
        }

        private IEnumerator OrthographicSizeTransition(float addValue, int time)
        {
            float rate = ((65 + addValue) - cameraComponent.fieldOfView) / time;
            while (time != 0)
            {
                cameraComponent.fieldOfView += rate;
                time -= 1;
                yield return null;
            }

            cameraComponent.orthographicSize = 65 + addValue;
            //orthographicCoroutine = null;
        }

























        // Placeholders ==================================================================

        public void SetCameraSwitchActor(bool goLeft)
        {
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);

            moving = true;
            rotating = false;
            if (goLeft == true)
            {
                transform.position = new Vector3(actorSwitchPosition.position.x + 0.05f, actorSwitchPosition.position.y, actorSwitchPosition.position.z + 0.05f);
                transform.eulerAngles = new Vector3(actorSwitchPosition.eulerAngles.x, actorSwitchPosition.eulerAngles.y, actorSwitchPosition.eulerAngles.z);
                MoveCamera(actorSwitchPosition.position.x - 0.05f, actorSwitchPosition.position.y, actorSwitchPosition.position.z - 0.05f, 90);
            }
            else
            {
                transform.position = new Vector3(actorSwitchPosition.position.x + 0.05f, actorSwitchPosition.position.y, -actorSwitchPosition.position.z + 0.05f);
                transform.eulerAngles = new Vector3(actorSwitchPosition.eulerAngles.x, actorSwitchPosition.eulerAngles.y - 90, -actorSwitchPosition.eulerAngles.z);
                MoveCamera(actorSwitchPosition.position.x - 0.05f, actorSwitchPosition.position.y, -actorSwitchPosition.position.z - 0.05f, 90);
            }
        }

        public void NotQuite()
        {
            ChangeOrthographicSize(20, 1);
            SetText(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z);
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if(rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);
            SetCamera(notQuitePosition.position.x, notQuitePosition.position.y, notQuitePosition.position.z);
            SetCameraRotation(notQuitePosition.eulerAngles.x, notQuitePosition.eulerAngles.y, notQuitePosition.eulerAngles.z);
            MoveCamera(notQuitePosition.position.x + 3, notQuitePosition.position.y, notQuitePosition.position.z, 80);
            RotateCamera(0, 90, 0, 80);
            cameraPlacement = 0;
            ChangeOrthographicSize(0, 80);
        }

        public void IngeSon()
        {
            StartCoroutine(MoveTextCoroutine(textInitialPosition.position.x - 1f, textInitialPosition.position.y - 0.4f, textInitialPosition.position.z + 0.4f, 20));
            //SetText(-7.2f, 2.4f, 0.4f);
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);

            noCameraEffect = true;

            soundEngineerCoroutine = IngeSonCameraMovementCoroutine();
            StartCoroutine(soundEngineerCoroutine);
        }

        private IEnumerator MoveTextCoroutine(float x, float y, float z, float time)
        {
            float speedX = (x - text.position.x) / time;
            float speedY = (y - text.position.y) / time;
            float speedZ = (z - text.position.z + offset) / time;
            while (time != 0)
            {
                text.position += new Vector3(speedX, speedY, speedZ);
                time -= 1;
                yield return null;
            }
            text.position = new Vector3(x, y, z + offset);
        }


        private IEnumerator RotateTextCoroutine(float x, float y, float z, float time)
        {
            float angleX = text.eulerAngles.x;
            if (angleX > 180)
            {
                angleX = -(360 - angleX);
            }
            if (x > 180)
                x = -(360 - x);
            float speedX = (x - angleX) / time;

            float angleY = text.eulerAngles.y;
            if (angleY > 180)
                angleY = -(360 - angleY);
            if (y > 180)
                y = -(360 - y);
            float speedY = (y - angleY) / time;

            float angleZ = text.eulerAngles.z;
            if (angleZ > 180)
                angleZ = -(360 - angleZ);
            if (z > 180)
                z = -(360 - z);

            float speedZ = (z - angleZ) / time;

            while (time != 0)
            {
                if (pauseCoroutine == false)
                {
                    text.eulerAngles += new Vector3(speedX, speedY, speedZ);
                    time -= 1;
                }
                yield return null;
            }
            text.eulerAngles = new Vector3(x, y, z);
        }




        private IEnumerator IngeSonCameraMovementCoroutine()
        {
            int time = 20;
            MoveCamera(ingeSonPosition.position.x, ingeSonPosition.position.y, ingeSonPosition.position.z, time);
            RotateCamera(ingeSonPosition.eulerAngles.x, ingeSonPosition.eulerAngles.y, ingeSonPosition.eulerAngles.z, time);
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            while (true)
            {
                time = 300;
                MoveCamera(ingeSonPosition.position.x-0.03f, ingeSonPosition.position.y-0.03f, ingeSonPosition.position.z, time);
                while (time != 0)
                {
                    time -= 1;
                    yield return null;
                }
                time = 300;
                MoveCamera(ingeSonPosition.position.x, ingeSonPosition.position.y, ingeSonPosition.position.z, time);
                while (time != 0)
                {
                    time -= 1;
                    yield return null;
                }
            }
        }


        public void IngeSon2Cancel()
        {
            //SetText(-6, 2.7f, 0);
            StartCoroutine(MoveTextCoroutine(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z, 30));
            if (soundEngineerCoroutine != null)
                StopCoroutine(soundEngineerCoroutine);
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);

            if (cinematicCoroutine != null)
                StopCoroutine(cinematicCoroutine);
            if (cinematicRotatingCoroutine != null)
                StopCoroutine(cinematicRotatingCoroutine);

            noCameraEffect = false;
            rotating = false;
            moving = false;
            MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, 30);
            RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z, 30);
            cameraPlacement = 0;
        }

        public void EndSequence1(float offset, float rotationOffset)
        {
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);
            SetCameraRotation(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + rotationOffset);
            SetCamera(initialPosition.position.x + offset, initialPosition.position.y, initialPosition.position.z);
            MoveCamera(initialPosition.position.x + offset + 0.02f, initialPosition.position.y, initialPosition.position.z, 60);
        }

        public void EndSequence()
        {
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);
            /*SetCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z);*/
            SetCameraRotation(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z);
            MoveCamera(this.transform.localPosition.x - 2, this.transform.localPosition.y, this.transform.localPosition.z, 80);
            StartCoroutine(EndSequenceCoroutine(80));
            //MoveCamera(initialPosition.position.x - 1.2f, initialPosition.position.y, initialPosition.position.z, 600);
        }

        private IEnumerator EndSequenceCoroutine(int time)
        {
            while(time != 0)
            {
                time -= 1;
                yield return null;
            }
            time = 110;
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            MoveCamera(this.transform.localPosition.x - 0.2f, this.transform.localPosition.y, this.transform.localPosition.z, time);
            while (time != 0)
            {
                time -= 1;
                yield return null;
            }
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            MoveCamera(this.transform.localPosition.x - 1f, this.transform.localPosition.y, this.transform.localPosition.z, 30);
        }


        [ContextMenu("enemySkill")]
        public void EnemySkill()
        {
            //SetText(-7.2f, 2.4f, 0.4f);
            StartCoroutine(MoveTextCoroutine(textAttackPosition.position.x, textAttackPosition.position.y, textAttackPosition.position.z, 40));
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);

            noCameraEffect = true;
            RotateCamera(enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 40);
            MoveCamera(enemyPosition.position.x, enemyPosition.position.y, enemyPosition.position.z, 40);
            RotateCamera(enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 600);
            //MoveCamera(enemyPosition.position.x, enemyPosition.position.y + 0.01f, enemyPosition.position.z, 1000);
        }

        [ContextMenu("enemySkill")]
        public void EnemySkillCancel()
        {
            //SetText(-7.2f, 2.4f, 0.4f);
            StartCoroutine(MoveTextCoroutine(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z, 40));
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            if (rotatingCoroutine != null)
                StopCoroutine(rotatingCoroutine);

            cameraPlacement = 0;
            noCameraEffect = false;
            rotating = false;
            moving = false;
            MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, 40);
            RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z, 40);
        }


        /*public void ChangeCameraViewport(float newX, float newY, float newWidth, float newHeight, float time)
        {
            StartCoroutine(ChangeCameraRect(newX, newY, newWidth, newHeight, time));
        }



        // Obsolete
        private IEnumerator ChangeCameraRect(float x, float y, float width, float height, float time)
        {
            float speedX = (x - cameraComponent.rect.x) / time;
            float speedY = (y - cameraComponent.rect.y) / time;
            float speedWidth = (width - cameraComponent.rect.width) / time;
            float speedHeight = (height - cameraComponent.rect.height) / time;
            Rect rect = cameraComponent.rect;
            while (time != 0)
            {
                rect.x += speedX;
                rect.width += speedWidth;
                rect.y += speedY;
                rect.height += speedHeight;
                cameraComponent.rect = rect;
                time -= 1;
                yield return null;
            }
            cameraComponent.rect = new Rect(x, y, width, height);
        }*/


        #endregion

    } // CameraController class

} // #PROJECTNAME# namespace