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
        protected string actionName;
        public string ActionName
        {
            get { return actionName; }
        }

        [SerializeField]
        protected int cost;
        public int Cost
        {
            get { return cost; }
        }

        public virtual int ApplyDay(SeiyuuData seiyuuData)
        {
            return 0;
        }

        public virtual void ApplyWeek(SeiyuuData seiyuuData)
        {

        }
    } 

} // #PROJECTNAME# namespace
