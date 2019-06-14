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
    public class SkillEffectCardGain : SkillEffectData
    {
        [SerializeField]
        [HideLabel]
        EmotionStat cardGain;
	
	} // SkillEffectCardGain class
	
}// #PROJECTNAME# namespace
