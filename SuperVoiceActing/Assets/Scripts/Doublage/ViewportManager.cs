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
	public class ViewportManager : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */












        [SerializeField]
        RectTransform viewport;
        /*[SerializeField]
        RectTransform viewportOutline;
        [SerializeField]
        RectTransform viewportRotationOutline;
        [SerializeField]
        RectTransform viewportMask;*/
        [SerializeField]
        RectTransform viewportTexture;

        [Space]
        [SerializeField]
        RectTransform viewportText;
        [SerializeField]
        CameraController viewportCam;

        [SerializeField]
        EffectManager effectManager;

        private float speedViewport = 1.05f;//1.05f;
        Vector2 initialSize;



        private IEnumerator coroutineMove = null;
        private IEnumerator coroutineViewport = null;
        private IEnumerator coroutineRotate = null;

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

        }

        public void SetCameraEffect(bool b)
        {
            viewportCam.gameObject.SetActive(b);
        }

        public void TextCameraEffect(DoublageEventText camEffect)
        {
            viewportCam.CinematicCamera(camEffect);
        }


        public void SetCameraData(DoublageEventCameraData data)
        {
            viewportCam.SetNewCameraData(data);
        }


        public void SetViewportSetting(DoublageEventViewport viewportSetting)
        {
            SetCameraEffect(true);
            if (coroutineMove != null)
                StopCoroutine(coroutineMove);
            if (coroutineRotate != null)
                StopCoroutine(coroutineRotate);

            if (viewportSetting.ChangeViewport == true)
            {
                coroutineMove = MoveViewport(viewportSetting.AnchorMin, viewportSetting.AnchorMax, viewportSetting.Time);
                StartCoroutine(coroutineMove);
                coroutineRotate = RotateViewport(viewportSetting.RotationZ, viewportSetting.Time);
                StartCoroutine(coroutineRotate);
            }

            if(viewportSetting.CamViewport == true)
            {
                viewportCam.SetInitialPosition(viewportSetting.CamPosition, viewportSetting.CamRotation, viewportSetting.TimeCamera);
            }

            if(viewportSetting.ChangeTextPosition == true)
            {
                viewportText.anchoredPosition = viewportSetting.TextPosition;
            }
        }







        private IEnumerator MoveViewport(Vector2 targetPositionMin, Vector2 targetPositionMax, float time)
        {
            float t = 0;
            time /= 60f;
            while (t < 1)
            {
                t += (Time.deltaTime / time);
                Vector2 positionMin = Vector2.Lerp(viewport.anchorMin, targetPositionMin, t);
                Vector2 positionMax = Vector2.Lerp(viewport.anchorMax, targetPositionMax, t);
                SetPosition(positionMin, positionMax);
                yield return null;
            }
        }

        private void SetPosition(Vector2 targetPositionMin, Vector2 targetPositionMax)
        {
            viewport.anchorMin = new Vector3(targetPositionMin.x, targetPositionMin.y, 0);
            viewport.anchorMax = new Vector3(targetPositionMax.x, targetPositionMax.y, 0);
            viewport.anchoredPosition = Vector2.zero;
        }

        private IEnumerator RotateViewport(float z, float time)
        {
            if (z > 180)
            {
                z = -(360 - z);
            }
            Vector3 finalRotation = new Vector3(0, 0, z);

            float t = 0;
            time /= 60f;
            while (t < 1)
            {
                t += (Time.deltaTime / time);
                viewport.eulerAngles = Vector3.Lerp(viewport.eulerAngles, finalRotation, t);
                viewportTexture.localEulerAngles = -viewport.localEulerAngles;
                yield return null;
            }
        }



        public void StartEffect(DoublageEventEffect effectData)
        {
            effectManager.StartEffect(effectData);
        }


        #endregion

    } // ViewportManager class
	
}// #PROJECTNAME# namespace
