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
    [CreateAssetMenu(fileName = "StoryBackground", menuName = "StoryEvent/StoryBackground", order = 1)]
    public class StoryBackgroundData : ScriptableObject
	{

        [SerializeField]
        private string nameBackground;
        public string NameBackground
        {
            get { return nameBackground; }
        }


        [SerializeField]
        private Sprite spriteBackground;
        public Sprite SpriteBackground
        {
            get { return spriteBackground; }
        }

    } // BackgroundData class
	
}// #PROJECTNAME# namespace
