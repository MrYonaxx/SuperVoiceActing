/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace VoiceActing
{

    public enum EmotionNPC
    {
        Normal,
        Joyeux
    }

    /// <summary>
    /// Definition of the CharacterData class
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterStoryData", menuName = "CharacterStory", order = 1)]
    public class StoryCharacterData : ScriptableObject
    {
        #region Attributes 

        [SerializeField]
        string name;

        [Header("Emotion")]
        [SerializeField]
        private Sprite[] spriteNormal;
        public Sprite[] SpriteNormal
        {
            get { return spriteNormal; }
        }

        [SerializeField]
        Sprite[] spriteJoy = null;

        public string GetName()
        {
            return name;
        }
        
        #endregion

    } // CharacterData class

} // #PROJECTNAME# namespace