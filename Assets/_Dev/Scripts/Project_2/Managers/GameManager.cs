using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using _Reusable.Singleton;
using System.Collections;
using Cinemachine;

namespace General
{
    public class GameManager : NonPersistentSingleton<GameManager>
    {
        #region Variables

        //Game Events
        public bool IsGameStarted;
        public bool IsGameFinished;
        private InputManager.InputListener _listener;

        public PlayerController PlayerController;
        private bool isTouched;

        [Space(5)]
        [Header("Hud Panel")]
        [SerializeField] private GameObject _panelView;

        [Space(5)]
        [Header("Camera")]
        public CinemachineVirtualCamera MainCamera;

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

            }
            else
            {

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
                isTouched = false;
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
            isTouched = true;
        }

        void InGameTouch()
        {
            if (isTouched) return;
            OnPlayerTouch?.Invoke();
        }

        public void NullInGameTouch(){
            OnPlayerTouch = null;
        }

        #endregion



    }
}