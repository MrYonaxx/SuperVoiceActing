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
	public class ResearchData : ScriptableObject
	{
        [Space]
        [SerializeField]
        protected string researchName;
        public string ResearchName
        {
            get { return researchName; }
        }

        [SerializeField]
        [HideLabel]
        [TextArea(1,1)]
        protected string researchDescription;
        public string ResearchDescription
        {
            get { return researchDescription; }
        }

        [Space]
        [HorizontalGroup]
        [SerializeField]
        protected int[] researchPrice;
        public int[] ResearchPrice
        {
            get { return researchPrice; }
        }
        [Space]
        [HorizontalGroup]
        [SerializeField]
        protected int[] researchValue;
        public int[] ResearchValue
        {
            get { return researchValue; }
        }

        public virtual void ApplyResearchEffect(PlayerData playerData, int researchLevel)
        {
    
        }

    } // ResearchData class
	
}// #PROJECTNAME# namespace
