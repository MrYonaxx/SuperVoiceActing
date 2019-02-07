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
    /// Definition of the VoiceActor class
    /// A VoiceActor is created from a VoiceActorData
    /// VoiceActorData contains all possibilities for a VoiceActor
    /// A VoiceActor object is a possibility of VoiceActorData
    /// </summary>
    public class VoiceActor
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [Header(" Informations générales")]
        [SerializeField]
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField]
        private int fan;
        public int Fan
        {
            get { return fan; }
            set { fan = value; }
        }




        [Header(" Statistiques")]
        [SerializeField]
        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [SerializeField]
        private int hp;
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        [SerializeField]
        private int hpMax;
        public int HpMax
        {
            get { return hpMax; }
            set { hpMax = value; }
        }

        [SerializeField]
        private EmotionStat statistique;
        public EmotionStat Statistique
        {
            get { return statistique; }
            set { statistique = value; }
        }

        [SerializeField]
        private int experience;
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        [SerializeField]
        private int relation;
        public int Relation
        {
            get { return relation; }
            set { relation = value; }
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

        public VoiceActor()
        {

        }

        public VoiceActor(VoiceActorData actorData)
        {
            name = actorData.Name;
            level = actorData.Level;
        }

        #endregion

    } // VoiceActor class

} // #PROJECTNAME# namespace