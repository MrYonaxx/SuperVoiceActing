/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using TMPro;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the PanelPlayer class
    /// </summary>
    public class PanelPlayer : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Header("Controller")]
        [SerializeField]
        RectTransform textPopup;
        [SerializeField]
        MouthAnimationImage mouthAnim;

        [SerializeField]
        TextMeshProUGUI textMeshProName;
        [SerializeField]
        TextMeshProUGUI textMeshProText;

        [Header("Position")]
        [SerializeField]
        RectTransform positionAppear;
        [SerializeField]
        RectTransform positionDisappear;


        RectTransform transform;

        [SerializeField]
        float speedPopupText = 15;
        [SerializeField]
        float initialTime = 60;
        [SerializeField]
        float letterTime = 3;

        private IEnumerator coroutine = null;

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
            transform = GetComponent<RectTransform>();
        }



        [ContextMenu("cameraScroll")]
        public void PanelScroll()
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = PanelScrollCoroutine(speedPopupText);
            StartCoroutine(coroutine);
        }

        private IEnumerator PanelScrollCoroutine(float time)
        {
            this.transform.SetParent(positionAppear);
            float speedScale = 1 / time;
            while (time != 0)
            {
                this.transform.anchoredPosition /= 1.1f;
                
                if(textPopup.transform.localScale.y < 1)
                    textPopup.transform.localScale += new Vector3(0, speedScale, 0);
                time -= 1;
                yield return null;
            }

            while (this.transform.anchoredPosition != Vector2.zero)
            {
                this.transform.anchoredPosition /= 1.1f;
                yield return null;
            }
        }



        [ContextMenu("cameraHide")]
        public void PanelHide()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = PanelHideCoroutine(speedPopupText / 2);
            StartCoroutine(coroutine);
        }

        private IEnumerator PanelHideCoroutine(float time)
        {
            this.transform.SetParent(positionDisappear);
            float speedScale = 1 / time;
            while (time != 0)
            {
                if (textPopup.transform.localScale.y > 0)
                    textPopup.transform.localScale -= new Vector3(0, speedScale, 0);
                time -= 1;
                yield return null;
            }


            //this.transform.SetParent(positionDisappear);
            while (this.transform.anchoredPosition != Vector2.zero)
            {
                this.transform.anchoredPosition /= 1.1f;
                yield return null;
            }
        }


        public void StartPopup(string name, string text)
        {
            textMeshProName.text = name;
            textMeshProText.text = text;
            //mouthAnim.ActivateMouth();
            StartCoroutine(PopupCoroutine());
        }


        private IEnumerator PopupCoroutine()
        {
            yield return PanelScrollCoroutine(speedPopupText);
            float time = initialTime + textMeshProText.text.Length * letterTime;
            while (time != 0)
            {
                /*if (time <= initialTime)
                    mouthAnim.DesactivateMouth();*/
                time -= 1;
                yield return null;
            }
            PanelHide();
        }


        #endregion

    } // PanelPlayer class

} // #PROJECTNAME# namespace