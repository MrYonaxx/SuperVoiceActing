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
	public class EffectManager : MonoBehaviour
	{

        [SerializeField]
        GameObject negativeEffect;

        [SerializeField]
        GameObject blurredEffect;


        public void StartEffect(DoublageEventEffect effectData)
        {
            switch(effectData.EventEffect)
            {
                case EventEffect.Negative:
                    negativeEffect.SetActive(effectData.Active);
                    break;
                case EventEffect.BlurredVision:
                    blurredEffect.SetActive(effectData.Active);
                    break;
            }
        }

		
	} // EffectManager class
	
}// #PROJECTNAME# namespace
