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

        public virtual void ApplySkillEffect(DoublageBattleParameter doublageBattleParameter)
        {

        }


        // Remove les effets généraux lié aux acteurs/phrases
        public virtual void RemoveSkillEffect(DoublageBattleParameter doublageBattleParameter)
        {

        }



        // Manual target pour les effets de cartes
        /*public virtual void ManualTarget(Emotion[] emotion, bool selectionByPack)
        {

        }*/


        public virtual void PreviewTarget()
        {

        }

        public virtual void StopPreview()
        {
            /*EmotionCardTotal[] cards = doublageManager.EmotionAttackManager.GetCards();
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards[i].Cards.Length; j++)
                {
                    if(cards[i].Cards[j] != null)
                        cards[i].Cards[j].StopPreview();
                }
            }*/
        }

    } // SkillEffectData class
	
}// #PROJECTNAME# namespace
