/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private float speedViewport = 1.05f;
        Vector2 initialSize;

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
            initialSize = viewportMask.sizeDelta;
        }

        public void SetViewportSetting(DoublageEventViewport viewportSetting)
        {
            if (viewportSetting.Time == 0)
            {
                viewport.anchoredPosition = viewportSetting.Position;
                viewportMask.sizeDelta = (initialSize * viewportSetting.ViewportSize);
                viewportOutline.sizeDelta = viewportSetting.ViewportSize;
            }
            else
            {
                StartCoroutine(MoveViewport(viewportSetting.Position));
                StartCoroutine(RotateViewport(viewportSetting.Rotation.z, viewportSetting.Time));
                if(viewportMask != null)
                    StartCoroutine(ChangeViewportSize(viewportSetting.ViewportSize, viewportSetting.Time));
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
            Vector3 speed = new Vector3(0,0, (z - viewportMask.eulerAngles.z) / time);
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
