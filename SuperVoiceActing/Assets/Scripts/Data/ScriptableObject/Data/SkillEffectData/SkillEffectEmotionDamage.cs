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
	public class SkillEffectEmotionDamage : SkillEffectData
    {
        [SerializeField]
        bool inPercentage;
        [SerializeField]
        int damageVariance;

        [HideLabel]
        [SerializeField]
        EmotionStat emotionDamage;


        public override void ApplySkillEffect(ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            if (inPercentage == true)
            {
            }
            else
            {
                //actorsManager.AddActorStat(emotionStat);
            }
        }

        public override void RemoveSkillEffect(ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            if (inPercentage == true)
            {
            }
            else
            {
                //actorsManager.AddActorStat(emotionStat.Reverse(emotionStat));
            }
        }

    } // SkillEffectEmotionDamage class
	
}// #PROJECTNAME# namespace
