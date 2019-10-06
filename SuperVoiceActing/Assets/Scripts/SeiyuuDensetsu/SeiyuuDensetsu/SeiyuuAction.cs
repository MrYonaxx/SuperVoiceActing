/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    //[CreateAssetMenu("SeiyuuDensetsu/Seiyuu")]
    public abstract class SeiyuuAction : ScriptableObject
    {
        [SerializeField]
        private string actionName;
        public string ActionName
        {
            get { return actionName; }
        }

        [SerializeField]
        private int cost;
        public int Cost
        {
            get { return cost; }
        }
    } 

} // #PROJECTNAME# namespace
