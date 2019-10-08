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

        [SerializeField]
        private int chanceOfBadDay;
        public int ChanceOfBadDay
        {
            get { return chanceOfBadDay; }
        }
        [SerializeField]
        private int chanceOfNormalDay;
        public int ChanceOfNormalDay
        {
            get { return chanceOfNormalDay; }
        }
        [SerializeField]
        private int chanceOfGoodDay;
        public int ChanceOfGoodDay
        {
            get { return chanceOfGoodDay; }
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

        public override int ApplyDay(SeiyuuData seiyuuData)
        {
            float health = seiyuuData.VoiceActor.Hp / seiyuuData.VoiceActor.HpMax;
            int stat = seiyuuData.VoiceActor.Statistique.GetEmotion(0);
            float rand = stat * (health / 0.5f);
            rand *= 100;
            rand += Random.Range(-10, 10);
            if(rand <= chanceOfBadDay)
            {
                seiyuuData.VoiceActor.Hp -= (int)(hpDamage * 1.25f);
                seiyuuData.Money += (int)(cost * 0.75f);
                return -1;
            }
            else if (rand <= chanceOfNormalDay)
            {
                seiyuuData.VoiceActor.Hp -= hpDamage;
                seiyuuData.Money += (int)(cost);
                return 0;
            }
            else if (rand <= chanceOfGoodDay)
            {
                seiyuuData.VoiceActor.Hp -= (int)(hpDamage * 0.75f);
                seiyuuData.Money += (int)(cost * 1.25f);
                return 1;
            }
            return 0;
        }

        public override void ApplyWeek(SeiyuuData seiyuuData)
        {
            seiyuuData.VoiceActor.Statistique.Add(emotionGain);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace