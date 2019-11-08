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
    public class ResearchEventSwitch: ResearchEvent
    {
        [SerializeField]
        private int eventSwitchBindOn;
        [SerializeField]
        private int eventSwitchBindOff;


        public override bool CanAddToPlayerDataSave()
        {
            return true;
        }

        public override void SetActive(bool b)
        {
            animator.SetBool("Active", b);
            isActive = b;
        }

        public override IEnumerator ApplyEvent(MenuResearchMovementManager menuResearch, int eventID)
        {
            SetResearchEventInPlayerData(menuResearch.PlayerData.ResearchEventSaves, eventID);
            SetResearchEventInPlayerData(menuResearch.PlayerData.ResearchEventSaves, eventSwitchBindOn);
            SetResearchEventInPlayerData(menuResearch.PlayerData.ResearchEventSaves, eventSwitchBindOff);
            SetActive(!isActive);

            for (int i = 0; i < menuResearch.ResearchEventList.Count; i++)
            {
                if (menuResearch.ResearchEventList[i].eventID == eventSwitchBindOn)
                {
                    menuResearch.ResearchEventList[i].researchEvent.SetActive(isActive);
                }
                else if (menuResearch.ResearchEventList[i].eventID == eventSwitchBindOff)
                {
                    menuResearch.ResearchEventList[i].researchEvent.SetActive(isActive);
                }
            }

            yield return new WaitForSeconds(0.4f);
        }

    } 

} // #PROJECTNAME# namespace