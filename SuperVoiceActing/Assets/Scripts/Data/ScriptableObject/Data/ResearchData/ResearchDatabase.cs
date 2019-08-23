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
    [System.Serializable]
    public class ResearchPlayerLevel
    {
        [SerializeField]
        private int[] researchLevel;
        public int[] ResearchLevel
        {
            get { return researchLevel; }
            set { researchLevel = value; }
        }

        public ResearchPlayerLevel(int size)
        {
            researchLevel = new int[size];
        }
    }


    [System.Serializable]
    public class ResearchDatabaseCategory
    {
        [SerializeField]
        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
        }

        [SerializeField]
        private ResearchData[] researches;
        public ResearchData[] Researches
        {
            get { return researches; }
        }
    }

    [CreateAssetMenu(fileName = "ResearchDatabase", menuName = "Research/ResearchDatabase", order = 1)]
    public class ResearchDatabase : ScriptableObject
	{
        [SerializeField]
        private ResearchDatabaseCategory[] researchesData;
        public ResearchDatabaseCategory[] ResearchesData
        {
            get { return researchesData; }
        }

    } // ResearchDatabase class
	
}// #PROJECTNAME# namespace
