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
    // Tout les gimmick à la con
    public class BattleGimmick
    {
        public float turnMinusValue;
    }

    // Tout les parametres utilisés par un combat réuni dans une seule classe que SkillData peut manipuler
    public class DoublageBattleParameter
    {
        public float Turn { get; set; }
        public int IndexCurrentCharacter { get; set; }
        public int KillCount { get; set; }

        public Contract Contract { get; set; }
        public List<VoiceActor> VoiceActors { get; set; }
        public SoundEngineer SoundEngi { get; set; }

        public EmotionCardTotal[] Cards { get; set; }

        public Emotion[] LastAttackEmotion { get; set; }
        public Emotion[] CurrentAttackEmotion { get; set; }


        public BattleGimmick BattleGimmick { get; set; }


        public ActorsManager ActorsManager { get; set; }
        public EnemyManager EnemyManager { get; set; }
        public TurnManager TurnManager { get; set; }


        public DoublageBattleParameter(PlayerData playerData, EmotionCardTotal[] cards)
        {
            Contract = playerData.CurrentContract;
            VoiceActors = playerData.GetVoiceActorsFromContractID(Contract);
            SoundEngi = playerData.GetSoundEngiFromName(Contract.SoundEngineerID);

            Turn = playerData.TurnLimit;
            IndexCurrentCharacter = Contract.TextData[Contract.CurrentLine].Interlocuteur;
            KillCount = 0;

            Cards = cards;
            //CurrentCombo = combo;
        }

        public void SetManagers(ActorsManager aM, EnemyManager eM, TurnManager tM)
        {
            ActorsManager = aM;
            EnemyManager = eM;
            TurnManager = tM;
        }


        public VoiceActor CurrentActor()
        {
            return VoiceActors[IndexCurrentCharacter];
        }

        public Role CurrentRole()
        {
            return Contract.Characters[IndexCurrentCharacter];
        }

        /*public Emotion[] GetEmotion()
        {
            if (CurrentCombo == null)
                return null;
            var emotion = new Emotion[CurrentCombo.Length];
            for (int i = 0; i < CurrentCombo.Length; i++)
            {
                emotion[i] = CurrentCombo[i].GetEmotion();
            }
            return emotion;
        }*/
    } 

} // #PROJECTNAME# namespace