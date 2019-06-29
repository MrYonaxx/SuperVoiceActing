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
    public class SkillEffectTurn : SkillEffectData
	{
        [SerializeField]
        bool inPercentage = false;
        [SerializeField]
        int turnGain;
        [SerializeField]
        int turnVariance;





        public override void ApplySkillEffect(SkillTarget skillTarget, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {
            doublageManager.AddTurn(turnGain);
        }




        /*public override void RemoveSkillEffect(SkillData skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            doublageManager.AddTurn(-turnGain);
        }*/


    } // SkillEffectTurn class
	
}// #PROJECTNAME# namespace
