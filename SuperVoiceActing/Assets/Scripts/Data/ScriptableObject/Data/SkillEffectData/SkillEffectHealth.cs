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
        SkillTarget skillTarget;
        [SerializeField]
        bool inPercentage = false;
        [SerializeField]
        bool restoreChipDamage = false;

        [HideIf("restoreChipDamage")]
        [SerializeField]
        int hpDamage;
        [HideIf("restoreChipDamage")]
        [SerializeField]
        int hpDamageVariance;

        /*public override void PreviewSkill(DoublageManager doublageManager)
        {

        }*/


        public override void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {
            int currentHP = 0;
            float damage = 0;
            if(restoreChipDamage == true)
            {
                damage = -doublageManager.ActorsManager.GetCurrentActorHPRegain();
                doublageManager.ActorsManager.ResetActorRegain();
                doublageManager.ActorsManager.ActorTakeDamage((int)damage);
                return;
            }
            switch (skillTarget)
            {
                case SkillTarget.VoiceActor:
                    if (inPercentage == true)
                    {
                        currentHP = doublageManager.ActorsManager.GetCurrentActorHPMax();
                        damage = currentHP * (hpDamage / 100f);
                    }
                    else
                    {
                        damage = hpDamage;
                    }
                    damage += Random.Range(-hpDamageVariance, hpDamageVariance);
                    doublageManager.ActorsManager.ActorTakeDamage((int)damage);
                    break;


                case SkillTarget.Sentence:
                    if (inPercentage == true)
                    {
                        currentHP = doublageManager.EnemyManager.GetHpMax();
                        damage = currentHP * (hpDamage / 100f);
                    }
                    else
                    {
                        damage = hpDamage;
                    }
                    damage += Random.Range(-hpDamageVariance, hpDamageVariance);
                    doublageManager.EnemyManager.SetHp(doublageManager.EnemyManager.GetHp() - (int)damage);
                    doublageManager.SetReprintText(true);
                    break;
            }
        }






        public override void PreviewTarget(DoublageManager doublageManager)
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
        }









    } // SkillEffectHealth class
	
}// #PROJECTNAME# namespace
