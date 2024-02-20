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
        public static event Action OnMatched;
        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            DOTween.KillAll();
            Input.multiTouchEnabled = false;

            OnMatched = null;
        }
        
        public void OnMatching(){
            OnMatched?.Invoke();
        }

        #endregion

    }
}

