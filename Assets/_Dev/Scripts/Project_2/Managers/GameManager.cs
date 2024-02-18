using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Singleton;
using System.Collections;
using Cinemachine;
using GameData;

namespace Project2.General
{
    public class GameManager : NonPersistentSingleton<GameManager>
    {
        #region Variables

        //Game Events
        public bool IsGameStarted;
        public bool IsGameFinished;
        private InputManager.InputListener _listener;

        public PlayerController PlayerController;
        private bool _isTouched;

        [Space(5)]
        [Header("Game Data")]
        public DifficultyData DifficultyData;

        [Space(5)]
        [Header("Levels")]
        public GameObject LevelPrefab;

        /* [Space(5)]
        [Header("Hud Panel")]
        [SerializeField] private GameObject _panelView;
         */
        [Space(5)]
        [Header("Camera")]
        public CameraManager CameraManager;

        

        #endregion

        #region Action
        public static Action OnPlayerTouch;
        public static event Action<bool> OnGameStart;
        public static event Action<bool> OnMovement;

        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            DOTween.KillAll();
            Input.multiTouchEnabled = false;

            OnGameStart = null;
            OnMovement = null;
            OnPlayerTouch = null;
        }

        private void Start()
        {
            _listener = new InputManager.InputListener();
            _listener.TouchEvent += Touch;
            InputManager.Instance.AddListener(_listener);
        }

        #endregion

        #region Methods
        public void LevelEnd(bool isWon)
        {
            if (IsGameFinished) return;
            IsGameFinished = true;
            IsGameStarted = false;

            OnGameStart?.Invoke(false);
            OnMovement?.Invoke(false);

            if (isWon)
            {
                CameraManager.CamState = CameraState.Win;
                LevelManager.Instance.LevelCompleted();
            }
            else
            {
                CameraManager.CamState = CameraState.Death;
                LevelManager.Instance.LevelFailed();
            }
        }

        public void GameStart() // Oyun Başlangıç
        {
            IsGameStarted = true;
            OnGameStart?.Invoke(true);
            OnMovement?.Invoke(true);
        }

        #endregion

        #region Handle Input

        void Touch(bool isDown)
        {
            if (!isDown)
            {
                _isTouched = false;
                return;
            }

            
            if (!IsGameStarted && !IsGameFinished)
            {
                GameStart();
                InGameTouch();
            }
            else
            {
                InGameTouch();
            }
            
        }

        void InGameTouch()
        {
            if (_isTouched) return;
            _isTouched = true;
            OnPlayerTouch?.Invoke();
        }

        public void NullInGameTouch(){
            OnPlayerTouch = null;
        }

        #endregion



    }
}