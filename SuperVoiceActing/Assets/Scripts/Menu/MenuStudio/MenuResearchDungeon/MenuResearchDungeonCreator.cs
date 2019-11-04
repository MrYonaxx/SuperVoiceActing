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
using System.IO;
using UnityEngine.Tilemaps;

namespace VoiceActing
{
    [System.Serializable]
    public class RPGMakerMVMapData
    {
        [SerializeField]
        public int width;

        [SerializeField]
        public int height;

        [SerializeField]
        public int[] data;
    }

    [System.Serializable]
    public class ResearchDungeonLayout
    {
        [SerializeField]
        public Vector2Int startPosition;

        [ShowInInspector]
        [BoxGroup("Labled table")]
        [TableMatrix(IsReadOnly = true)]
        public int[,] dungeonLayout = new int[1,1];

        public ResearchDungeonLayout(RPGMakerMVMapData mapData)
        {
            dungeonLayout = new int[mapData.width, mapData.height];

            for(int y = 0; y < mapData.height; y++ )
            {
                for(int x = 0; x < mapData.width; x++)
                {
                    dungeonLayout[x, y] = mapData.data[mapData.width * y + x];
                }
            }
        }
    }


    public class MenuResearchDungeonCreator: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [FilePath]
        public string jsonPath;

        [SerializeField]
        public RPGMakerMVMapData mvMapData;

        [Space]
        [Space]
        [Space]
        [SerializeField]
        public ResearchDungeonLayout researchDungeon;

        [SerializeField]
        public Grid grid;

        public Tilemap tilemaps;


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
        [Button("Generate the dungeon")]
        private void GenerateDungeon()
        {
            //string filePath = string.Format("{0}/saves/{1}{2}.json", Application.persistentDataPath, saveFileName, index);
            //Debug.Log(filePath);
            if (File.Exists(jsonPath))
            {
                string dataAsJson = File.ReadAllText(jsonPath);
                JsonUtility.FromJsonOverwrite(dataAsJson, mvMapData);
            }
            researchDungeon = new ResearchDungeonLayout(mvMapData);
            /*TileBase tile;
            tile.name;
            tilemaps.SetTile
            tilemaps.GetTile*/
        }

        #endregion

    } 

} // #PROJECTNAME# namespace