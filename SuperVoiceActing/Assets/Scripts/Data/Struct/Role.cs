﻿/*****************************************************************
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
        private int attack;
        public int Attack
        {
            get { return attack; }
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

        [SerializeField]
        private int bestStat;
        public int BestStat
        {
            get { return bestStat; }
        }

        [SerializeField]
        private int secondBestStat;
        public int SecondBestStat
        {
            get { return secondBestStat; }
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

            this.attack = Random.Range(data.AtkMin, data.AtkMax);

            //!\ à tester 
            this.timbre = new Vector2Int(Random.Range(data.Timbre.x, data.TimbreRand.x), Random.Range(data.Timbre.y, data.TimbreRand.y));
            //!\ à tester 

            this.characterStat = new EmotionStat
                                 (
                                    Random.Range(data.CharacterStatMin.Joy, data.CharacterStatMax.Joy),
                                    Random.Range(data.CharacterStatMin.Sadness, data.CharacterStatMax.Sadness),
                                    Random.Range(data.CharacterStatMin.Disgust, data.CharacterStatMax.Disgust),
                                    Random.Range(data.CharacterStatMin.Anger, data.CharacterStatMax.Anger),
                                    Random.Range(data.CharacterStatMin.Surprise, data.CharacterStatMax.Surprise),
                                    Random.Range(data.CharacterStatMin.Sweetness, data.CharacterStatMax.Sweetness),
                                    Random.Range(data.CharacterStatMin.Fear, data.CharacterStatMax.Fear),
                                    Random.Range(data.CharacterStatMin.Trust, data.CharacterStatMax.Trust)
                                 );



            int[] bestValues = new int[8];

            bestValues[0] = characterStat.Joy;
            bestValues[1] = characterStat.Sadness;
            bestValues[2] = characterStat.Disgust;
            bestValues[3] = characterStat.Anger;
            bestValues[4] = characterStat.Surprise;
            bestValues[5] = characterStat.Sweetness;
            bestValues[6] = characterStat.Fear;
            bestValues[7] = characterStat.Trust;

            bestStat = 0;
            secondBestStat = 0;

            for (int i = 0; i < bestValues.Length; i++)
            {
                if (bestValues[i] > bestStat)
                {
                    secondBestStat = bestStat;
                    bestStat = bestValues[i];
                }
                else if (bestValues[i] > secondBestStat)
                {
                    secondBestStat = bestValues[i];
                }
            }


        }

        #endregion

    } // Role class
	
}// #PROJECTNAME# namespace
