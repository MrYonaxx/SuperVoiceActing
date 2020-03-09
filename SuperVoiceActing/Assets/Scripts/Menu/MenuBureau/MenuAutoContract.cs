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
    public class MenuAutoContract: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        PlayerData playerData;


        [Title("Parameter Actor Work")]
        [SerializeField]
        float expWorkMultiplier = 0.75f;

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

        public void AutoBattle(Contract c, List<VoiceActor> voiceActors)
        {
            int turn = playerData.TurnLimit;
            int indexCurrentCharacter = 0;
            for (int i = 0; i < c.TextData.Count; i++)
            {
                indexCurrentCharacter = c.TextData[i].Interlocuteur;
                CalculateBattle(c.TextData[i], voiceActors[indexCurrentCharacter], c.Characters[indexCurrentCharacter]);
            }

        }

        public void CalculateBattle(TextData textLine, VoiceActor va, Role r)
        {
            List<int> emotionUsable = new List<int>();

            int bestStat = 0;
            int currentStat = 0;
            for (int i = 1; i < textLine.EnemyResistance.GetLength(); i++)
            {
                currentStat = textLine.EnemyResistance.GetEmotion(i);

                if (currentStat > bestStat)
                    bestStat = currentStat;

                if(currentStat >= 50)
                    emotionUsable.Add(i);
            }

            if (emotionUsable.Count == 0)
                emotionUsable.Add(Random.Range(1, textLine.EnemyResistance.GetLength()));

            int selectEmotion = emotionUsable[Random.Range(0, emotionUsable.Count - 1)];
            int hp = textLine.HPMax;
            int damage = 0;
            while(hp > 0)
            {
                damage = (va.Statistique.GetEmotion(selectEmotion) * playerData.ComboMax) * textLine.EnemyResistance.GetEmotion(selectEmotion);
                va.Hp -= (r.Attack * playerData.ComboMax);
                hp -= damage;
            }
            r.RolePerformance += textLine.EnemyResistance.GetEmotion(selectEmotion) + damage;
            r.RoleBestScore += bestStat;
                
        }







        public void AutoContract(Contract c, List<VoiceActor> voiceActors)
        {
            int indexCurrentCharacter = 0;
            for (int i = 0; i < c.TextData.Count; i++)
            {
                indexCurrentCharacter = c.TextData[i].Interlocuteur;
                CalculateLine(c.TextData[i], voiceActors[indexCurrentCharacter], c.Characters[indexCurrentCharacter]);
            }


            for (int i = 0; i < voiceActors.Count; i++)
            {
                voiceActors[i].GainExp((int)(((c.ExpBonus + (c.ExpGain * c.TotalLine)) / voiceActors.Count) * expWorkMultiplier));
                voiceActors[i].CreateVoxography(c.Name);
            }
            for (int i = 0; i < c.Characters.Count; i++)
            {
                voiceActors[i].AddVoxography(c.Characters[i], (Emotion)c.Characters[i].BestStatEmotion);
            }

        }

        public void CalculateLine(TextData textLine, VoiceActor va, Role r)
        {
            List<int> emotionUsable = new List<int>();

            int bestStat = 0;
            int currentStat = 0;
            for (int i = 1; i < textLine.EnemyResistance.GetLength(); i++)
            {
                currentStat = textLine.EnemyResistance.GetEmotion(i);

                if (currentStat > bestStat)
                    bestStat = currentStat;

                if (currentStat >= 50)
                    emotionUsable.Add(i);
            }

            if (emotionUsable.Count == 0)
                emotionUsable.Add(Random.Range(1, textLine.EnemyResistance.GetLength()));

            int selectEmotion = emotionUsable[Random.Range(0, emotionUsable.Count - 1)];
            int damage = (int)((va.Statistique.GetEmotion(selectEmotion) * playerData.ComboMax) * ((100 + textLine.EnemyResistance.GetEmotion(selectEmotion)) / 100f));
            va.Hp -= r.Attack;
            r.RolePerformance += textLine.EnemyResistance.GetEmotion(selectEmotion) + (damage / 10);
            r.RoleBestScore += bestStat;
        }


        #endregion

    } 

} // #PROJECTNAME# namespace