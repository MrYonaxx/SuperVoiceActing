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

    [System.Serializable]
    public struct TileDictionnary
    {
        public int tileID;
        public Tile tile;



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

        [Space]
        [Space]
        [Space]

        public Tilemap tilemap;

        public TileDictionnary[] tileDictionnaries;



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
            if (File.Exists(jsonPath))
            {
                string dataAsJson = File.ReadAllText(jsonPath);
                JsonUtility.FromJsonOverwrite(dataAsJson, mvMapData);
            }
            researchDungeon = new ResearchDungeonLayout(mvMapData);
        }


        [Button("Render the dungeon on the grid")]
        private void RenderDungeonTilemap()
        {
            for (int x = 0; x < researchDungeon.dungeonLayout.GetLength(0); x++)
            {
                for (int y = 0; y < researchDungeon.dungeonLayout.GetLength(1); y++)
                {
                    if(researchDungeon.dungeonLayout[x,y] != 0)
                        tilemap.SetTile(new Vector3Int(x, y, 0), GetCorrespondingTile(researchDungeon.dungeonLayout[x, y]));
                    else
                        tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }


        [Button("Test")]
        private void GenerateDungeonFromTilemap()
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            researchDungeon.dungeonLayout = new int[bounds.size.x, bounds.size.y];
            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    if (tile != null)
                    {
                        researchDungeon.dungeonLayout[x, y] = GetCorrespondingID(tile.name);
                    }
                    else
                    {
                        researchDungeon.dungeonLayout[x, y] = 0;
                    }
                }
            }
        }



        public Tile GetCorrespondingTile(int id)
        {
            for(int i = 0; i < tileDictionnaries.Length; i++)
            {
                if (id == tileDictionnaries[i].tileID)
                    return tileDictionnaries[i].tile;
            }
            return null;
        }

        public int GetCorrespondingID(string tile)
        {
            for (int i = 0; i < tileDictionnaries.Length; i++)
            {
                if (tile == tileDictionnaries[i].tile.name)
                    return tileDictionnaries[i].tileID;
            }
            return 0;
        }

        #endregion

    } 

} // #PROJECTNAME# namespace