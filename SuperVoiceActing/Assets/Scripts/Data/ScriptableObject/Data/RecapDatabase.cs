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
    [CreateAssetMenu(fileName = "RecapData", menuName = "RecapData/RecapDatabase", order = 1)]
    public class RecapDatabase : ScriptableObject
    {

        [SerializeField]
        private RecapData[] recapDatas;
        public RecapData[] RecapDatas
        {
            get { return recapDatas; }
            set { recapDatas = value; }
        }


    } // RecapDatabase class
	
}// #PROJECTNAME# namespace
