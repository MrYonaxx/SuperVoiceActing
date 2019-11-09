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
    public class ResearchEventMover: ResearchEvent
    {
        [SerializeField]
        int moverDirection;

        private bool resetAfterMove = false;

        public override IEnumerator ApplyEvent(MenuResearchMovementManager menuResearch, int eventID)
        {
            if (menuResearch.PlayerData.ResearchPoint == 0)
            {
                resetAfterMove = true;
            }
            menuResearch.PlayerData.ResearchPoint += 1;
            switch (moverDirection)
            {
                case 2:
                    yield return menuResearch.Move(new Vector3(0, 0.48f, 0));
                    break;
                case 4:
                    yield return menuResearch.Move(new Vector3(-0.48f, 0, 0));
                    break;
                case 6:
                    yield return menuResearch.Move(new Vector3(0.48f, 0, 0));
                    break;
                case 8:
                    yield return menuResearch.Move(new Vector3(0, -0.48f, 0));
                    break;
            }
            if (resetAfterMove == true)
            {
                resetAfterMove = false;
                menuResearch.PlayerData.ResearchPoint = 0;
            }

        }
    } 

} // #PROJECTNAME# namespace