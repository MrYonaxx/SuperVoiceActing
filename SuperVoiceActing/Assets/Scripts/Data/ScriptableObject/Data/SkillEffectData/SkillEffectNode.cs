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

    public enum SkillEffect
    {
        Health,
        EmotionStat,
        EmotionStatDamage,
        Turn,
        ActorStat,
        CardGain,
        TradeStat,
    }

    [System.Serializable]
    public class SkillEffectNode
	{
        [HorizontalGroup("SkillEffectGroup", Width = 0.2f)]
        [HideLabel]
        public SkillEffect eventNode;

        [VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.Health)]
        [SerializeField]
        [HideLabel]
        public SkillEffectHealth skillEffectHealth = null;

        [VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.EmotionStat)]
        [SerializeField]
        [HideLabel]
        public SkillEffectEmotionStat skillEffectEmotionStat = null;

        [VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.EmotionStatDamage)]
        [SerializeField]
        [HideLabel]
        public SkillEffectEmotionDamage skillEffectEmotionDamage = null;



        [VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.Turn)]
        [SerializeField]
        [HideLabel]
        public SkillEffectTurn skillEffectTurn = null;


        [VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.ActorStat)]
        [SerializeField]
        [HideLabel]
        public SkillEffectResistance skillEffectResistance = null;



        [VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.CardGain)]
        [SerializeField]
        [HideLabel]
        public SkillEffectCardGain skillEffectCardGain = null;

        [VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.TradeStat)]
        [SerializeField]
        [HideLabel]
        public SkillEffectTradeStat skillEffectTradeStat = null;



        /*[VerticalGroup("SkillEffectGroup/Right")]
        [ShowIf("eventNode", SkillEffect.Influence)]
        [SerializeField]
        [HideLabel]
        public SkillEffectInfluenceBonus skillEffectInfluenceBonus = null;*/


        public SkillEffectData GetSkillEffectNode()
        {
            switch(eventNode)
            {
                case SkillEffect.Health:
                    return skillEffectHealth;
                case SkillEffect.EmotionStat:
                    return skillEffectEmotionStat;
                case SkillEffect.EmotionStatDamage:
                    return skillEffectEmotionDamage;
                case SkillEffect.Turn:
                    return skillEffectTurn;
                case SkillEffect.ActorStat:
                    return skillEffectResistance;
                case SkillEffect.CardGain:
                    return skillEffectCardGain;
                case SkillEffect.TradeStat:
                    return skillEffectTradeStat;
            }
            return null;
        }


    } // SkillEffectData class
	
}// #PROJECTNAME# namespace
