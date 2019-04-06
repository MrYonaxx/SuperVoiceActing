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
    [CreateAssetMenu(fileName = "SkillActorData", menuName = "Skills/SkillActorData", order = 1)]
    public class SkillActorData : SkillData
	{



        [Header("Conditions d'Activation")]

        [SerializeField]
        private bool afterAttack;
        public bool AfterAttack
        {
            get { return afterAttack; }
        }

        [SerializeField]
        private bool afterCritical;
        public bool AfterCritical
        {
            get { return afterCritical; }
        }

        [SerializeField]
        private bool afterKill;
        public bool AfterKill
        {
            get { return afterKill; }
        }

        [SerializeField]
        private EmotionStat phraseType;
        public EmotionStat PhraseType
        {
            get { return phraseType; }
        }

        [MinMaxSlider(0, 100)]
        [SerializeField]
        private Vector2Int hpInterval = new Vector2Int(0, 100);
        public Vector2Int HpInterval
        {
            get { return hpInterval; }
        }

        [SerializeField]
        private float percentageActivation;
        public float PercentageActivation
        {
            get { return percentageActivation; }
        }


    } // SkillActorData class
	
}// #PROJECTNAME# namespace
