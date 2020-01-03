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
            int currentHP = 0;
            float damage = 0;

            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    ModifyActorHP(doublageBattleParameter.VoiceActors[0]);
                    break;
                case SkillTarget.Sentence:
                    break;
            }
        }



        private void ModifyActorHP(VoiceActor voiceActor)
        {
            int currentHP = 0;
            int damage = 0;
            if (inPercentage == true)
            {
                currentHP = voiceActor.Hp;
                damage = (int) (currentHP * (hpGain / 100f));
            }
            else
            {
                damage = hpGain;
            }
            damage += Random.Range(-hpGainVariance, hpGainVariance);
            if(ignoreHPLimit == false)
                damage = Mathf.Clamp(damage, damage, voiceActor.ChipDamage);
            voiceActor.Hp += damage;
        }





        /*public override void PreviewTarget(DoublageManager doublageManager)
        {
            base.PreviewTarget(doublageManager);

            // Fonction ?
            int currentHP = 0;
            float damage = 0;
            if (inPercentage == true)
            {
                currentHP = doublageManager.ActorsManager.GetCurrentActorHPMax();
                damage = currentHP * (hpDamage / 100f);
            }
            else
            {
                damage = hpDamage;
            }
            //

            doublageManager.ActorsManager.AddAttackDamage((int)damage, 1);
            doublageManager.ActorsManager.DrawDamagePrevisualization();
        }

        public override void StopPreview(DoublageManager doublageManager)
        {
            // Fonction ?
            int currentHP = 0;
            float damage = 0;
            if (inPercentage == true)
            {
                currentHP = doublageManager.ActorsManager.GetCurrentActorHPMax();
                damage = currentHP * (hpDamage / 100f);
            }
            else
            {
                damage = hpDamage;
            }
            //

            doublageManager.ActorsManager.AddAttackDamage((int)-damage, 1);
            doublageManager.ActorsManager.DrawDamagePrevisualization();
        }*/









    } // SkillEffectHealth class
	
}// #PROJECTNAME# namespace
