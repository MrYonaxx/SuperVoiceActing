/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace VoiceActing
{
    public class SkillMovelistWindow: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        ImageDictionnary emotionDictionary;
        [SerializeField]
        Color colorActive;
        [SerializeField]
        Color colorUnactive;

        [SerializeField]
        Image[] imageEmotion;

        [SerializeField]
        Image outlineSkill;
        [SerializeField]
        TextMeshProUGUI textSkill;


        List<Emotion> emotionSkill = new List<Emotion>();
        List<bool> emotionActive = new List<bool>();

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

     

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public string GetSkillName()
        {
            return textSkill.text;
        }

        public void DrawSkillMove(SkillActorData skill)
        {
            textSkill.text = skill.SkillName;

            emotionSkill.Clear();
            emotionActive.Clear();
            for (int i = 0; i < imageEmotion.Length; i++)
            {
                imageEmotion[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < skill.Movelist.GetLength(); i++)
            {
                int emotionNumber = skill.Movelist.GetEmotion(i);
                if(emotionNumber != 0)
                {
                    for(int j = 0; j < emotionNumber; j++)
                    {
                        emotionSkill.Add((Emotion)i);
                        emotionActive.Add(false);
                        imageEmotion[emotionSkill.Count-1].sprite = emotionDictionary.GetSprite(i);
                        imageEmotion[emotionSkill.Count - 1].gameObject.SetActive(true);
                        imageEmotion[emotionSkill.Count - 1].color = colorUnactive;
                        textSkill.color = colorUnactive;
                        outlineSkill.color = colorUnactive;
                    }
                }
            }
        }

        public bool CheckMove(Emotion[] emotions)
        {
            for(int i = 0; i < emotionActive.Count; i++)
            {
                emotionActive[i] = false;
                imageEmotion[i].color = colorUnactive;
            }

            for(int i = 0; i < emotions.Length; i++)
            {
                for(int j = 0; j < emotionSkill.Count; j++)
                {
                    if(emotions[i] == emotionSkill[j] && emotionActive[j] == false)
                    {
                        emotionActive[j] = true;
                        break;
                    }
                }
            }

            bool active = true;
            for (int i = 0; i < emotionActive.Count; i++)
            {

                if (emotionActive[i] == false)
                {
                    imageEmotion[i].color = colorUnactive;
                    active = false;
                }
                else
                {
                    imageEmotion[i].color = colorActive;
                }

            }

            if(active == true)
            {
                textSkill.color = colorActive;
                outlineSkill.color = colorActive;
            }
            else
            {
                textSkill.color = colorUnactive;
                outlineSkill.color = colorUnactive;
            }
            return active;
        }

        #endregion

    } 

} // #PROJECTNAME# namespace