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
    public class SkillConditionHP: SkillCondition
    {
        [SerializeField]
        bool targetVoiceActor;
        [SerializeField]
        bool targetSentence;

        [MinMaxSlider(0, 100)]
        [SerializeField]
        Vector2Int hpInterval;

        public override bool CheckCondition(DoublageBattleParameter battleParameter)
        {
            if(targetVoiceActor)
                return CheckVoiceActorHP(battleParameter);

            if (targetSentence)
                return CheckSentenceHP(battleParameter);

            return false;
        }


        private bool CheckVoiceActorHP(DoublageBattleParameter battleParameter)
        {
            VoiceActor va = battleParameter.VoiceActors[battleParameter.IndexCurrentCharacter];
            float percentage = ((float)va.Hp / va.HpMax) * 100;
            return (hpInterval.x <= percentage && percentage <= hpInterval.y);
        }



        private bool CheckSentenceHP(DoublageBattleParameter battleParameter)
        {
            return false;
        }

    } 

} // #PROJECTNAME# namespace