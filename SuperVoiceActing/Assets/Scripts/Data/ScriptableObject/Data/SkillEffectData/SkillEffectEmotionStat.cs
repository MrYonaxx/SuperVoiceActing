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
    public class SkillEffectEmotionStat : SkillEffectData
    {
        [SerializeField]
        bool inPercentage;
        [SerializeField]
        int statVariance;

        [HideLabel]
        [SerializeField]
        EmotionStat emotionStat;





        public override void ApplySkillEffect(ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            if(inPercentage == true)
            {
            }
            else
            {
                actorsManager.AddActorStat(emotionStat);
            }
        }

        public override void RemoveSkillEffect(ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            if (inPercentage == true)
            {
            }
            else
            {
                actorsManager.AddActorStat(emotionStat.Reverse(emotionStat));
            }
        }

    } // SkillEffectEmotionStat class
	
}// #PROJECTNAME# namespace
