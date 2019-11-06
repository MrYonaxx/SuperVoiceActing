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
    public class MenuResearchMovementManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        ResearchDungeonData dungeonLayout;
        [SerializeField]
        int[,] explorationLayout;

        [SerializeField]
        Tilemap explorationTilemap;
        [SerializeField]
        Tile tileUnexplored;

        [SerializeField]
        PlayerData playerData;
        [SerializeField]
        InputController inputController;
        [SerializeField]
        Vector2Int playerPosition;

        [Title("Movement")]
        [SerializeField]
        float time = 1f;
        [SerializeField]
        float cellSize = 0.48f;
        [SerializeField]
        Transform character;

        private IEnumerator moveCoroutine;

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

        /*[Button("Test")]
        private void CreateExplorationLayout()
        {
            explorationLayout.dungeonLayout = new int[debugLayout.dungeonLayout.GetLength(0), debugLayout.dungeonLayout.GetLength(1)];
            for (int x = 0; x < debugLayout.dungeonLayout.GetLength(0); x++)
            {
                for (int y = 0; y < debugLayout.dungeonLayout.GetLength(1); y++)
                {
                    if (debugLayout.dungeonLayout[x, y] != 0)
                        explorationLayout.dungeonLayout[x, y] = 1;
                    else
                        explorationLayout.dungeonLayout[x, y] = 0;
                }
            }
        }

        [Button("Test2")]
        private void RenderExplorationLayout()
        {
            for (int x = 0; x < explorationLayout.dungeonLayout.GetLength(0); x++)
            {
                for (int y = 0; y < explorationLayout.dungeonLayout.GetLength(1); y++)
                {
                    if (explorationLayout.dungeonLayout[x, y] > 0)
                        explorationTilemap.SetTile(new Vector3Int(x, y, 0), tileUnexplored);
                }
            }
        }*/


        public void Start()
        {
            playerPosition = dungeonLayout.StartPosition;
            SetCharacter(playerPosition);
        }

        public void SetCharacter(Vector2Int position)
        {
            character.transform.localPosition = new Vector3(cellSize * position.x, cellSize * position.y, 0);
            character.transform.localPosition += new Vector3(cellSize / 2f, cellSize / 2f, 0f);
        }

        public void Move(int direction)
        {
            if (moveCoroutine != null)
                return;
            switch(direction)
            {
                case 2:
                    Move(new Vector3(0, 0.48f, 0));
                    break;
                case 4:
                    Move(new Vector3(-0.48f, 0, 0));
                    break;
                case 6:
                    Move(new Vector3(0.48f, 0, 0));
                    break;
                case 8:
                    Move(new Vector3(0, -0.48f, 0));
                    break;

            }

        }

        public void Move(Vector3 direction)
        {
            // Check si prochaine case est traversable
            Vector2Int directionNormalized = new Vector2Int((int)direction.normalized.x, (int)direction.normalized.y);
            if(dungeonLayout.ResearchDungeonLayout[playerPosition.x + directionNormalized.x, playerPosition.y + directionNormalized.y] != 0)
            {
                moveCoroutine = MoveCoroutine(direction);
                StartCoroutine(moveCoroutine);
                playerPosition += new Vector2Int(directionNormalized.x, directionNormalized.y);

                explorationTilemap.SetTile(new Vector3Int(playerPosition.x, playerPosition.y, 0), null);
                //explorationLayout.dungeonLayout[playerPosition.x, playerPosition.y] = -1;
            }
            else
            {
                // Wall
            }



            // Deplace la position du perso dans le player data

            // Ajoute la case decouverte au pourcentage
        }

        private IEnumerator MoveCoroutine(Vector3 destination)
        {
            Vector3 finalPosition = character.transform.localPosition + destination;
            Vector3 position = character.transform.localPosition;
            float t = 0f;
            while(t<1f)
            {
                t += Time.deltaTime * (time / 60f);
                character.transform.localPosition = Vector3.Lerp(position, finalPosition, t);
                yield return null;
            }
            moveCoroutine = null;
        }


        #endregion

    } 

} // #PROJECTNAME# namespace