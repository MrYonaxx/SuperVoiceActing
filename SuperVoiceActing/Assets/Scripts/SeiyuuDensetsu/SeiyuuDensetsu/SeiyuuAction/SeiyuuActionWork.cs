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
        private int hpMaxGain;
        public int HpMaxGain
        {
            get { return hpMaxGain; }
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
            int stat = 0;
            for(int i = 0; i < seiyuuData.VoiceActor.Statistique.GetLength(); i++)
            {
                if(emotionGain.GetEmotion(i) != 0)
                {
                    stat += seiyuuData.VoiceActor.Statistique.GetEmotion(i);
                }
            }
            float rand = stat * (health / 0.5f);
            rand *= 100;
            rand += Random.Range(-10, 10);
            if(rand <= chanceOfBadDay)
            {
                seiyuuData.VoiceActor.Hp -= (int)(hpDamage * 1.25f);
                seiyuuData.VoiceActor.Hp = Mathf.Clamp(seiyuuData.VoiceActor.Hp, 0, seiyuuData.VoiceActor.HpMax);
                seiyuuData.MoneyGain += (int)(cost * 0.75f);
                return -1;
            }
            else if (rand <= chanceOfNormalDay)
            {
                seiyuuData.VoiceActor.Hp -= hpDamage;
                seiyuuData.VoiceActor.Hp = Mathf.Clamp(seiyuuData.VoiceActor.Hp, 0, seiyuuData.VoiceActor.HpMax);
                seiyuuData.MoneyGain += (int)(cost);
                return 0;
            }
            else
            {
                seiyuuData.VoiceActor.Hp -= (int)(hpDamage * 0.75f);
                seiyuuData.VoiceActor.Hp = Mathf.Clamp(seiyuuData.VoiceActor.Hp, 0, seiyuuData.VoiceActor.HpMax);
                seiyuuData.MoneyGain += (int)(cost * 1.25f);
                return 1;
            }
        }

        public override void ApplyWeek(SeiyuuData seiyuuData)
        {
            seiyuuData.VoiceActor.Statistique.Add(emotionGain);
        }

        // Used for showing changement
        public override void ApplyWeek(SeiyuuData seiyuuData, SeiyuuActorManager seiyuuActorManager)
        {
            seiyuuData.VoiceActor.Statistique.Add(emotionGain);
            seiyuuData.VoiceActor.HpMax += hpMaxGain;

            seiyuuActorManager.ShowActorStatModification(emotionGain);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace