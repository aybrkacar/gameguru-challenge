using System;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

namespace General
{
    public class InputManager : NonPersistentSingleton<InputManager>
    {
        #region Variable
        [SerializeField] private Camera _orthoCam;
        private Vector3 _firstPos, _lastPos, _diff;
        private bool _isTouched = false;
        private List<InputListener> _listeners = new List<InputListener>();

        // Field
        public float DiffX => _diff.x;
        public bool IsTouched => _isTouched;
        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();
        }

        void Update()
        {
            if (_isTouched)
            {
                _lastPos = _orthoCam.ScreenToWorldPoint(Input.mousePosition);
                _diff = _lastPos - _firstPos;
                _firstPos = _orthoCam.ScreenToWorldPoint(Input.mousePosition);
            }

            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].UpdateInput(_isTouched, _diff.x);
            }
        }

        #endregion

        #region Func

        public void AddListener(InputListener listener)
        {
            _listeners.Add(listener);
        }

        #endregion

        #region Input

        public void TouchDown()
        {
            _isTouched = true;
            _firstPos = _orthoCam.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].UpdateTouchInput(_isTouched);
            }

        }

        public void TouchUp()
        {
            _isTouched = false;
            _diff = Vector3.zero;

            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].UpdateTouchInput(_isTouched);
            }
        }

        #endregion

        #region ListenerClass

        public class InputListener
        {
            public bool IsTouch { get; private set; }
            public float HorizontalAmount { get; private set; }

            public event Action<bool> TouchEvent;

            public void UpdateInput(bool isTouch, float horizontalAmount)
            {
                IsTouch = isTouch;
                HorizontalAmount = horizontalAmount;
            }

            public void UpdateTouchInput(bool isDown)
            {
                TouchEvent?.Invoke(isDown);
            }
        }


        #endregion
    }
}
