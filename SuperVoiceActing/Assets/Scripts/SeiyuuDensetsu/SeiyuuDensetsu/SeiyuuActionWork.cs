/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    [CreateAssetMenu(fileName = "SeiyuuActionWorkData", menuName = "SeiyuuDensetsu/SeiyuuAction/SeiyuuActionWork", order = 1)]
    public class SeiyuuActionWork: SeiyuuAction
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        private EmotionStat emotionGain;
        public EmotionStat EmotionGain
        {
            get { return emotionGain; }
        }

        [SerializeField]
        private int hpDamage;
        public int HpDamage
        {
            get { return hpDamage; }
        }


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


        #endregion

    } 

} // #PROJECTNAME# namespace