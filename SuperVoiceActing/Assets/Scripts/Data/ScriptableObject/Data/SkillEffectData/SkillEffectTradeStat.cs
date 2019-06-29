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
	public class SkillEffectTradeStat : SkillEffectData
	{
        [HideLabel]
        [SerializeField]
        Emotion firstEmotion;

        [HideLabel]
        [SerializeField]
        Emotion secondEmotion;


        public override void ApplySkillEffect(SkillTarget skillTarget, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {


        }

        public override void RemoveSkillEffect(SkillData skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {

        }


    } // SkillEffectTradeStat class
	
}// #PROJECTNAME# namespace
