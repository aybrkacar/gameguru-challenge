
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Project2.General
{
    public class CollectableManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject _starPrefab;
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private MovementData _movementData;
        [SerializeField] private PlatformParentController _platformParent;

        [SerializeField] private int _emptyCount;
        [SerializeField] private int _starCount;
        [SerializeField] private int _coinCount;
        [SerializeField] private float _emptyPossibility;
        [SerializeField] private float _starPossibility;
        [SerializeField] private float _coinPossibility;

        private Vector3 _spawnPos;

        #endregion

        #region Mono
        private void Start()
        {
            
            LevelManager.OnLevelStarted += SpawnCollectables;
        }

        private void OnDisable() {
             LevelManager.OnLevelStarted  -= SpawnCollectables;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                CreateCollectables();
            }
        }
        #endregion

        #region Methods

        void SetupCollectable(){
            _starCount = LevelManager.Instance.CurrentLevelData.StarCount;
            _coinCount = LevelManager.Instance.CurrentLevelData.CoinCount;

            var platformCount = LevelManager.Instance.CurrentLevelData.LevelPlatformCount;
            _emptyCount = platformCount - _starCount - _coinCount - 1;

            _spawnPos = _platformParent.BasePlatform.transform.position;

        }
        void SpawnCollectables()
        {
            SetupCollectable();

            var platformCount = LevelManager.Instance.CurrentLevelData.LevelPlatformCount;
            for (int i = 0; i < platformCount; i++)
            {
                CreateCollectables();
            }
        }
        void CreateCollectables()
        {
            CalculatePossibility();
            var randomVal = Random.value;
            if (randomVal <= _starPossibility)
            {
                CreateStar();
            }
            else if (randomVal <= _starPossibility + _coinPossibility)
            {
                CreateCoin();
            }
            else
            {
                Empty();
            }
        }

        void CreateStar()
        {
            _starCount--;
            GameObject star = Instantiate(_starPrefab, CalculateSpawnPos(), Quaternion.identity);
        }

        void CreateCoin()
        {
            _coinCount--;
            GameObject star = Instantiate(_coinPrefab, CalculateSpawnPos(), Quaternion.identity);
        }
    
        void Empty()
        {
            _emptyCount--;
            CalculateSpawnPos();
        }

        void CalculatePossibility()
        {
            var total = _starCount + _coinCount + _emptyCount;
            _emptyPossibility = (float)_emptyCount / (float)total;
            _starPossibility = (float)_starCount / (float)total;
            _coinPossibility = (float)_coinCount / (float)total;
        }
        Vector3 CalculateSpawnPos()
        {
            var platformCount = LevelManager.Instance.CurrentLevelData.LevelPlatformCount;
            var distance = _platformParent.CurrentEndGame.transform.position.z - _platformParent.BasePlatform.transform.position.z;
            Vector3 pos = new Vector3(GetRandomX(), 0.5f, _spawnPos.z + distance / platformCount);
            _spawnPos = pos;
            return pos;
        }

        float GetRandomX(){
            var randomXPos = Random.Range(_movementData.MovementNormalXLimit[0], _movementData.MovementNormalXLimit[1]);
            return randomXPos;
        }
        #endregion
    }
}

