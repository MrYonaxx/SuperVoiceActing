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
	public class StoryEventEffect : StoryEvent
	{

        [SerializeField]
        EventEffect eventEffect;
        public EventEffect EventEffect
        {
            get { return eventEffect; }
        }

        [ShowIf("eventEffect", EventEffect.FadeTotal)]
        [SerializeField]
        private int time = 60;

        [ShowIf("eventEffect", EventEffect.Tint)]
        [SerializeField]
        private int timeTint = 60;

        [ShowIf("eventEffect", EventEffect.Tint)]
        [SerializeField]
        private Color tintColor;


        public override bool InstantNodeCoroutine()
        {
            return true;
        }

        public override IEnumerator ExecuteNodeCoroutine(StoryEventManager storyManager)
        {
            switch (eventEffect)
            {
                case EventEffect.Flash:
                    storyManager.StoryEventEffectManager.FlashEffect();
                    break;
                case EventEffect.Shake:
                    storyManager.StoryEventEffectManager.ShakeEffect();
                    break;
                case EventEffect.BigShake:
                    storyManager.StoryEventEffectManager.BigShakeEffect();
                    break;
                case EventEffect.ShakePlayer:
                    storyManager.StoryEventEffectManager.ShakePlayerEffect();
                    break;
                case EventEffect.FadeTotal:
                    storyManager.StoryEventEffectManager.Fade(true, time);
                    break;
                case EventEffect.FadeOut:
                    storyManager.StoryEventEffectManager.Fade(false, time);
                    break;
                case EventEffect.Tint:
                    storyManager.StoryEventEffectManager.Tint(tintColor, timeTint);
                    break;

            }
            yield return null;
        }


    } // StoryEventEffect class
	
}// #PROJECTNAME# namespace
