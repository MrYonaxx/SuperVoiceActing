/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

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

        [Title("Camera Center")]
        [SerializeField]
        Transform initialPosition;
        [SerializeField]
        Transform enemyPosition;
        [SerializeField]
        Transform actorSwitchPosition;

        [Title("Text")]
        [SerializeField]
        Transform text;
        [SerializeField]
        Transform textInitialPosition;
        [SerializeField]
        Transform textAttackPosition;


        [Title("Camera Data")]
        [SerializeField]
        CameraMovementData[] cameraMovementSet1;
        [SerializeField]
        CameraMovementData[] cameraMovementSet2;

        [Space]
        [SerializeField]
        CameraMovementData[] cameraMovementRetake;
        [SerializeField]
        CameraMovementData[] cameraMovementSoundEngi;
        [SerializeField]
        CameraMovementData[] cameraMovementEndSequence;
        [SerializeField]
        CameraMovementData cameraMovementRoleAttack;
        [SerializeField]
        CameraMovementData cameraMovementRoleCounter;

        [Space]
        [SerializeField]
        Animator animatorCameraComplex;

        [Space]
        [Title("Debug")]
        public bool noCameraEffect = false;
        public float speedMovement = 0.5f;




        CameraMovementData currentCamMovement;
        [SerializeField]
        bool moving = false;

        private bool pauseCoroutine = false;
        private int cameraPlacement = 0;

        private IEnumerator movCoroutine;

        private IEnumerator movTextCoroutine;

        private IEnumerator soundEngineerCoroutine;



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
            Application.targetFrameRate = 60;
        }

        protected void Start()
        {
            Cursor.visible = true;
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
            if (moving == false)
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
            CameraMovement(initialPosition.localPosition.x, initialPosition.localPosition.y, initialPosition.localPosition.z,
                           initialPosition.localEulerAngles.x, initialPosition.localEulerAngles.y, initialPosition.localEulerAngles.z, time);
        }

        public void SetInitialPosition(Vector3 newPos, Vector3 newRot, int time)
        {
            initialPosition.localPosition = newPos;
            initialPosition.localEulerAngles = newRot;
            MoveToInitialPosition(time);
        }






        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        // B A T T L E    C A M E R A    M O V E M E N T
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        ///

        public void SetNewCameraData(DoublageEventCameraData doublageEventCameraData)
        {
            cameraMovementSet1 = doublageEventCameraData.CameraSet1;
            cameraMovementSet2 = doublageEventCameraData.CameraSet2;

            if (doublageEventCameraData.AnimatorCustomCameraID != 0)
            {
                animatorCameraComplex.SetInteger("Anim", doublageEventCameraData.AnimatorCustomCameraID);
                SetNoCameraEffect(true);
                animatorCameraComplex.enabled = true;
                cameraPlacement = -1;
                currentCamMovement = null;
            }

            if (doublageEventCameraData.CurrentCameraData != null)
            {
                currentCamMovement = doublageEventCameraData.CurrentCameraData;
            }
        }



        private void InitializeCameraEffect()
        {
            // Set Initial position (just to be sure its in place)
            SetText(textInitialPosition.localPosition.x, textInitialPosition.localPosition.y, textInitialPosition.localPosition.z);
            SetTextRotation(textInitialPosition.localEulerAngles.x, textInitialPosition.localEulerAngles.y, textInitialPosition.localEulerAngles.z);

            SetCamera(initialPosition.localPosition.x, initialPosition.localPosition.y, initialPosition.localPosition.z);
            SetCameraRotation(initialPosition.localEulerAngles.x, initialPosition.localEulerAngles.y, initialPosition.localEulerAngles.z);

            if (cameraPlacement == -1)
            {
                cameraPlacement = 0;
            }
            else if (cameraPlacement == 0)
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
                cameraPlacement = 0;
            }
            CameraDataMovement(currentCamMovement);

        }






        public void CameraDataMovement(CameraMovementData info)
        {
            if (info.ChangeTextMovement == true)
            {
                if (movTextCoroutine != null)
                    StopCoroutine(movTextCoroutine);
                movTextCoroutine = TextMovementCoroutine(info.TextDataNodes);
                StartCoroutine(movTextCoroutine);
            }

            if (info.ChangeCamMovement == true)
            {
                if (movCoroutine != null)
                    StopCoroutine(movCoroutine);
                movCoroutine = CameraMovementCoroutine(info.CamDataNodes);
                StartCoroutine(movCoroutine);
            }

            if (info.ChangeOrthographic == true)
            {
                ChangeOrthographicSize(info.OrthographicStart, 1);
                ChangeOrthographicSize(info.OrthographicEnd, (int)(info.TimeOrthographic * speedMovement));
            }
        }



        public void CinematicCamera(DoublageEventText info)
        {
            moving = false;
            cameraPlacement = 0;
            noCameraEffect = true;

            if (info.CameraMovementData != null)
            {
                CameraDataMovement(info.CameraMovementData);
            }
            else if (info.CamDataNodes.Length != 0)
            {
                if (movCoroutine != null)
                    StopCoroutine(movCoroutine);
                movCoroutine = CameraMovementCoroutine(info.CamDataNodes);
                StartCoroutine(movCoroutine);
            }
        }



        public void CameraMovement(float x, float y, float z, float angleX, float angleY, float angleZ, float time)
        {
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            movCoroutine = MovementCoroutine(this.transform, x, y, z, angleX, angleY, angleZ, time);
            StartCoroutine(movCoroutine);
        }

        public void TextMovement(float x, float y, float z, float angleX, float angleY, float angleZ, float time)
        {
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);
            movTextCoroutine = MovementCoroutine(text, x, y, z, angleX, angleY, angleZ, time);
            StartCoroutine(movTextCoroutine);
        }



        private IEnumerator CameraMovementCoroutine(CamDataNode[] camDatas)
        {
            for (int i = 0; i < camDatas.Length; i++)
            {
                yield return MovementCoroutine(this.transform,
                                                 initialPosition.localPosition.x + camDatas[i].DataPosition.x,
                                                 initialPosition.localPosition.y + camDatas[i].DataPosition.y,
                                                 initialPosition.localPosition.z + camDatas[i].DataPosition.z,
                                                 initialPosition.localEulerAngles.x + camDatas[i].DataRotation.x,
                                                 initialPosition.localEulerAngles.y + camDatas[i].DataRotation.y,
                                                 initialPosition.localEulerAngles.z + camDatas[i].DataRotation.z,
                                                 (int)(camDatas[i].DataTime * speedMovement));
            }
            movCoroutine = null;
        }




        private IEnumerator TextMovementCoroutine(CamDataNode[] textDatas)
        {
            for (int i = 0; i < textDatas.Length; i++)
            {
                yield return MovementCoroutine(text, 
                                               textInitialPosition.localPosition.x + textDatas[i].DataPosition.x,
                                               textInitialPosition.localPosition.y + textDatas[i].DataPosition.y,
                                               textInitialPosition.localPosition.z + textDatas[i].DataPosition.z,
                                               textInitialPosition.localEulerAngles.x + textDatas[i].DataRotation.x,
                                               textInitialPosition.localEulerAngles.y + textDatas[i].DataRotation.y,
                                               textInitialPosition.localEulerAngles.z + textDatas[i].DataRotation.z,
                                               (int)(textDatas[i].DataTime * speedMovement));
            }
            movTextCoroutine = null;
        }





        public void SetCamera(float x, float y, float z)
        {
            transform.localPosition = new Vector3(x, y, z);
        }

        public void SetCameraRotation(float x, float y, float z)
        {
            transform.localEulerAngles = new Vector3(x, y, z);
        }

        private void SetText(float x, float y, float z)
        {
            text.localPosition = new Vector3(x, y, z);
        }
        private void SetTextRotation(float x, float y, float z)
        {
            text.localEulerAngles = new Vector3(x, y, z);
        }


        public void SkipCameraMovement()
        {
            noCameraEffect = false;
            moving = false;
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);
            InitializeCameraEffect();
        }



        private IEnumerator MovementCoroutine(Transform transform, float x, float y, float z, float angleX, float angleY, float angleZ, float time)
        {
            time = Mathf.Max(1, time);
            time /= 60;
            Vector3 finalPosition = new Vector3(x, y, z);

            Vector3 finalRotation = GetRotateSpeed(transform, angleX, angleY, angleZ, 1);
            Vector3 currentPosition = transform.localPosition;
            Vector3 currentRotation = GetRotateSpeed(transform, transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z, 1);//this.transform.eulerAngles;
            moving = true;
            float t = 0f;
            while (t < 1)
            {
                if (pauseCoroutine == false)
                {
                    t += Time.deltaTime / time;
                    transform.localPosition = Vector3.Lerp(currentPosition, finalPosition, t);
                    transform.localEulerAngles = Vector3.Lerp(currentRotation, finalRotation, t);
                }
                yield return null;
            }
            moving = false;
            transform.localPosition = finalPosition;
            transform.localEulerAngles = finalRotation;
        }


        private Vector3 GetRotateSpeed(Transform transform, float x, float y, float z, float time)
        {
            if (x > 180)
                x = -(360 - x);
            if (y > 180)
                y = -(360 - y);
            if (z > 180)
                z = -(360 - z);


            return new Vector3(x, y, z);
        }








        public void ChangeOrthographicSize(float addValue, int time = 20)
        {
            StartCoroutine(OrthographicSizeTransition(addValue, time));
        }

        private IEnumerator OrthographicSizeTransition(float addValue, int time)
        {
            time = Mathf.Max(1, time);
            float rate = ((65 + addValue) - cameraComponent.fieldOfView) / time;
            while (time != 0)
            {
                cameraComponent.fieldOfView += rate;
                time -= 1;
                yield return null;
            }

            cameraComponent.orthographicSize = 65 + addValue;
        }




        // Placeholders ==================================================================

        public void SetCameraSwitchActor(bool goLeft)
        {
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);

            moving = true;
            if (goLeft == true)
            {
                SetCamera(actorSwitchPosition.localPosition.x + 0.05f, actorSwitchPosition.localPosition.y, actorSwitchPosition.localPosition.z + 0.05f);
                SetCameraRotation(actorSwitchPosition.localEulerAngles.x, actorSwitchPosition.localEulerAngles.y, actorSwitchPosition.localEulerAngles.z);
                CameraMovement(actorSwitchPosition.localPosition.x - 0.05f, actorSwitchPosition.localPosition.y, actorSwitchPosition.localPosition.z - 0.05f,
                               actorSwitchPosition.localEulerAngles.x, actorSwitchPosition.localEulerAngles.y, actorSwitchPosition.localEulerAngles.z, 90);
            }
            else
            {
                SetCamera(-actorSwitchPosition.localPosition.x + 0.05f, actorSwitchPosition.localPosition.y, actorSwitchPosition.localPosition.z + 0.05f);
                SetCameraRotation(-actorSwitchPosition.localEulerAngles.x, actorSwitchPosition.localEulerAngles.y-90, -actorSwitchPosition.localEulerAngles.z);
                CameraMovement(-actorSwitchPosition.localPosition.x - 0.05f, actorSwitchPosition.localPosition.y, actorSwitchPosition.localPosition.z - 0.05f,
                               -actorSwitchPosition.localEulerAngles.x, actorSwitchPosition.localEulerAngles.y-90, -actorSwitchPosition.localEulerAngles.z+360, 90);
            }
        }

        public void NotQuite()
        {
            CameraDataMovement(cameraMovementRetake[Random.Range(0, cameraMovementRetake.Length)]);
            if(cameraPlacement != -1)
                cameraPlacement = 0;
        }



        public void IngeSon()
        {
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);

            noCameraEffect = true;

            soundEngineerCoroutine = IngeSonCameraMovementCoroutine();
            StartCoroutine(soundEngineerCoroutine);
        }

        private IEnumerator IngeSonCameraMovementCoroutine()
        {
            yield return CameraMovementCoroutine(cameraMovementSoundEngi[0].CamDataNodes);
            while (true)
            {
                for(int i = 0; i < cameraMovementSoundEngi.Length; i++)
                {
                    yield return CameraMovementCoroutine(cameraMovementSoundEngi[i].CamDataNodes);
                }
            }
        }


        public void IngeSon2Cancel()
        {
            if (cameraPlacement == -1)
                return;

            if (soundEngineerCoroutine != null)
                StopCoroutine(soundEngineerCoroutine);
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);

            noCameraEffect = false;
            moving = false;
            MoveToInitialPosition(30);
            TextMovement(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z, 0, 90, 0, 30);
            cameraPlacement = 0;
        }

        public void EndSequence1(float offset, float rotationOffset)
        {
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            SetCamera(initialPosition.position.x + offset, initialPosition.position.y, initialPosition.position.z);
            SetCameraRotation(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + rotationOffset);
            CameraMovement(initialPosition.position.x + offset + 0.02f, initialPosition.position.y, initialPosition.position.z, 
                           initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z + rotationOffset, 60);
        }

        public void EndSequence()
        {
            CameraDataMovement(cameraMovementEndSequence[0]);
        }



        [ContextMenu("enemySkill")]
        public void EnemySkill()
        {
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);

            noCameraEffect = true;
            CameraDataMovement(cameraMovementRoleAttack);
        }

        [ContextMenu("enemySkill")]
        public void EnemySkillCounter()
        {
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);

            cameraPlacement = 0;
            noCameraEffect = false;
            CameraDataMovement(cameraMovementRoleCounter);
        }

        [ContextMenu("enemySkill")]
        public void EnemySkillCancel()
        {
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);

            cameraPlacement = 0;
            noCameraEffect = false;
            moving = false;
            MoveToInitialPosition(60);
            TextMovement(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z, 0, 90, 0, 60);
        }


        #endregion

    } // CameraController class

} // #PROJECTNAME# namespace