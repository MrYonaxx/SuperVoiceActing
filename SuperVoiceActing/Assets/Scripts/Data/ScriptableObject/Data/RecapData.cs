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
    [CreateAssetMenu(fileName = "RecapData", menuName = "RecapData/RecapData", order = 1)]
    public class RecapData : ScriptableObject
	{
        [SerializeField]
        string recapTitle;
        public string RecapTitle
        {
            get { return recapTitle; }
            set { recapTitle = value; }
        }



        [SerializeField]
        [TextArea(10,20)]
        string recapContent;
        public string RecapContent
        {
            get { return recapContent; }
            set { recapContent = value; }
        }

    } // RecapData class
	
}// #PROJECTNAME# namespace
