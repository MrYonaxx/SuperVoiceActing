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
    public enum SkillConditionType
    {
        Hp,
        Percentage,
        Combo,

        Timing,
        RoleType,
        ContractType,
        Date,
        Turn,
        Line,
        Character

    }

    [System.Serializable]
    public class SkillConditionNode
    {
        [HorizontalGroup("SkillCondition", Width = 0.2f)]
        [HideLabel]
        public SkillConditionType eventNode;

        [VerticalGroup("SkillCondition/Right")]
        [ShowIf("eventNode", SkillConditionType.Hp)]
        [SerializeField]
        [HideLabel]
        public SkillConditionHP skillConditionHP = null;

        [VerticalGroup("SkillCondition/Right")]
        [ShowIf("eventNode", SkillConditionType.Percentage)]
        [SerializeField]
        [HideLabel]
        public SkillConditionPercentage skillConditionPercentage = null;

        [VerticalGroup("SkillCondition/Right")]
        [ShowIf("eventNode", SkillConditionType.Combo)]
        [SerializeField]
        [HideLabel]
        public SkillConditionCombo skillConditionCombo = null;



        [VerticalGroup("SkillCondition/Right")]
        [ShowIf("eventNode", SkillConditionType.ContractType)]
        [SerializeField]
        [HideLabel]
        public SkillConditionContractType skillConditionContractType = null;


        [VerticalGroup("SkillCondition/Right")]
        [ShowIf("eventNode", SkillConditionType.Character)]
        [SerializeField]
        [HideLabel]
        public SkillConditionCharacter skillConditionCharacter = null;



        public SkillCondition GetSkillConditionNode()
        {
            switch (eventNode)
            {
                case SkillConditionType.Hp:
                    return skillConditionHP;
                case SkillConditionType.Percentage:
                    return skillConditionPercentage;
                case SkillConditionType.Combo:
                    return skillConditionCombo;
                case SkillConditionType.ContractType:
                    return skillConditionContractType;
                case SkillConditionType.Character:
                    return skillConditionCharacter;
            }
            return null;
        }

    } 

} // #PROJECTNAME# namespace