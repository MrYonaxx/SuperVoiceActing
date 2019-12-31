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
    [System.Serializable]
    public class SkillEffectCardGain : SkillEffectData
    {
        [SerializeField]
        [HideLabel]
        EmotionStat cardGain;

        public override void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {
            //doublageManager.ModifyDeck(cardGain);
        }

        /*public override void PreviewTarget(DoublageManager doublageManager)
        {
            base.PreviewTarget(doublageManager);
            int gain = 0;
            EmotionCardTotal[] cards = doublageManager.EmotionAttackManager.GetCards();

            for (int i = 0; i < 8; i++)
            {
                gain = cardGain.GetEmotion(i + 1);
                if(gain < 0)
                {
                    for (int j = 0; j < Mathf.Abs(gain); j++)
                    {
                        if (cards[i].Cards[j] != null)
                            cards[i + 1].Cards[j].DrawPreviewText("Destruction");
                    }
                }
            }
        }*/

    } // SkillEffectCardGain class
	
}// #PROJECTNAME# namespace
