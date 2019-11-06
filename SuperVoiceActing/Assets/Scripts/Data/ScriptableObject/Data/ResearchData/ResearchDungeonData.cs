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
using UnityEngine.Tilemaps;

namespace VoiceActing
{

    [System.Serializable]
    public struct TileDictionnary
    {
        [HorizontalGroup]
        public int tileID;
        [HorizontalGroup]
        public Tile tile;

    }

    [System.Serializable]
    public struct ResearchEvent
    {
        [HorizontalGroup]
        public int researchEventID;
        [HorizontalGroup]
        [HideLabel]
        public ResearchData researchData;
    }

    [CreateAssetMenu(fileName = "ResearchDungeon", menuName = "Research/ResearchDungeon", order = 1)]
    public class ResearchDungeonData: SerializedScriptableObject
    {

        [SerializeField]
        private Vector2Int startPosition;
        public Vector2Int StartPosition
        {
            get { return startPosition; }
        }

        [SerializeField]
        [TableMatrix]
        private int[,] researchDungeonLayout;
        public int[,] ResearchDungeonLayout
        {
            get { return researchDungeonLayout; }
        }

        [Space]
        [SerializeField]
        private TileDictionnary[] tileDictionnaries;
        public TileDictionnary[] TileDictionnaries
        {
            get { return tileDictionnaries; }
        }


        [Space]
        [Space]
        [Title("Events")]
        [SerializeField]
        [TableMatrix]
        private int[,] researchEventLayout;
        public int[,] ResearchEventLayout
        {
            get { return researchEventLayout; }
        }

        [Space]
        [SerializeField]
        private ResearchEvent[] researchEventsDictionnary;
        public ResearchEvent[] ResearchEventsDictionnary
        {
            get { return researchEventsDictionnary; }
        }






        public void CreateResearchDungeonLayout(int boundX, int boundY)
        {
            researchDungeonLayout = new int[boundX, boundY];
        }

        public void CreateResearchDungeonLayout(RPGMakerMVMapData mapData)
        {
            researchDungeonLayout = new int[mapData.width, mapData.height];

            for (int y = 0; y < mapData.height; y++)
            {
                for (int x = 0; x < mapData.width; x++)
                {
                    researchDungeonLayout[x, y] = mapData.data[mapData.width * y + x];
                }
            }
        }



        public Tile GetCorrespondingTile(int x, int y)
        {
            int id = researchDungeonLayout[x, y];
            for (int i = 0; i < tileDictionnaries.Length; i++)
            {
                if (id == tileDictionnaries[i].tileID)
                    return tileDictionnaries[i].tile;
            }
            return null;
        }

        public Tile GetCorrespondingTile(int id)
        {
            for (int i = 0; i < tileDictionnaries.Length; i++)
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


    } 

} // #PROJECTNAME# namespace