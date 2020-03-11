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
    [CreateAssetMenu(fileName = "VoiceActorDatabase", menuName = "Database/VoiceActorDatabase", order = 1)]
    public class VoiceActorDatabase : ScriptableObject
    {

        [AssetList(AutoPopulate = true, Path = "/ScriptableObject/VoiceActor")]
        [SerializeField]
        private List<VoiceActorData> voiceActorsDatabase;
        public List<VoiceActorData> VoiceActorsDatabase
        {
            get { return voiceActorsDatabase; }
        }

        public VoiceActorData GetVoiceActorData(string characterName)
        {
            for (int i = 0; i < voiceActorsDatabase.Count; i++)
            {
                if (voiceActorsDatabase[i].NameID == characterName)
                {
                    return voiceActorsDatabase[i];
                }
            }
            return null;
        }

        public StoryCharacterData GetCharacterData(string characterName)
        {
            for (int i = 0; i < voiceActorsDatabase.Count; i++)
            {
                if (voiceActorsDatabase[i].NameID == characterName)
                {
                    return voiceActorsDatabase[i].SpriteSheets;
                }
            }
            return null;
        }

    } 

} // #PROJECTNAME# namespace