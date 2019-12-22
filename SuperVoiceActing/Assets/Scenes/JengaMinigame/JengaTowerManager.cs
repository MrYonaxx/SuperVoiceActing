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
    public class JengaTowerManager: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        JengaPiece jengaPiecePrefab;

        [SerializeField]
        int towerStartHeight = 18;

        [SerializeField]
        float defaultHeight = 1.25f;
        [SerializeField]
        float pieceHeight = 1.5f;
        [SerializeField]
        float pieceWidth = 2.5f;

        List<JengaPiece> jengaPieces = new List<JengaPiece>();

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
        private void Start()
        {
            InitializeTower();
        }

        private void InitializeTower()
        {
            float y = defaultHeight;
            for (int i = 0; i < towerStartHeight; i++)
            {
                if (i % 2 == 0)
                {
                    jengaPieces.Add(Instantiate(jengaPiecePrefab, new Vector3(0, y, -pieceWidth), Quaternion.Euler(0f, 0f, 0f)));
                    jengaPieces.Add(Instantiate(jengaPiecePrefab, new Vector3(0, y, 0), Quaternion.Euler(0f, 0f, 0f)));
                    jengaPieces.Add(Instantiate(jengaPiecePrefab, new Vector3(0, y, pieceWidth), Quaternion.Euler(0f, 0f, 0f)));
                }
                else
                {
                    jengaPieces.Add(Instantiate(jengaPiecePrefab, new Vector3(-pieceWidth, y, 0), Quaternion.Euler(0f, 90f, 0f)));
                    jengaPieces.Add(Instantiate(jengaPiecePrefab, new Vector3(0, y, 0), Quaternion.Euler(0f, 90f, 0f)));
                    jengaPieces.Add(Instantiate(jengaPiecePrefab, new Vector3(pieceWidth, y, 0), Quaternion.Euler(0f, 90f, 0f)));
                }
                y += pieceHeight;
            }
        }

        #endregion

    } 

} // #PROJECTNAME# namespace