/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
    [System.Serializable]
    public class SkillEffectHealth : SkillEffectData
    {
        [SerializeField]
        bool inPercentage = false;
        [SerializeField]
        int hpDamage;
        [SerializeField]
        int hpDamageVariance;



        public override void ApplySkillEffect(SkillTarget skillTarget, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {
            int currentHP = 0;
            float damage = 0;
            switch (skillTarget)
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
                    actorsManager.ActorTakeDamage((int)damage);
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
                    enemyManager.SetHp(enemyManager.GetHp() - (int)damage);
                    break;
            }
        }












        public override void RemoveSkillEffect(SkillData skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            int currentHP = 0;
            float damage = 0;
            switch (skill.SkillTarget)
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
        }







    } // SkillEffectHealth class
	
}// #PROJECTNAME# namespace
