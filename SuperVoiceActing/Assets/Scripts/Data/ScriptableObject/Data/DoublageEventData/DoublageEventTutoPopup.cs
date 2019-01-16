/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the DoublageEventTutoPopup class
    /// </summary>
    /// 
    [System.Serializable]
    public class DoublageEventTutoPopup : DoublageEvent
    {
        [SerializeField]
        private int popupID;
        public int PopupID
        {
            get { return popupID; }
        }

    } // DoublageEventTutoPopup class

} // #PROJECTNAME# namespace