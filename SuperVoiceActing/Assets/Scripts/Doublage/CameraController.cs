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

        bool moving = true;

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
            CameraMovement(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z,
                           initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z, time);
        }

        public void SetInitialPosition(Vector3 newPos, Vector3 newRot, int time)
        {
            StartCoroutine(MoveInitialPositionCoroutine(newPos.x, newPos.y, newPos.z, newRot.x, newRot.y, newRot.z, time));
        }

        private IEnumerator MoveInitialPositionCoroutine(float x, float y, float z, float angleX, float angleY, float angleZ, float time)
        {
            float speedX = (x - initialPosition.localPosition.x) / time;
            float speedY = (y - initialPosition.localPosition.y) / time;
            float speedZ = (z - initialPosition.localPosition.z) / time;
            Vector3 speedPosition = new Vector3(speedX, speedY, speedZ);
            Vector3 speedRotation = GetRotateSpeed(initialPosition, angleX, angleY, angleZ, time);
            while (time != 0)
            {
                if (pauseCoroutine == false)
                {
                    initialPosition.localPosition += speedPosition;
                    initialPosition.eulerAngles += speedRotation;
                    time -= 1;
                }
                yield return null;
            }
            initialPosition.localPosition = new Vector3(x, y, z);
            initialPosition.eulerAngles = new Vector3(angleX, angleY, angleZ);
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
            SetText(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z);
            SetTextRotation(textInitialPosition.eulerAngles.x, textInitialPosition.eulerAngles.y, textInitialPosition.eulerAngles.z);

            SetCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z);
            SetCameraRotation(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z);

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
            //Debug.Log(currentCamMovement.name);
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
                                                 initialPosition.position.x + camDatas[i].DataPosition.x,
                                                 initialPosition.position.y + camDatas[i].DataPosition.y,
                                                 initialPosition.position.z + camDatas[i].DataPosition.z,
                                                 initialPosition.eulerAngles.x + camDatas[i].DataRotation.x,
                                                 initialPosition.eulerAngles.y + camDatas[i].DataRotation.y,
                                                 initialPosition.eulerAngles.z + camDatas[i].DataRotation.z,
                                                 (int)(camDatas[i].DataTime * speedMovement));
            }
            movCoroutine = null;
        }




        private IEnumerator TextMovementCoroutine(CamDataNode[] textDatas)
        {
            for (int i = 0; i < textDatas.Length; i++)
            {
                yield return MovementCoroutine(text, 
                                               textInitialPosition.position.x + textDatas[i].DataPosition.x,
                                               textInitialPosition.position.y + textDatas[i].DataPosition.y,
                                               textInitialPosition.position.z + textDatas[i].DataPosition.z,
                                               textInitialPosition.eulerAngles.x + textDatas[i].DataRotation.x,
                                               textInitialPosition.eulerAngles.y + textDatas[i].DataRotation.y,
                                               textInitialPosition.eulerAngles.z + textDatas[i].DataRotation.z,
                                               (int)(textDatas[i].DataTime * speedMovement));
            }
            movTextCoroutine = null;
        }





        public void SetCamera(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
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









        private IEnumerator MovementCoroutine(Transform transform, float x, float y, float z, float angleX, float angleY, float angleZ, float time)
        {
            time = Mathf.Max(1, time);
            float speedX = (x - transform.position.x) / time;
            float speedY = (y - transform.position.y) / time;
            float speedZ = (z - transform.position.z) / time;
            Vector3 speedPosition = new Vector3(speedX, speedY, speedZ);
            Vector3 speedRotation = GetRotateSpeed(transform, angleX, angleY, angleZ, time);
            moving = true;
            while (time != 0)
            {
                if (pauseCoroutine == false)
                {
                    transform.position += speedPosition;
                    transform.eulerAngles += speedRotation;
                    time -= 1;
                }
                yield return null;
            }
            moving = false;
            transform.position = new Vector3(x, y, z);
            transform.eulerAngles = new Vector3(angleX, angleY, angleZ);
        }


        private Vector3 GetRotateSpeed(Transform transform, float x, float y, float z, float time)
        {
            float angleX = transform.eulerAngles.x;
            if (angleX > 180)
            {
                angleX = -(360 - angleX);
            }
            if (x > 180)
                x = -(360 - x);
            float speedX = (x - angleX) / time;

            float angleY = transform.eulerAngles.y;
            if (angleY > 180)
                angleY = -(360 - angleY);
            if (y > 180)
                y = -(360 - y);
            float speedY = (y - angleY) / time;

            float angleZ = transform.eulerAngles.z;
            if (angleZ > 180)
                angleZ = -(360 - angleZ);
            if (z > 180)
                z = -(360 - z);

            float speedZ = (z - angleZ) / time;

            return new Vector3(speedX, speedY, speedZ);
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
                SetCamera(actorSwitchPosition.position.x + 0.05f, actorSwitchPosition.position.y, actorSwitchPosition.position.z + 0.05f);
                SetCameraRotation(actorSwitchPosition.eulerAngles.x, actorSwitchPosition.eulerAngles.y, actorSwitchPosition.eulerAngles.z);
                CameraMovement(actorSwitchPosition.position.x - 0.05f, actorSwitchPosition.position.y, actorSwitchPosition.position.z - 0.05f,
                               actorSwitchPosition.eulerAngles.x, actorSwitchPosition.eulerAngles.y, actorSwitchPosition.eulerAngles.z, 90);
            }
            else
            {
                SetCamera(actorSwitchPosition.position.x + 0.05f, actorSwitchPosition.position.y, -actorSwitchPosition.position.z + 0.05f);
                SetCameraRotation(actorSwitchPosition.eulerAngles.x, actorSwitchPosition.eulerAngles.y-90, -actorSwitchPosition.eulerAngles.z);
                CameraMovement(actorSwitchPosition.position.x - 0.05f, actorSwitchPosition.position.y, -actorSwitchPosition.position.z - 0.05f,
                               actorSwitchPosition.eulerAngles.x, actorSwitchPosition.eulerAngles.y-90, -actorSwitchPosition.eulerAngles.z+360, 90);
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
            //StartCoroutine(MoveTextCoroutine(textAttackPosition.position.x, textAttackPosition.position.y, textAttackPosition.position.z, 40));
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);

            noCameraEffect = true;
            CameraDataMovement(cameraMovementRoleAttack);
            //CameraMovement(enemyPosition.position.x, enemyPosition.position.y, enemyPosition.position.z, enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 40);
            /*RotateCamera(enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 40);
            MoveCamera(enemyPosition.position.x, enemyPosition.position.y, enemyPosition.position.z, 40);
            RotateCamera(enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 600);*/
        }

        [ContextMenu("enemySkill")]
        public void EnemySkillCounter()
        {
            //StartCoroutine(MoveTextCoroutine(textAttackPosition.position.x, textAttackPosition.position.y, textAttackPosition.position.z, 40));
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);

            cameraPlacement = 0;
            noCameraEffect = false;
            CameraDataMovement(cameraMovementRoleCounter);
            //CameraMovement(enemyPosition.position.x, enemyPosition.position.y, enemyPosition.position.z, enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 40);
            /*RotateCamera(enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 40);
            MoveCamera(enemyPosition.position.x, enemyPosition.position.y, enemyPosition.position.z, 40);
            RotateCamera(enemyPosition.eulerAngles.x, enemyPosition.eulerAngles.y, enemyPosition.eulerAngles.z, 600);*/
        }

        [ContextMenu("enemySkill")]
        public void EnemySkillCancel()
        {
            //StartCoroutine(MoveTextCoroutine(textInitialPosition.position.x, textInitialPosition.position.y, textInitialPosition.position.z, 40));
            if (movCoroutine != null)
                StopCoroutine(movCoroutine);
            if (movTextCoroutine != null)
                StopCoroutine(movTextCoroutine);

            cameraPlacement = 0;
            noCameraEffect = false;
            moving = false;
            MoveToInitialPosition(40);
            /*MoveCamera(initialPosition.position.x, initialPosition.position.y, initialPosition.position.z, 40);
            RotateCamera(initialPosition.eulerAngles.x, initialPosition.eulerAngles.y, initialPosition.eulerAngles.z, 40);*/
        }


        #endregion

    } // CameraController class

} // #PROJECTNAME# namespace