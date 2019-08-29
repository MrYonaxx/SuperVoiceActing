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
    [CreateAssetMenu(fileName = "SkillSE", menuName = "Skills/SkillSoundEngiData", order = 1)]
    public class SkillDataSoundEngi : SkillData
	{
        [Space]
        [Title("Sound Engineer")]
        [SerializeField]
        private bool cardSelection = false;
        public bool CardSelection
        {
            get { return cardSelection; }
        }

        [ShowIf("cardSelection")]
        [SerializeField]
        private bool packSelection = false;
        public bool PackSelection
        {
            get { return packSelection; }
        }

        [ShowIf("cardSelection")]
        [SerializeField]
        [MinMaxSlider(1,3)]
        private Vector2Int cardComboSize;
        public Vector2Int CardComboSize
        {
            get { return cardComboSize; }
        }


    } // SkillSoundEngi class
	
}// #PROJECTNAME# namespace
