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
    /// Definition of the DoublageEventDeck class
    /// </summary>

    [System.Serializable]
    public class DoublageEventDeck : DoublageEvent
    {

        [SerializeField]
        [HideLabel]
        private EmotionStat newDeck;
        public EmotionStat NewDeck
        {
            get { return newDeck; }
        }
        [Space]
        [SerializeField]
        private int addComboMax;
        public int AddComboMax
        {
            get { return addComboMax; }
        }


    } // DoublageEventDeck class

} // #PROJECTNAME# namespace