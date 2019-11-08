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
    public class ResearchEventChest: ResearchEvent
    {
        [SerializeField]
        private ResearchData researchData;


        public override bool CanAddToPlayerDataSave()
        {
            return true;
        }


        public override IEnumerator ApplyEvent(MenuResearchMovementManager menuResearch, int eventID)
        {
            if(isActive == false)
            {
                isActive = true;
                SetResearchEventInPlayerData(menuResearch.PlayerData.ResearchEventSaves, eventID);
                researchData.ApplyResearchEffect(menuResearch.PlayerData);
                animator.SetTrigger("Opening");
                yield return new WaitForSeconds(0.4f);
            }
            yield return null;

        }



    } 

} // #PROJECTNAME# namespace