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

        public virtual void ApplySkillEffect(SkillTarget skillTarget, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager, BuffData buffData = null)
        {

        }

        /*public virtual void ApplyBuffEffect(BuffData buffData, SkillTarget skillTarget, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {
            ApplySkillEffect(skillTarget, actorsManager, enemyManager, doublageManager);

        }*/


        public virtual void RemoveSkillEffect(SkillData skill, ActorsManager actorsManager, EnemyManager enemyManager, DoublageManager doublageManager)
        {

        }

        public virtual void RemoveSkillEffectCard(EmotionCard card)
        {

        }

    } // SkillEffectData class
	
}// #PROJECTNAME# namespace
