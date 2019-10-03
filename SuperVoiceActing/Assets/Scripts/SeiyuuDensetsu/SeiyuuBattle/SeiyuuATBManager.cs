/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SeiyuuATBManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        float atbMaxValue = 100f;

        [SerializeField]
        float atbDepletionSpeed = 1;

        [SerializeField]
        RectTransform atbGauge;

        [SerializeField]
        UnityEvent eventAtb;

        float atbCurrentValue = 50f;

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

        public void AddATB(float value)
        {
            atbCurrentValue += value;
            atbGauge.localScale = new Vector3((atbCurrentValue / atbMaxValue), atbGauge.localScale.y, atbGauge.localScale.z);
        }

        public void StartATB()
        {
            StartCoroutine(ATBCoroutine());
        }

        public void StartATB(float value)
        {
            atbCurrentValue = value;
            StartCoroutine(ATBCoroutine());
        }

        private IEnumerator ATBCoroutine()
        {
            while (atbCurrentValue > 0)
            {
                atbCurrentValue -= atbDepletionSpeed * Time.deltaTime;
                if (atbCurrentValue <= 0)
                {
                    atbGauge.localScale = new Vector3(0, atbGauge.localScale.y, atbGauge.localScale.z);
                    atbCurrentValue = 0;
                    eventAtb.Invoke();
                }
                else
                {
                    atbGauge.localScale = new Vector3((atbCurrentValue / atbMaxValue), atbGauge.localScale.y, atbGauge.localScale.z);
                }
                yield return null;
            }

        }


        #endregion

    } // EmotionCard class

} // #PROJECTNAME# namespace