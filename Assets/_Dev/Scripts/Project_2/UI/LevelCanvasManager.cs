using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project2.General;
using UnityEngine;

namespace Project2.UI
{
    public class LevelCanvasManager : MonoBehaviour
    {
        #region Variables
        [Header("Canvases")]
        public GameObject TouchCanvas;
        public GameObject DynamicCanvas;
        public GameObject WinCanvas;
        public GameObject FailCanvas;

        [Space(6)]
        [Header("Win Screen Delays")]
        public float EndGameCanvasDelay;

        [Space(6)]
        [Header("UI")]
        public RectTransform MovingObject;
        public RectTransform StarsParent;


        private WaitForSeconds canvasDelay;

        #endregion

        #region Mono
        void Start()
        {
            LevelManager.OnLevelStarted += HideAllCanvases;
            LevelManager.OnLevelCompleted += DisplayWinCanvas;
            LevelManager.OnLevelFailed += DisplayFailCanvas;

            DynamicCanvas.SetActive(true);
            canvasDelay = new WaitForSeconds(EndGameCanvasDelay);
        }

        private void OnDestroy()
        {
             LevelManager.OnLevelStarted -= HideAllCanvases;
            LevelManager.OnLevelCompleted -= DisplayWinCanvas;
            LevelManager.OnLevelFailed -= DisplayFailCanvas;
        }
        #endregion

        #region Methods
        void MoveDynamicObjects()
        {
            MovingObject.DOAnchorPosY(-110f, 0.7f);
        }

        void MoveStars()
        {
            StarsParent.DOAnchorPosY(130f, 0.7f);
            StarsParent.DOScale(1f, 0.7f);
        }

        private IEnumerator Victory()
        {
            yield return canvasDelay;

            WinCanvas.SetActive(true);
            TouchCanvas.SetActive(false);
            MoveDynamicObjects();
            MoveStars();
        }

        private IEnumerator Fail()
        {
            yield return canvasDelay;

            FailCanvas.SetActive(true);
            DynamicCanvas.SetActive(false);
            TouchCanvas.SetActive(false);
        }

        void DisplayWinCanvas()
        {
            StartCoroutine(Victory());
        }

        void DisplayFailCanvas()
        {
            StartCoroutine(Fail());
        }

        void HideAllCanvases()
        {
            WinCanvas.SetActive(false);
            FailCanvas.SetActive(false);

            DynamicCanvas.SetActive(true);
            TouchCanvas.SetActive(true);
        }


        #endregion
    }
}

