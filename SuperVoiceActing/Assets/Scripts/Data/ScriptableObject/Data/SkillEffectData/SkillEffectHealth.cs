﻿/*****************************************************************
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












        /*public override void RemoveSkillEffect(SkillTarget skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            int currentHP = 0;
            float damage = 0;
            switch (skill)
            {
                case SkillTarget.VoiceActor:
                    if (inPercentage == true)
                    {
                        currentHP = actorsManager.GetCurrentActorHPMax();
                        damage = currentHP * (hpDamage / 100f);
                    }
                    else
                    {
                        damage = hpDamage;
                    }
                    damage += Random.Range(-hpDamageVariance, hpDamageVariance);
                    actorsManager.ActorTakeDamage((int)-damage);
                    break;


                case SkillTarget.Sentence:
                    if (inPercentage == true)
                    {
                        currentHP = enemyManager.GetHpMax();
                        damage = currentHP * (hpDamage / 100f);
                    }
                    else
                    {
                        damage = hpDamage;
                    }
                    damage += Random.Range(-hpDamageVariance, hpDamageVariance);
                    enemyManager.SetHp(enemyManager.GetHp() + (int)damage);
                    break;
            }
        }*/







    } // SkillEffectHealth class
	
}// #PROJECTNAME# namespace
