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

        public override IEnumerator ApplyEvent(MenuResearchMovementManager menuResearch, int eventID)
        {
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

        }
    } 

} // #PROJECTNAME# namespace