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
    [CreateAssetMenu(fileName = "SeiyuuActionRestData", menuName = "SeiyuuDensetsu/SeiyuuAction/SeiyuuActionRest", order = 1)]
    public class SeiyuuActionRest: SeiyuuAction
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        private int hpPercentage;
        public int HpPercentage
        {
            get { return hpPercentage; }
        }

        [SerializeField]
        private int randomPercentage;
        public int RandomPercentage
        {
            get { return randomPercentage; }
        }


        [SerializeField]
        private int hpMaxGain;

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
        public override int ApplyDay(SeiyuuData seiyuuData)
        {
            float health = seiyuuData.VoiceActor.HpMax * ((hpPercentage + Random.Range(-randomPercentage, randomPercentage)) / 100f);
            seiyuuData.VoiceActor.Hp += (int)health;
            seiyuuData.VoiceActor.Hp = Mathf.Clamp(seiyuuData.VoiceActor.Hp, 0, seiyuuData.VoiceActor.HpMax);
            return 0;
        }

        public override void ApplyWeek(SeiyuuData seiyuuData, SeiyuuActorManager seiyuuActorManager)
        {
            seiyuuData.VoiceActor.HpMax += hpMaxGain;
            seiyuuData.VoiceActor.Hp = Mathf.Clamp(seiyuuData.VoiceActor.Hp, 0, seiyuuData.VoiceActor.HpMax);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace