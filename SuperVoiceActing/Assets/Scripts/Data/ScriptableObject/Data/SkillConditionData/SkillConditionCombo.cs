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
    public class SkillConditionCombo: SkillCondition
    {
        [HideLabel]
        [SerializeField]
        EmotionStat emotionCheck;

        public override bool CheckCondition(DoublageBattleParameter battleParameter)
        {
            EmotionStat attackCheck = new EmotionStat(emotionCheck.Joy, emotionCheck.Sadness, emotionCheck.Disgust, emotionCheck.Anger,
                                                      emotionCheck.Surprise, emotionCheck.Sweetness, emotionCheck.Fear, emotionCheck.Trust
                                                      );

            for (int i = 0; i < battleParameter.LastAttackEmotion.Length; i++)
            {
                attackCheck.Add((int)battleParameter.LastAttackEmotion[i], -1);
            }
            if (attackCheck.Neutral > 0) return false;
            if (attackCheck.Joy > 0) return false;
            if (attackCheck.Sadness > 0) return false;
            if (attackCheck.Disgust > 0) return false;
            if (attackCheck.Anger > 0) return false;
            if (attackCheck.Surprise > 0) return false;
            if (attackCheck.Sweetness > 0) return false;
            if (attackCheck.Fear > 0) return false;
            if (attackCheck.Trust > 0) return false;
            return true;
        }
    } 

} // #PROJECTNAME# namespace