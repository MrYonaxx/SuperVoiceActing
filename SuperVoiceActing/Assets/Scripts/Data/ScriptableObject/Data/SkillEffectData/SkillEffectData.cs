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
	public class SkillEffectData
	{

        protected List<Vector3Int> cardTargetsData = new List<Vector3Int>();
        protected bool targetAcquired = false;

        public virtual void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {

        }


        // Remove les effets généraux lié aux acteurs/phrases
        public virtual void RemoveSkillEffect(DoublageManager doublageManager)
        {

        }

        // Remove les effets lié aux actors
        public virtual void RemoveSkillEffectActor(VoiceActor card)
        {

        }

        // Remove les effets lié aux cartes
        public virtual void RemoveSkillEffectCard(EmotionCard card)
        {

        }


        // Manual target pour les effets de cartes
        public virtual void ManualTarget(Emotion[] emotion, bool selectionByPack)
        {

        }

        // Manual target pour les effets de cartes
        public virtual void PreviewTarget(DoublageManager doublageManager)
        {

        }

        // Manual target pour les effets de cartes
        public virtual void StopPreview(DoublageManager doublageManager)
        {
            EmotionCardTotal[] cards = doublageManager.EmotionAttackManager.GetCards();
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards[i].Cards.Length; j++)
                {
                    if(cards[i].Cards[j] != null)
                        cards[i].Cards[j].StopPreview();
                }
            }
        }

    } // SkillEffectData class
	
}// #PROJECTNAME# namespace
