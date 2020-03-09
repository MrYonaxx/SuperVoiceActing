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
    public struct EmotionStat
    {
        [VerticalGroup("EmotionStat", PaddingBottom = 10)]
        [Title("Neutr", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion",Width = 50)]
        [SerializeField]
        private int neutral;
        public int Neutral
        {
            get { return neutral; }
            set { neutral = value; }
        }

        [Title("Joie", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int joy;
        public int Joy
        {
            get { return joy; }
            set { joy = value; }
        }

        [Title("Triste", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int sadness;
        public int Sadness
        {
            get { return sadness; }
            set { sadness = value; }
        }

        [Title("Dégo", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int disgust;
        public int Disgust
        {
            get { return disgust; }
            set { disgust = value; }
        }

        [Title("Colèr", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int anger;
        public int Anger
        {
            get { return anger; }
            set { anger = value; }
        }

        [Title("Surpr", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int surprise;
        public int Surprise
        {
            get { return surprise; }
            set { surprise = value; }
        }

        [Title("Douce", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int sweetness;
        public int Sweetness
        {
            get { return sweetness; }
            set { sweetness = value; }
        }

        [Title("Peur", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int fear;
        public int Fear
        {
            get { return fear; }
            set { fear = value; }
        }

        [Title("Confi", " ", TitleAlignments.Centered)]
        [HideLabel]
        [HorizontalGroup("EmotionStat/Emotion", PaddingLeft = 10, Width = 50)]
        [SerializeField]
        private int trust;
        public int Trust
        {
            get { return trust; }
            set { trust = value; }
        }

        public EmotionStat(string a = "")
        {
            this.joy = 0;
            this.sadness = 0;
            this.disgust = 0;
            this.anger = 0;
            this.surprise = 0;
            this.sweetness = 0;
            this.fear = 0;
            this.trust = 0;
            this.neutral = 0;
        }

        public EmotionStat(int jo, int sa, int di, int an, int su, int sw, int fe, int tr)
        {
            this.joy = jo;
            this.sadness = sa;
            this.disgust = di;
            this.anger = an;
            this.surprise = su;
            this.sweetness = sw;
            this.fear = fe;
            this.trust = tr;
            this.neutral = (jo + sa + di + an + su + sw + fe + tr) / 8;
        }

        public EmotionStat(EmotionStat newStat)
        {
            this.joy = newStat.Joy;
            this.sadness = newStat.Sadness;
            this.disgust = newStat.Disgust;
            this.anger = newStat.Anger;
            this.surprise = newStat.Surprise;
            this.sweetness = newStat.Sweetness;
            this.fear = newStat.Fear;
            this.trust = newStat.Trust;
            this.neutral = (joy + sadness + disgust + anger + surprise + sweetness + fear + trust) / 8;
        }

        public void Add(EmotionStat addition)
        {
            this.joy += addition.joy;
            this.sadness += addition.sadness;
            this.disgust += addition.disgust;
            this.anger += addition.anger;
            this.surprise += addition.surprise;
            this.sweetness += addition.sweetness;
            this.fear += addition.fear;
            this.trust += addition.trust;
            this.neutral += addition.neutral;
        }

        public void Add(int emotion, int addValue)
        {
            switch (emotion)
            {
                case 0: // Neutr
                    neutral += addValue;
                    break;
                case 1: // Joie
                    joy += addValue;
                    break;
                case 2: // Tristesse
                    sadness += addValue;
                    break;
                case 3: // Degout
                    disgust += addValue;
                    break;
                case 4: // Anger
                    anger += addValue;
                    break;
                case 5: // Surprise
                    surprise += addValue;
                    break;
                case 6: // Douceur
                    sweetness += addValue;
                    break;
                case 7: // Peur
                    fear += addValue;
                    break;
                case 8: // confiance
                    trust += addValue;
                    break;
            }
        }

        public void Clamp(int minValue, int maxValue)
        {
            this.joy = Mathf.Clamp(this.joy, minValue, maxValue);
            this.sadness = Mathf.Clamp(this.sadness, minValue, maxValue);
            this.disgust = Mathf.Clamp(this.disgust, minValue, maxValue);
            this.anger = Mathf.Clamp(this.anger, minValue, maxValue);
            this.surprise = Mathf.Clamp(this.surprise, minValue, maxValue);
            this.sweetness = Mathf.Clamp(this.sweetness, minValue, maxValue);
            this.fear = Mathf.Clamp(this.fear, minValue, maxValue);
            this.trust = Mathf.Clamp(this.trust, minValue, maxValue);
            this.neutral = Mathf.Clamp(this.neutral, minValue, maxValue);
        }

        public void Substract(EmotionStat addition)
        {
            this.joy -= addition.joy;
            this.sadness -= addition.sadness;
            this.disgust -= addition.disgust;
            this.anger -= addition.anger;
            this.surprise -= addition.surprise;
            this.sweetness -= addition.sweetness;
            this.fear -= addition.fear;
            this.trust -= addition.trust;
            this.neutral -= addition.neutral;
        }

        public EmotionStat Reverse()
        {
            return new EmotionStat(-this.joy, -this.sadness, -this.disgust, -this.anger, -this.surprise, -this.sweetness, -this.fear, -this.trust);
        }

        public void Multiply(EmotionStat multiplication)
        {
            this.joy *= multiplication.joy;
            this.sadness *= multiplication.sadness;
            this.disgust *= multiplication.disgust;
            this.anger *= multiplication.anger;
            this.surprise *= multiplication.surprise;
            this.sweetness *= multiplication.sweetness;
            this.fear *= multiplication.fear;
            this.trust *= multiplication.trust;
            this.neutral *= multiplication.neutral;
        }
        public void Multiply(float multiplication)
        {
            this.joy = (int)(joy * multiplication);
            this.sadness = (int)(sadness * multiplication);
            this.disgust = (int)(disgust * multiplication);
            this.anger = (int)(anger * multiplication);
            this.surprise = (int)(surprise * multiplication);
            this.sweetness = (int)(sweetness * multiplication);
            this.fear = (int)(fear * multiplication);
            this.trust = (int)(trust * multiplication);
            this.neutral = (int)(neutral * multiplication);
        }

        public int GetEmotion(int emotion)
        {
            switch (emotion)
            {
                case 0: // Neutr
                    return neutral;
                case 1: // Joie
                    return joy;
                case 2: // Tristesse
                    return sadness;
                case 3: // Degout
                    return disgust;
                case 4: // Anger
                    return anger;
                case 5: // Surprise
                    return surprise;
                case 6: // Douceur
                    return sweetness;
                case 7: // Peur
                    return fear;
                case 8: // confiance
                    return trust;
            }
            return 0;
        }

        public int SetValue(int emotion, int newValue)
        {
            switch (emotion)
            {
                case 0: // Neutr
                    neutral = newValue;
                    break;
                case 1: // Joie
                    joy = newValue;
                    break;
                case 2: // Tristesse
                    sadness = newValue;
                    break;
                case 3: // Degout
                    disgust = newValue;
                    break;
                case 4: // Anger
                    anger = newValue;
                    break;
                case 5: // Surprise
                    surprise = newValue;
                    break;
                case 6: // Douceur
                    sweetness = newValue;
                    break;
                case 7: // Peur
                    fear = newValue;
                    break;
                case 8: // confiance
                    trust = newValue;
                    break;
            }
            return 0;
        }

        public int GetLength()
        {
            return 9;
        }

    }

} // #PROJECTNAME# namespace