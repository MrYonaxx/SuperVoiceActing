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
    [CreateAssetMenu(fileName = "SeiyuuActionDatabase", menuName = "SeiyuuDensetsu/SeiyuuAction/SeiyuuActionDatabase", order = 1)]
    public class SeiyuuActionDatabase: ScriptableObject
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [TabGroup("Action")]
        [SerializeField]
        private SeiyuuAction[] seiyuuActions;
        public SeiyuuAction[] SeiyuuActions
        {
            get { return seiyuuActions; }
            set { seiyuuActions = value; }
        }

        [TabGroup("Work")]
        [SerializeField]
        private SeiyuuAction[] seiyuuActionsWork;
        public SeiyuuAction[] SeiyuuActionsWork
        {
            get { return seiyuuActionsWork; }
            set { seiyuuActionsWork = value; }
        }

        [TabGroup("Study")]
        [SerializeField]
        private SeiyuuAction[] seiyuuActionsStudy;
        public SeiyuuAction[] SeiyuuActionsStudy
        {
            get { return seiyuuActionsStudy; }
            set { seiyuuActionsStudy = value; }
        }
        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */

        public SeiyuuAction[] GetSeiyuuActions(int tab)
        {
            switch(tab)
            {
                case 0:
                    return seiyuuActions;
                case 1:
                    return seiyuuActionsWork;
                case 2:
                    return seiyuuActionsStudy;
            }
            return null;
        }

        #endregion

    } 

} // #PROJECTNAME# namespace