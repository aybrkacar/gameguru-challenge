using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEngine;
using GameData;
using System;
using UnityEngine.SceneManagement;

namespace Project2.General
{
    public class LevelManager : NonPersistentSingleton<LevelManager>
    {
        #region Variables
        public bool TestMode;
        public LevelData TestData;
        public LevelData[] AllLevelDatas;
        public LevelData[] RepeatLevelDatas;
        public LevelData CurrentLevelData;

        //Actions
        public static event Action OnLevelStarted;
        public static event Action OnLevelCompleted;
        public static event Action OnLevelFailed;
        #endregion

        #region Mono
        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Start()
        {
            LevelStarted();
        }
        #endregion

        #region Methods
        void Initialize()
        {
            SetupLevelIndex();
        }

        void SetupLevelIndex()
        {
            var currentLevelIndex = SaveData.Level;
            if (TestMode)
            {
                CurrentLevelData = TestData;
            }
            else
            {
                CurrentLevelData = currentLevelIndex < AllLevelDatas.Length ? AllLevelDatas[currentLevelIndex] : RepeatLevelDatas[(currentLevelIndex - AllLevelDatas.Length) % RepeatLevelDatas.Length];
            }
        }

        public void LevelStarted()
        {
            OnLevelStarted?.Invoke();
        }

        public void LevelCompleted()
        {
            OnLevelCompleted?.Invoke();
        }

        public void LevelFailed()
        {
            OnLevelFailed?.Invoke();
        }

        public void LoadNextLevel()
        {
            SaveData.Level++;
        }

        public void RestartLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        #endregion
    }
}

