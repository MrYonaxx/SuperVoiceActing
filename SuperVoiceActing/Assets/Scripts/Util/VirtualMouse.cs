﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace VoiceActing
{
	public class VirtualMouse : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        int changeScreenVelocity = 50;

        float lastPositionX = 0;
        bool mouseHeldDown = false;

        [SerializeField]
        RectTransform virtualMouse;
        /*[SerializeField]
        TextMeshProUGUI textMouseDescription;
        [SerializeField]
        TextMeshProUGUI textMouseDescription;*/
        [SerializeField]
        ParticleSystem[] particleMouse;

        [SerializeField]
        UnityEvent eventMouseLeft;
        [SerializeField]
        UnityEvent eventMouseRight;


        private IEnumerator cursorCoroutine = null;

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

        protected void Start()
        {
            //StartCoroutine(ParticleCoroutine());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        protected void Update()
        {
            float screenRatioX = Input.mousePosition.x / Screen.width;
            float screenRatioY = Input.mousePosition.y / Screen.height;

            virtualMouse.anchoredPosition = new Vector2((-1920 / 2) + 1920 * screenRatioX, (-1080 / 2) + 1080 * screenRatioY);

            if (Input.GetMouseButton(0) && mouseHeldDown == false)
            {
                if (virtualMouse.anchoredPosition.x - lastPositionX >= changeScreenVelocity)
                {             
                    eventMouseRight.Invoke();
                    mouseHeldDown = true;
                    cursorCoroutine = WaitRepeat();
                    StartCoroutine(cursorCoroutine);
                }
                else if (virtualMouse.anchoredPosition.x - lastPositionX <= -changeScreenVelocity)
                {
                    eventMouseLeft.Invoke();
                    mouseHeldDown = true;
                    cursorCoroutine = WaitRepeat();
                    StartCoroutine(cursorCoroutine);
                }
            }
            else if (!Input.GetMouseButton(0) && mouseHeldDown == true)
            {
                mouseHeldDown = false;
                if(cursorCoroutine != null)
                    StopCoroutine(cursorCoroutine);
            }

            lastPositionX = virtualMouse.anchoredPosition.x;

        }


        private IEnumerator WaitRepeat()
        {
            yield return new WaitForSeconds(0.2f);
            mouseHeldDown = false;
        }

       /* private IEnumerator ParticleCoroutine()
        {
            while (true)
            {
                for (int i = 0; i < particleMouse.Length; i++)
                {
                    particleMouse[i].Play();
                    particleMouse[i].transform.position = virtualMouse.transform.position;
                    yield return new WaitForSeconds(0.1f / particleMouse.Length);
                }
            }
        }*/

        #endregion

    } // VirtualMouse class
	
}// #PROJECTNAME# namespace