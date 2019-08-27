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

        public virtual void ApplySkillEffect(DoublageManager doublageManager, BuffData buffData = null)
        {

        }


        // Remove les effets généraux lié aux acteurs/phrases
        /*public virtual void RemoveSkillEffect(SkillTarget skillTarget, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {

        }*/

        // Remove les effets lié aux actors
        public virtual void RemoveSkillEffectActor(VoiceActor card)
        {

        }

        // Remove les effets lié aux cartes
        public virtual void RemoveSkillEffectCard(EmotionCard card)
        {

        }


        // Manual target pour les effets de cartes
        public virtual void ManualTarget(Emotion emotion)
        {

        }

    } // SkillEffectData class
	
}// #PROJECTNAME# namespace
