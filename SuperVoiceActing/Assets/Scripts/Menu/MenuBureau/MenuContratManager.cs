/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;

namespace VoiceActing
{
    /// <summary>
    /// Definition of the MenuContratManager class
    /// </summary>
    public class MenuContratManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private ContractData[] contractDatabase;

        [SerializeField]
        private ContractData[] contractAvailableList;

        [SerializeField]
        private ContractData[] contractCurrentList;


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

        // Créer un Contract depuis ContractData
        public void CreateContract()
        {

        }

        public void DrawAvailableContract()
        {

        }

        #endregion

    } // MenuContratManager class

} // #PROJECTNAME# namespace