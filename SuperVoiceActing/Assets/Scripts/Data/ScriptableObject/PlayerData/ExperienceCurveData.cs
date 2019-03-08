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
    [CreateAssetMenu(fileName = "ExperienceCurveData", menuName = "ExperienceCurveData", order = 1)]
    public class ExperienceCurveData : ScriptableObject
	{

        [OnValueChanged("CalculateExperienceCurve")]
        [SerializeField]
        private int baseStat = 100;

        [OnValueChanged("CalculateExperienceCurve")]
        [SerializeField]
        private float multiplier = 1.2f;

        [ReadOnly]
        [SerializeField]
        private int[] experienceCurve = new int[255];
        public int[] ExperienceCurve
        {
            get { return experienceCurve; }
        }

        private void CalculateExperienceCurve()
        {
            experienceCurve[0] = baseStat;
            for (int i = 1; i < experienceCurve.Length; i++)
            {
                experienceCurve[i] = (int) (experienceCurve[i-1] * multiplier);
            }
        }



    } // ExperienceCurveData class
	
}// #PROJECTNAME# namespace
