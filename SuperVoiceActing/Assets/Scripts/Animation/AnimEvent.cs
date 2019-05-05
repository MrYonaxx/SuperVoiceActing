/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the AnimEvent class
    /// </summary>
    public class AnimEvent : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        UnityEvent unityEvent;

        [FoldoutGroup("")]
        [SerializeField]
        UnityEvent unityEvent2;

        #endregion


        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public void CallEvent()
        {
            unityEvent.Invoke();
        }

        public void CallEvent2()
        {
            unityEvent2.Invoke();
        }

        #endregion

    } // AnimEvent class

} // #PROJECTNAME# namespace