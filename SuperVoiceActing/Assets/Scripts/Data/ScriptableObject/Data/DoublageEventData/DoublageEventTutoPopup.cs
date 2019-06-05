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
        private string titlePopup;
        public string TitlePopup
        {
            get { return titlePopup; }
        }

        [SerializeField]
        [TextArea(1,10)]
        private string textPopup;
        public string TextPopup
        {
            get { return textPopup; }
        }

    } // DoublageEventTutoPopup class

} // #PROJECTNAME# namespace