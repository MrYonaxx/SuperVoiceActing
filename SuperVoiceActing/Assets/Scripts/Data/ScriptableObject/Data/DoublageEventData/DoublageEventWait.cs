/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageEventWait class
    /// </summary>

    [System.Serializable]
    public class DoublageEventWait : DoublageEvent
    {
        
        [SerializeField]
        [HideLabel]
        private float wait;
        public float Wait
        {
            get { return wait; }
        }


        public override IEnumerator ExecuteNodeCoroutine(DoublageEventManager eventManager)
        {
            yield return new WaitForSeconds(wait / 60f);
        }


    } // DoublageEventWait class

} // #PROJECTNAME# namespace