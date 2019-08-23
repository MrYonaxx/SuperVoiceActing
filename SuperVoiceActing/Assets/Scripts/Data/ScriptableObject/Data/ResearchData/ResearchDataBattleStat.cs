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
    public enum BattleSessionStat
    {
        Turn,
        MaxCombo,
        Atk,
        Def
    }

    [CreateAssetMenu(fileName = "ResearchDataBattleStat", menuName = "Research/ResearchDataBattleStat", order = 1)]
    public class ResearchDataBattleStat : ResearchData
	{
        [Space]
        [SerializeField]
        protected BattleSessionStat battleStat;
        public BattleSessionStat BattleStat
        {
            get { return battleStat; }
        }

        public override void ApplyResearchEffect(PlayerData playerData, int researchLevel)
        {
            switch(battleStat)
            {
                case BattleSessionStat.Turn:
                    //playerData.TurnLimit += researchValue[researchLevel];
                    break;
            }
        }

    } // ResearchDataBattleStat class
	
}// #PROJECTNAME# namespace
