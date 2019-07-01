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
    [System.Serializable]
	public class SkillEffectResistance : SkillEffectData
	{
        [SerializeField]
        int resistancePercentage = 0;


        public override void ApplySkillEffect(SkillTarget skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {
            switch(skill)
            {
                case SkillTarget.VoiceActor:
                    if (buffData != null)
                        actorsManager.ApplyBuff(this, buffData);
                    actorsManager.AddActorResistance(resistancePercentage);
                    break;
            }


        }



        public override void RemoveSkillEffect(SkillTarget skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            switch (skill)
            {
                case SkillTarget.VoiceActor:
                    actorsManager.AddActorResistance(-resistancePercentage);
                    break;
            }
        }

    } // SkillEffectResistance class

}// #PROJECTNAME# namespace
