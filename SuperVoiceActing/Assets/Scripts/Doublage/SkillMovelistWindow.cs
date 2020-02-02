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
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace VoiceActing
{
    public class SkillMovelistWindow: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

        int skillID = 0;

        [SerializeField]
        UnityEventInt eventClickEnter;
        [SerializeField]
        UnityEventInt eventClickExit;

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

        public void DrawSkillName(string name, int id)
        {
            textSkill.text = name;

            emotionSkill.Clear();
            emotionActive.Clear();
            for (int i = 0; i < imageEmotion.Length; i++)
            {
                imageEmotion[i].gameObject.SetActive(false);
            }
            textSkill.color = colorUnactive;
            outlineSkill.color = colorUnactive;
            skillID = id;
        }


        public void DrawSkillMove(SkillActorData skill, int id)
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
            skillID = id;
        }

        public void DrawSkillMove(SkillRoleData skill, int id)
        {
            textSkill.text = skill.SkillName;

            emotionSkill.Clear();
            emotionActive.Clear();
            for (int i = 0; i < imageEmotion.Length; i++)
            {
                imageEmotion[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < skill.CounterSkill.GetLength(); i++)
            {
                int emotionNumber = skill.CounterSkill.GetEmotion(i);
                if (emotionNumber != 0)
                {
                    for (int j = 0; j < emotionNumber; j++)
                    {
                        emotionSkill.Add((Emotion)i);
                        emotionActive.Add(false);
                        imageEmotion[emotionSkill.Count - 1].sprite = emotionDictionary.GetSprite(i);
                        imageEmotion[emotionSkill.Count - 1].gameObject.SetActive(true);
                        imageEmotion[emotionSkill.Count - 1].color = colorUnactive;
                        textSkill.color = colorUnactive;
                        outlineSkill.color = colorUnactive;
                    }
                }
            }
            skillID = id;
        }

        public void ResetSkill()
        {
            textSkill.color = colorUnactive;
            outlineSkill.color = colorUnactive;
            for(int i = 0; i < emotionActive.Count; i++)
            {
                imageEmotion[i].color = colorUnactive;
                emotionActive[i] = false;
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


        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            eventClickEnter.Invoke(skillID);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            eventClickExit.Invoke(skillID);
        }

        /*public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
                eventOnClick.Invoke(buttonIndex);
        }*/

        #endregion

    } 

} // #PROJECTNAME# namespace