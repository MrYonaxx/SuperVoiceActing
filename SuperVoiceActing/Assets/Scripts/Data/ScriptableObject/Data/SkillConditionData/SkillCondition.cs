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
    public abstract class SkillCondition
    {

        public virtual bool CheckCondition(DoublageBattleParameter battleParameter)
        {
            return true;
        }

    } 

} // #PROJECTNAME# namespace