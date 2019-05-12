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
    [System.Serializable]
	public class StoryEventEffect : StoryEvent
	{

        [SerializeField]
        EventEffect eventEffect;
        public EventEffect EventEffect
        {
            get { return eventEffect; }
        }









        StoryEventEffectManager storyEventEffectManager;

        public void SetNode(StoryEventEffectManager effects)
        {
            storyEventEffectManager = effects;
        }

        protected override IEnumerator StoryEventCoroutine()
        {
            switch(eventEffect)
            {
                case EventEffect.Flash:
                    storyEventEffectManager.FlashEffect();
                    break;
                case EventEffect.Shake:
                    storyEventEffectManager.ShakeEffect();
                    break;
                case EventEffect.BigShake:
                    storyEventEffectManager.BigShakeEffect();
                    break;
                case EventEffect.ShakePlayer:
                    storyEventEffectManager.ShakePlayerEffect();
                    break;

            }
            yield return null;
        }

    } // StoryEventEffect class
	
}// #PROJECTNAME# namespace
