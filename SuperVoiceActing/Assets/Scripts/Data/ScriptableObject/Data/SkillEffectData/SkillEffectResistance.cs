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
        float addAttackPercentage = 0;
        [SerializeField]
        float addResistancePercentage = 0;


        public override void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            VoiceActor va = doublageBattleParameter.VoiceActors[0];
            va.BonusDamage += addAttackPercentage;
            va.BonusResistance += addResistancePercentage;
        }

        public override void RemoveSkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            doublageBattleParameter.VoiceActors[0].BonusDamage -= addAttackPercentage;
            doublageBattleParameter.VoiceActors[0].BonusResistance -= addResistancePercentage;
        }



        public override void PreviewSkill(DoublageBattleParameter doublageBattleParameter)
        {
            VoiceActor va = doublageBattleParameter.VoiceActors[0];
            va.BonusDamage += addAttackPercentage;
            va.BonusResistance += addResistancePercentage;
            doublageBattleParameter.ActorsManager.AddAttackPower(va, 0);
            doublageBattleParameter.ActorsManager.AddAttackDamage(va, 0, 0);
        }

        public override void StopPreview(DoublageBattleParameter doublageBattleParameter)
        {
            VoiceActor va = doublageBattleParameter.VoiceActors[0];
            va.BonusDamage -= addAttackPercentage;
            va.BonusResistance -= addResistancePercentage;
            doublageBattleParameter.ActorsManager.AddAttackPower(va, 0);
            doublageBattleParameter.ActorsManager.AddAttackDamage(va, 0, 0);
        }


    } // SkillEffectResistance class

}// #PROJECTNAME# namespace
