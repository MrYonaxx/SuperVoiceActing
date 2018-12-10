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



    [System.Serializable]
    public class EmotionStat
    {
        [SerializeField]
        private int joy;
        public int Joy
        {
            get { return joy; }
            set { joy = value; }
        }

        [SerializeField]
        private int sadness;
        public int Sadness
        {
            get { return sadness; }
            set { sadness = value; }
        }

        [SerializeField]
        private int disgust;
        public int Disgust
        {
            get { return disgust; }
            set { disgust = value; }
        }

        [SerializeField]
        private int anger;
        public int Anger
        {
            get { return anger; }
            set { anger = value; }
        }

        [SerializeField]
        private int surprise;
        public int Surprise
        {
            get { return surprise; }
            set { surprise = value; }
        }

        [SerializeField]
        private int sweetness;
        public int Sweetness
        {
            get { return sweetness; }
            set { sweetness = value; }
        }

        [SerializeField]
        private int fear;
        public int Fear
        {
            get { return fear; }
            set { fear = value; }
        }


        [SerializeField]
        private int trust;
        public int Trust
        {
            get { return trust; }
            set { trust = value; }
        }

    }


    [System.Serializable]
    public class WeakPoint
    {
        [SerializeField]
        private int wordIndex;
        public int WordIndex
        {
            get { return wordIndex; }
            set { wordIndex = value; }
        }
        [SerializeField]
        private EmotionStat weakPointStat;
        public EmotionStat WeakPointStat
        {
            get { return weakPointStat; }
            set { weakPointStat = value; }
        }

    }




    /// <summary>
    /// Definition of the EnemyManager class
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        string enemyText;

        [SerializeField]
        int enemyHP = 100;

        [SerializeField]
        EmotionStat enemyResistance;

        [SerializeField]
        WeakPoint enemyWeakPoint;

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



        #endregion

    } // EnemyManager class

} // #PROJECTNAME# namespace