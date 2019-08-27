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
        int turnGain;
        [SerializeField]
        int turnVariance;





        public override void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {
            doublageManager.AddTurn(turnGain);
        }




    } // SkillEffectTurn class
	
}// #PROJECTNAME# namespace
