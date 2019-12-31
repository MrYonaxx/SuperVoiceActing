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
	public class SkillEffectInfluenceBonus : SkillEffectData
	{
        [SerializeField]
        bool inPercentage;
        [SerializeField]
        int influenceBonus;

        public override void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            /*Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }
            if (buff != null)
            {
                if (doublageManager.RolesManager.AddBuff(buff) == true) // on peut ajouter un buff au role
                {
                    SkillEffect(doublageManager.RolesManager, 1);
                }
            }
            else
            {
                SkillEffect(doublageManager.RolesManager, 1);
            }*/
        }

        /*public override void RemoveSkillEffect(DoublageManager doublageManager)
        {
            SkillEffect(doublageManager.RolesManager, -1);
        }


        private void SkillEffect(RoleManager roleManager, int multiplier)
        {
            if (inPercentage == true)
            {
                int value = roleManager.GetCurrentRoleDefense();
                roleManager.AddRoleBonus((int)(((multiplier * influenceBonus) / 100f) * value));
            }
            else
            {
                roleManager.AddRoleBonus((multiplier * influenceBonus));
            }
        }*/




    } // SkillEffectInfluenceBonus class
	
}// #PROJECTNAME# namespace
