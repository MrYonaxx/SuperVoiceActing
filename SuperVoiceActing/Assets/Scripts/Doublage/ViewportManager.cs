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
        [SerializeField]
        RectTransform viewportOutline;
        [SerializeField]
        RectTransform viewportRotationOutline;
        [SerializeField]
        RectTransform viewportMask;
        [SerializeField]
        RectTransform viewportTexture;

        [SerializeField]
        RectTransform viewportText;
        [SerializeField]
        CameraController viewportCam;

        private float speedViewport = 1.05f;
        Vector2 initialSize;

        [Space]
        [Space]
        [Space]

        [Header("Debug")]


        [OnValueChanged("DebugChangeSize")]
        [SerializeField]
        Vector2 debugSize = new Vector2(2880, 1620);

        [Space]
        [SerializeField]
        Vector3 debugRotation = new Vector3(0f, 0f, 0f);
        [SerializeField]
        Vector2 debugSizeModifier = new Vector2(1f, 1f);


        private void DebugChangeSize()
        {
            viewportMask.sizeDelta = (debugSize * debugSizeModifier);
            viewportOutline.sizeDelta = (debugSize * debugSizeModifier);
        }
        private void DebugChangeRotation()
        {
            viewportMask.eulerAngles = debugRotation;
            viewportRotationOutline.eulerAngles = debugRotation;
            viewportTexture.eulerAngles = debugRotation - new Vector3(0,0,debugRotation.z);
        }


        [Button]
        private void UpdateDebug()
        {
            DebugChangeSize();
            DebugChangeRotation();
        }

        [Button]
        private void ResetDebug()
        {
            debugSize = new Vector2(2880, 1620);
            debugSizeModifier = new Vector2(1f, 1f);
            debugRotation = new Vector3(0f, 0f, 0f);
            DebugChangeSize();
            DebugChangeRotation();
        }



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
            if (viewportMask != null)
                initialSize = viewportMask.sizeDelta;
        }

        public void SetViewportSetting(DoublageEventViewport viewportSetting)
        {
            if (coroutineMove != null)
                StopCoroutine(coroutineMove);
            if (coroutineViewport != null)
                StopCoroutine(coroutineViewport);
            if (coroutineRotate != null)
                StopCoroutine(coroutineRotate);

            if (viewportSetting.ChangeViewport == true)
            {
                if (viewportSetting.Time == 0)
                {
                    viewport.anchoredPosition = viewportSetting.Position;
                    if (viewportMask != null)
                        viewportMask.sizeDelta = (initialSize * viewportSetting.ViewportSize);
                    if (viewportOutline != null)
                        viewportOutline.sizeDelta = viewportSetting.ViewportSize;
                }
                else
                {
                    coroutineMove = MoveViewport(viewportSetting.Position);
                    StartCoroutine(coroutineMove);
                    if (viewportMask != null)
                    {
                        coroutineViewport = ChangeViewportSize(viewportSetting.ViewportSize, viewportSetting.Time);
                        StartCoroutine(coroutineViewport);
                        coroutineRotate = RotateViewport(viewportSetting.Rotation.z, viewportSetting.Time);
                        StartCoroutine(coroutineRotate);
                    }
                }
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


        private IEnumerator MoveViewport(Vector2 targetPosition)
        {
            float transformX = viewport.anchoredPosition.x - targetPosition.x;
            float transformY = viewport.anchoredPosition.y - targetPosition.y;
            Vector2 speed = new Vector2(transformX - (transformX / speedViewport),
                                        transformY - (transformY / speedViewport));

            while (Mathf.Abs(transformX) > 1 || Mathf.Abs(transformY) > 1)
            {
                viewport.anchoredPosition -= speed;
                transformX /= speedViewport;
                transformY /= speedViewport;
                speed = new Vector2(transformX - (transformX / speedViewport),
                                    transformY - (transformY / speedViewport));
                yield return null;
            }
            viewport.anchoredPosition = targetPosition;
        }


        // Bon c'est trop tard mais je viens te hanter pour te dire que t'aurais pu faire une classe plutot que de te retaper ce truc un milliard de fois
        private IEnumerator RotateViewport(float z, float time)
        {
            if (z > 180)
            {
                z = -(360 - z);
            }
            float angleZ = viewportMask.eulerAngles.z;
            if (angleZ > 180)
            {
                angleZ = -(360 - angleZ);
            }
            Vector3 speed = new Vector3(0,0, (z - angleZ) / time);
            while (time != 0)
            {
                viewportMask.eulerAngles += speed;
                viewportRotationOutline.eulerAngles += speed;
                viewportTexture.eulerAngles -= speed;
                time -= 1;
                yield return null;
            }
        }


        private IEnumerator ChangeViewportSize(Vector2 targetSize, float time)
        {
            Vector2 speed = new Vector2((viewportMask.sizeDelta.x - (initialSize.x * targetSize.x)) / time,
                                        (viewportMask.sizeDelta.y - (initialSize.y * targetSize.y)) / time);
            while (time != 0)
            {
                viewportMask.sizeDelta -= speed;
                viewportOutline.sizeDelta = viewportMask.sizeDelta;
                time -= 1;
                yield return null;
            }
            viewportMask.sizeDelta = (initialSize * targetSize);
            viewportOutline.sizeDelta = (initialSize * targetSize);
        }

        //private void Move

        #endregion

    } // ViewportManager class
	
}// #PROJECTNAME# namespace
