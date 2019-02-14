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
	public class Role
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header("CharacterData")]

        [SerializeField]
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField]
        private int line;
        public int Line
        {
            get { return line; }
        }

        [SerializeField]
        private int fan;
        public int Fan
        {
            get { return fan; }
        }

        [SerializeField]
        private Vector2Int timbre;
        public Vector2Int Timbre
        {
            get { return timbre; }
            set { timbre = value; }
        }

        [SerializeField]
        private EmotionStat characterStat;
        public EmotionStat CharacterStat
        {
            get { return characterStat; }
            set { characterStat = value; }
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

        public Role(RoleContractData data)
        {
            this.name = data.Names[Random.Range(0, data.Names.Length)];
            this.line = Random.Range(data.LineMin, data.LineMax);
            this.fan = Random.Range(data.FanMin, data.FanMax);

            //!\ à tester 
            this.timbre = new Vector2Int(Random.Range(data.Timbre.x, data.TimbreRand.x), Random.Range(data.Timbre.y, data.TimbreRand.y));
            //!\ à tester 


        }

        #endregion

    } // Role class
	
}// #PROJECTNAME# namespace
