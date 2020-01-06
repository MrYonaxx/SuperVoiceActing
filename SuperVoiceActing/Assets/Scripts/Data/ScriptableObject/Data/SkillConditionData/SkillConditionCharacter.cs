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
    public class SkillConditionCharacter: SkillCondition
    {
        [SerializeField]
        StoryCharacterData characterData;

        public override bool CheckCondition(DoublageBattleParameter battleParameter)
        {
            for(int i = 0; i < battleParameter.VoiceActors.Count; i++)
            {
                if(battleParameter.VoiceActors[i].VoiceActorID == characterData.name)
                {
                    return true;
                }
            }
            return false;
        }

    } 

} // #PROJECTNAME# namespace