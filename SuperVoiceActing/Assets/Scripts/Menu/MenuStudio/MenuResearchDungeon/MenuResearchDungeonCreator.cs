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





    public class MenuResearchDungeonCreator: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [Title("Rpg maker MV")]
        [FilePath]
        public string jsonPath;
        [SerializeField]
        public RPGMakerMVMapData mvMapData;

        [Space]
        [Space]
        [Title("Data")]
        [SerializeField]
        public ResearchDungeonData researchDungeon;

        [Space]
        [Space]
        [Title("Renderer")]

        public Tilemap tilemap;


        [Title("Function")]

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
        [Button("Generate the dungeon from a rpg maker mv file")]
        private void GenerateDungeon()
        {
            if (File.Exists(jsonPath))
            {
                string dataAsJson = File.ReadAllText(jsonPath);
                JsonUtility.FromJsonOverwrite(dataAsJson, mvMapData);
            }
            researchDungeon.CreateResearchDungeonLayout(mvMapData);
        }


        [Button("Generate the dungeon from the tilemap")]
        private void GenerateDungeonFromTilemap()
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            researchDungeon.CreateResearchDungeonLayout(bounds.size.x, bounds.size.y);
            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    if (tile != null)
                    {
                        researchDungeon.ResearchDungeonLayout[x, y] = researchDungeon.GetCorrespondingID(tile.name);
                    }
                    else
                    {
                        researchDungeon.ResearchDungeonLayout[x, y] = 0;
                    }
                }
            }
        }


        [Button("Render the dungeon on the grid")]
        private void RenderDungeonTilemap()
        {
            tilemap.ClearAllTiles();
            for (int x = 0; x < researchDungeon.ResearchDungeonLayout.GetLength(0); x++)
            {
                for (int y = 0; y < researchDungeon.ResearchDungeonLayout.GetLength(1); y++)
                {
                    if (researchDungeon.ResearchDungeonLayout[x, y] != 0)
                        tilemap.SetTile(new Vector3Int(x, y, 0), researchDungeon.GetCorrespondingTile(x, y));
                    else
                        tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }

        #endregion

    }

} // #PROJECTNAME# namespace