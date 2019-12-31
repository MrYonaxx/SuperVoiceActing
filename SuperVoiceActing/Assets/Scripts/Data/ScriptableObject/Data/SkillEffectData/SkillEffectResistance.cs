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
        int resistancePercentage = 0;


        public override void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            /*Buff buff = null;
            if (buffData != null)
            {
                buff = new Buff(this, buffData);
            }

            if (buffData != null)
                doublageManager.ActorsManager.AddBuff(buff);
            doublageManager.ActorsManager.AddActorResistance(resistancePercentage);*/
        }

        /*public override void RemoveSkillEffectActor(VoiceActor actor)
        {
            actor.AddActorResistance(-resistancePercentage);
        }*/


    } // SkillEffectResistance class

}// #PROJECTNAME# namespace
