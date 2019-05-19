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
        public int Time
        {
            get { return time; }
        }

        [ShowIf("eventEffect", EventEffect.Tint)]
        [SerializeField]
        private int timeTint = 60;
        public int TimeTint
        {
            get { return timeTint; }
        }

        [ShowIf("eventEffect", EventEffect.Tint)]
        [SerializeField]
        private Color tintColor;
        public Color TintColor
        {
            get { return tintColor; }
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
                case EventEffect.FadeTotal:
                    storyEventEffectManager.Fade(true, time);
                    break;
                case EventEffect.FadeOut:
                    storyEventEffectManager.Fade(false, time);
                    break;
                case EventEffect.Tint:
                    storyEventEffectManager.Tint(tintColor, timeTint);
                    break;

            }
            yield return null;
        }

    } // StoryEventEffect class
	
}// #PROJECTNAME# namespace
