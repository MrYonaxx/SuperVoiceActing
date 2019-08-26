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
        Joyeux,
        Triste,
        Colere,
        Degout,
        Surprise,
        Douceur,
        Peur,
        Confiance,
        Special
    }

    /// <summary>
    /// Definition of the CharacterData class
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterStoryData", menuName = "CharacterStory", order = 1)]
    public class StoryCharacterData : ScriptableObject
    {
        #region Attributes 

        [SerializeField]
        string characterName;

        [SerializeField]
        private AudioClip characterVoice;
        public AudioClip CharacterVoice
        {
            get { return characterVoice; }
        }

        [SerializeField]
        private Sprite spriteIcon;
        public Sprite SpriteIcon
        {
            get { return spriteIcon; }
        }

        [SerializeField]
        private Sprite spriteEye;
        public Sprite SpriteEye
        {
            get { return spriteEye; }
        }

        [Header("Emotion")]
        [SerializeField]
        private Sprite[] spriteNormal;
        public Sprite[] SpriteNormal
        {
            get { return spriteNormal; }
        }

        [SerializeField]
        private Sprite[] spriteJoy;
        public Sprite[] SpriteJoy
        {
            get { return spriteJoy; }
        }

        [SerializeField]
        private Sprite[] spriteSpecial;
        public Sprite[] SpriteSpecial
        {
            get { return spriteSpecial; }
        }

        public string GetName()
        {
            return characterName;
        }
        
        #endregion

    } // CharacterData class

} // #PROJECTNAME# namespace