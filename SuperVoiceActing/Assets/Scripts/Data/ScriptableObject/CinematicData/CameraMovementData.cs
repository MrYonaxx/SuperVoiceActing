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
    [System.Serializable]
    public class CamDataNode
    {
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 dataPosition;
        public Vector3 DataPosition
        {
            get { return dataPosition; }
        }

        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        private Vector3 dataRotation;
        public Vector3 DataRotation
        {
            get { return dataRotation; }
        }

        // -1 pour ne pas set de position initiale
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [SerializeField]
        [HideLabel]
        [MinValue(1)]
        private int dataTime = 1;
        public int DataTime
        {
            get { return dataTime; }
        }
    }



    [CreateAssetMenu(fileName = "CameraMovementData", menuName = "Cinematic/CameraMovement", order = 1)]
    public class CameraMovementData : ScriptableObject
	{
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */



        [Title("New Initial Positin")]
        [SerializeField]
        private bool changeCamInitialPosition = false;
        public bool ChangeCamInitialPosition
        {
            get { return changeCamInitialPosition; }
        }
        [ShowIf("changeCamInitialPosition")]
        [SerializeField]
        private Vector3 camInitialPosition;
        public Vector3 CamInitialPosition
        {
            get { return camInitialPosition; }
        }
        [ShowIf("changeCamInitialPosition")]
        [SerializeField]
        private Vector3 camInitialRotation;
        public Vector3 CamInitialRotation
        {
            get { return camInitialRotation; }
        }


        [Space]
        [Space]
        [Space]
        [Title("Camera Movement")]
        [SerializeField]
        private bool changeCamMovement = true;
        public bool ChangeCamMovement
        {
            get { return changeCamMovement; }
        }

        [InfoBox("Position                                                  Rotation                                                  Time")]
        [SerializeField]
        private CamDataNode[] camDataNodes;
        public CamDataNode[] CamDataNodes
        {
            get { return camDataNodes; }
        }




        [Space]
        [Space]
        [Space]
        [Title("Text Movement")]
        [SerializeField]
        private bool changeTextMovement = false;
        public bool ChangeTextMovement
        {
            get { return changeTextMovement; }
        }

        [ShowIf("changeTextMovement")]
        [SerializeField]
        private CamDataNode[] textDataNodes;
        public CamDataNode[] TextDataNodes
        {
            get { return textDataNodes; }
        }




        [Space]
        [Space]
        [Space]

        [Title("Orthographic Movement")]
        [SerializeField]
        private bool changeOrthographic = false;
        public bool ChangeOrthographic
        {
            get { return changeOrthographic; }
        }
        [ShowIf("changeOrthographic")]
        [SerializeField]
        private int orthographicStart;
        public int OrthographicStart
        {
            get { return orthographicStart; }
        }
        [ShowIf("changeOrthographic")]
        [SerializeField]
        private int orthographicEnd;
        public int OrthographicEnd
        {
            get { return orthographicEnd; }
        }
        [ShowIf("changeOrthographic")]
        [SerializeField]
        private int timeOrthographic;
        public int TimeOrthographic
        {
            get { return timeOrthographic; }
        }


        #endregion



    } // CameraMovementData class
	
}// #PROJECTNAME# namespace
