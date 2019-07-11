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
	public class MenuScrollList : MonoBehaviour
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("Menu Input")]
        [SerializeField]
        protected RectTransform buttonScrollListTransform;
        [SerializeField]
        protected Animator animatorSelection;
        [SerializeField]
        protected int timeBeforeRepeat = 10;
        [SerializeField]
        protected int repeatInterval = 3;
        [SerializeField]
        protected int scrollSize = 8;
        [SerializeField]
        protected int buttonSize = 85;
        [SerializeField]
        protected RectTransform[] buttonsList;



        protected int currentTimeBeforeRepeat = -1;
        protected int currentRepeatInterval = -1;
        protected int lastDirection = 0; // 2 c'est bas, 8 c'est haut (voir numpad)
        protected int indexLimit = 0;

        protected IEnumerator coroutineScroll = null;
        protected int indexSelected = 0;



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

        public void SelectUp()
        {
            if (buttonsList.Length == 0)
            {
                return;
            }
            if (lastDirection != 8)
            {
                StopRepeat();
                lastDirection = 8;
            }

            if (CheckRepeat() == false)
                return;

            indexSelected -= 1;
            if (indexSelected <= -1)
            {
                indexSelected = buttonsList.Length - 1;
            }
            MoveScrollRect();
        }

        public void SelectDown()
        {
            if (buttonsList.Length == 0)
            {
                return;
            }
            if (lastDirection != 2)
            {
                StopRepeat();
                lastDirection = 2;
            }
            if (CheckRepeat() == false)
                return;

            indexSelected += 1;
            if (indexSelected >= buttonsList.Length)
            {
                indexSelected = 0;
            }
            MoveScrollRect();
        }


        // Check si on peut repeter l'input
        private bool CheckRepeat()
        {
            if (currentRepeatInterval == -1)
            {
                if (currentTimeBeforeRepeat == -1)
                {
                    currentTimeBeforeRepeat = timeBeforeRepeat;
                    return true;
                }
                else if (currentTimeBeforeRepeat == 0)
                {
                    currentRepeatInterval = repeatInterval;
                }
                else
                {
                    currentTimeBeforeRepeat -= 1;
                }
            }
            else if (currentRepeatInterval == 0)
            {
                currentRepeatInterval = repeatInterval;
                return true;
            }
            else
            {
                currentRepeatInterval -= 1;
            }
            return false;
        }

        public void StopRepeat()
        {
            currentRepeatInterval = -1;
            currentTimeBeforeRepeat = -1;
        }









        private void MoveScrollRect()
        {
            if (buttonScrollListTransform == null)
            {
                animatorSelection.gameObject.transform.position = buttonsList[indexSelected].transform.position;
                return;
            }
            if (indexSelected > indexLimit)
            {
                indexLimit = indexSelected;
                coroutineScroll = MoveScrollRectCoroutine();
                if (coroutineScroll != null)
                {
                    StopCoroutine(coroutineScroll);
                }
                StartCoroutine(coroutineScroll);
            }
            else if (indexSelected < indexLimit - scrollSize + 1)
            {
                indexLimit = indexSelected + scrollSize - 1;
                coroutineScroll = MoveScrollRectCoroutine();
                if (coroutineScroll != null)
                {
                    StopCoroutine(coroutineScroll);
                }
                StartCoroutine(coroutineScroll);
            }

        }

        private IEnumerator MoveScrollRectCoroutine()
        {
            float time = 10f;
            int ratio = indexLimit - scrollSize;
            Vector2 speed = new Vector2(0, (buttonScrollListTransform.anchoredPosition.y - ratio * buttonSize) / time);
            while (time != 0)
            {
                animatorSelection.gameObject.transform.position = buttonsList[indexSelected].transform.position;
                buttonScrollListTransform.anchoredPosition -= speed;
                time -= 1;
                yield return null;
            }
            animatorSelection.gameObject.transform.position = buttonsList[indexSelected].transform.position;
        }



        #endregion

    } // MenuScrollList class

}// #PROJECTNAME# namespace
