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
    public enum BattleState
    {
        Stress, 
        Cringe, 
        Oscar, 
        Overact,
        Theatrical,

    }

    [System.Serializable]
	public class SkillEffectState : SkillEffectData
	{
        [InfoBox("Poison\n" +
                 "Chais pas\n" +
                 "Pas d'ingeSon\n" +
                 "Pack de carte only\n" +
                 "Critique Only\n" +
                 " ")]

        [SerializeField]
        private BattleState state;


        public override void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {
            Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }


            if (doublageManager.ActorsManager.AddBuff(buff) == true)
            {
                SkillEffect(doublageManager, true);
            }
        }


        // Remove les effets généraux lié aux acteurs/phrases
        public override void RemoveSkillEffect(DoublageManager doublageManager)
        {
            SkillEffect(doublageManager, false);
        }








        private void SkillEffect(DoublageManager doublageManager, bool b)
        {
            switch(state)
            {
                case BattleState.Stress:
                    break;
                case BattleState.Theatrical:
                    doublageManager.EnemyManager.SetDamageOnlyCritical(b);
                    break;
            }
        }





    } // SkillEffectState class
	
}// #PROJECTNAME# namespace
