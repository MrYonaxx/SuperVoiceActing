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

        [Title("Position")]
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 camStart;
        public Vector3 CamStart
        {
            get { return camStart; }
        }

        [Title("Rotation")]
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 camStartRotation;
        public Vector3 CamStartRotation
        {
            get { return camStartRotation; }
        }

        [Title("Time")]
        // -1 pour ne pas set de position initiale
        [HorizontalGroup("Mouvement", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private int timeStart;
        public int TimeStart
        {
            get { return timeStart; }
        }








        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd;
        public Vector3 CameraEnd
        {
            get { return cameraEnd; }
        }

        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEndRotation;
        public Vector3 CameraEndRotation
        {
            get { return cameraEndRotation; }
        }

        [HorizontalGroup("MouvementFin", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private int timeEnd;
        public int TimeEnd
        {
            get { return timeEnd; }
        }






        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd2;
        public Vector3 CameraEnd2
        {
            get { return cameraEnd2; }
        }

        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 cameraEnd2Rotation;
        public Vector3 CameraEnd2Rotation
        {
            get { return cameraEnd2Rotation; }
        }

        [HorizontalGroup("MouvementFin2", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeCamMovement")]
        [SerializeField]
        [HideLabel]
        private int timeEnd2;
        public int TimeEnd2
        {
            get { return timeEnd2; }
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

        [Title("Position")]
        [HorizontalGroup("TextMovement", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 textStart;
        public Vector3 TextStart
        {
            get { return textStart; }
        }
        [Title("Rotation")]
        [HorizontalGroup("TextMovement", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 textStartRotation;
        public Vector3 TextStartRotation
        {
            get { return textStartRotation; }
        }
        [Title("Time")]
        // -1 pour ne pas set de position initiale
        [HorizontalGroup("TextMovement", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private int textTimeStart;
        public int TextTimeStart
        {
            get { return textTimeStart; }
        }








        [HorizontalGroup("TextMovementEnd", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 textEnd;
        public Vector3 TextEnd
        {
            get { return textEnd; }
        }

        [HorizontalGroup("TextMovementEnd", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 textEndRotation;
        public Vector3 TextEndRotation
        {
            get { return textEndRotation; }
        }

        [HorizontalGroup("TextMovementEnd", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private int textTimeEnd;
        public int TextTimeEnd
        {
            get { return textTimeEnd; }
        }









        [HorizontalGroup("TextMovementEnd2", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 textEnd2;
        public Vector3 TextEnd2
        {
            get { return textEnd2; }
        }

        [HorizontalGroup("TextMovementEnd2", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private Vector3 textEnd2Rotation;
        public Vector3 TextEnd2Rotation
        {
            get { return textEnd2Rotation; }
        }

        [HorizontalGroup("TextMovementEnd2", PaddingRight = 50, Width = 0.33f)]
        [ShowIf("changeTextMovement")]
        [SerializeField]
        [HideLabel]
        private int textTimeEnd2;
        public int TextTimeEnd2
        {
            get { return textTimeEnd2; }
        }








        #endregion



    } // CameraMovementData class
	
}// #PROJECTNAME# namespace
