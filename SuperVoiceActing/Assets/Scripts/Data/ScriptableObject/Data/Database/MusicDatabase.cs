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
    [CreateAssetMenu(fileName = "MusicDatabase", menuName = "Database/MusicDatabase", order = 1)]
    public class MusicDatabase: ScriptableObject
    {
        [AssetList(AutoPopulate = true, Path = "/Sounds/Music")]
        [SerializeField]
        AudioClip[] musicDatabase;

        public AudioClip GetMusicData(string musicName)
        {
            for (int i = 0; i < musicDatabase.Length; i++)
            {
                if (musicDatabase[i].name == musicName)
                {
                    return musicDatabase[i];
                }
            }
            return null;
        }
    } 

} // #PROJECTNAME# namespace