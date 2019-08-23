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
    public class SkillEffectCardGain : SkillEffectData
    {
        [SerializeField]
        [HideLabel]
        EmotionStat cardGain;

        public override void ApplySkillEffect(SkillTarget skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {
            doublageManager.ModifyDeck(cardGain);

        }

    } // SkillEffectCardGain class
	
}// #PROJECTNAME# namespace
