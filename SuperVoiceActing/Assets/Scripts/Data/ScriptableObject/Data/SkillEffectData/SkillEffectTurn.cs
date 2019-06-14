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

    } // SkillEffectTurn class
	
}// #PROJECTNAME# namespace
