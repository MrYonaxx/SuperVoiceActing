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
        int hpGain;
        [SerializeField]
        int hpGainVariance;

    } // SkillEffectHealth class
	
}// #PROJECTNAME# namespace
