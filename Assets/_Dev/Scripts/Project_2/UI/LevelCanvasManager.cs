using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameData;
using Project2.General;
using TMPro;
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
        public List<RectTransform> UIStarImageList;
        public List<RectTransform> CameraStarImageList;
        public int StarIndex = 0;
        public TextMeshProUGUI LevelText;
        public string LevelTextPrefix;

        public Transform MoneyUITransform;
        public TextMeshProUGUI MoneyText;


        private WaitForSeconds canvasDelay;

        #endregion

        #region Mono
        void Start()
        {
            LevelManager.OnLevelStarted += HideAllCanvases;
            LevelManager.OnLevelCompleted += DisplayWinCanvas;
            LevelManager.OnLevelFailed += DisplayFailCanvas;
            LevelManager.OnLevelStarted += UpdateLevelText;
            LevelManager.OnLevelStarted += ResetStars;
            LevelManager.OnLevelStarted += UpdateMoney;

            DynamicCanvas.SetActive(true);
            canvasDelay = new WaitForSeconds(EndGameCanvasDelay);
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStarted -= HideAllCanvases;
            LevelManager.OnLevelCompleted -= DisplayWinCanvas;
            LevelManager.OnLevelFailed -= DisplayFailCanvas;
            LevelManager.OnLevelStarted -= ResetStars;
            LevelManager.OnLevelStarted -= UpdateMoney;
        }
        #endregion

        #region Methods
        void MoveDynamicObjects()
        {
            MovingObject.DOAnchorPosY(-110f, 0.7f);
        }

        public void FillStar()
        {
            if (StarIndex >= CameraStarImageList.Count) return;

            RectTransform rectTransformCamera = CameraStarImageList[StarIndex];
            RectTransform rectTransformUI = UIStarImageList[StarIndex];

            var uiStarImageTransform = rectTransformUI.GetChild(0);
            uiStarImageTransform.gameObject.SetActive(true);
            uiStarImageTransform.DOScale(1f, 0.4f);

            var cameraStarImageTransform = rectTransformCamera.GetChild(0);
            cameraStarImageTransform.gameObject.SetActive(true);
            cameraStarImageTransform.DOScale(1f, 0.4f);
            StarIndex++;
        }

        public Transform GetCurrentStarTransform()
        {
            return CameraStarImageList[StarIndex];
        }

        public void ResetStars()
        {
            CameraStarImageList.ForEach(rects => rects.GetChild(0).gameObject.SetActive(false));
            UIStarImageList.ForEach(rects => rects.GetChild(0).gameObject.SetActive(false));
        }

        void MoveDynamicObjectsDefault()
        {
            MovingObject.DOAnchorPosY(0f, 0.7f);
        }


        private IEnumerator Victory()
        {
            yield return canvasDelay;

            WinCanvas.SetActive(true);
            TouchCanvas.SetActive(false);
            MoveDynamicObjects();
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

            MoveDynamicObjectsDefault();
        }

        public void UpdateLevelText()
        {
            LevelText.text = $"{LevelTextPrefix} {SaveData.Level + 1}";
        }

        public void UpdateMoney()
        {
            ScaleMoney();
            MoneyText.text = SaveData.MoneyValue.ToString("F0");
        }

        void ScaleMoney()
        {
            if (!DOTween.IsTweening(MoneyUITransform))
            {
                MoneyUITransform.DOPunchScale(Vector3.one * 0.1f, 0.3f);
            }
        }


        #endregion
    }
}

