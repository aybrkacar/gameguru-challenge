using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Singleton;
using Cinemachine;
namespace Project1.General
{
    public class GameManager : NonPersistentSingleton<GameManager>
    {
        #region Variables

  
        public GridGenerator GridGenerator;
        
        #endregion

        #region Action

        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            DOTween.KillAll();
            Input.multiTouchEnabled = false;
        }

        private void Start()
        {

        }

        #endregion

    }
}

