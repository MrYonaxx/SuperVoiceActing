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
    public class SkillEffectHealth : SkillEffectData
    {
        [SerializeField]
        [HideLabel]
        SkillTarget skillTarget;

        [HorizontalGroup("effectHP", LabelWidth = 150)]
        [SerializeField]
        bool inPercentage = false;
        [HorizontalGroup("effectHP", LabelWidth = 150)]
        [SerializeField]
        bool ignoreHPLimit = false;

        [HorizontalGroup("effectHPDigit", LabelWidth = 150)]
        [SerializeField]
        int hpGain;

        [HorizontalGroup("effectHPDigit", LabelWidth = 150)]
        [SerializeField]
        int hpGainVariance;

        /*public override void PreviewSkill(DoublageManager doublageManager)
        {

        }*/


        public override void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            int damage = 0;
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    damage = CalculateDamage(doublageBattleParameter.VoiceActors[0], doublageBattleParameter.ActorsManager);
                    doublageBattleParameter.ActorsManager.ActorTakeDamage(doublageBattleParameter.VoiceActors[0], -damage);
                    break;
                case SkillTarget.Sentence:
                    break;
            }
        }

        public override void RemoveSkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            //return;
            int damage = 0;
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    damage = CalculateDamage(doublageBattleParameter.VoiceActors[0], doublageBattleParameter.ActorsManager);
                    doublageBattleParameter.ActorsManager.ActorTakeDamage(doublageBattleParameter.VoiceActors[0], damage);
                    break;
                case SkillTarget.Sentence:
                    break;
            }
        }



        private int CalculateDamage(VoiceActor voiceActor, ActorsManager actorManager)
        {
            int currentHP = 0;
            int damage = 0;
            if (inPercentage == true)
            {
                currentHP = voiceActor.Hp;
                damage = (int)(currentHP * (hpGain / 100f));
            }
            else
            {
                damage = hpGain;
            }
            damage += Random.Range(-hpGainVariance, hpGainVariance);
            if (ignoreHPLimit == false)
                damage = Mathf.Min(damage, damage, voiceActor.ChipDamage);
            return damage;
        }



        public override void PreviewSkill(DoublageBattleParameter doublageBattleParameter)
        {
            int damage = 0;
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    damage = CalculateDamage(doublageBattleParameter.VoiceActors[0], doublageBattleParameter.ActorsManager);
                    doublageBattleParameter.ActorsManager.AddAttackDamageBonus(doublageBattleParameter.VoiceActors[0], -damage);
                    break;
            }
        }

        public override void StopPreview(DoublageBattleParameter doublageBattleParameter)
        {
            int damage = 0;
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    damage = CalculateDamage(doublageBattleParameter.VoiceActors[0], doublageBattleParameter.ActorsManager);
                    doublageBattleParameter.ActorsManager.AddAttackDamageBonus(doublageBattleParameter.VoiceActors[0], damage);
                    break;
            }
        }









    } // SkillEffectHealth class
	
}// #PROJECTNAME# namespace
