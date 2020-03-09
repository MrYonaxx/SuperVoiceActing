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
    /// 
    [System.Serializable]
    public class StoryCharacterData
    {
        #region Attributes 

        [SerializeField]
        private AudioClip characterVoice;
        public AudioClip CharacterVoice
        {
            get { return characterVoice; }
        }

        [HorizontalGroup("VoiceReel")]
        [SerializeField]
        string voiceReel;
        public string VoiceReel
        {
            get { return voiceReel; }
        }

        [HorizontalGroup("VoiceReel", Width = 100)]
        [HideLabel]
        [SerializeField]
        Emotion emotionVoiceReel;
        public Emotion EmotionVoiceReel
        {
            get { return emotionVoiceReel; }
        }

        [Space]
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
        
        #endregion

    } // CharacterData class

} // #PROJECTNAME# namespace