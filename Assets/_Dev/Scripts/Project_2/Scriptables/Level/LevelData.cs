using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="LevelData", menuName ="Scriptables/LevelData")]
public class LevelData : ScriptableObject
{
   [SerializeField] private int _levelIndex;
   [SerializeField] private int _levelPlatformCount;
   [SerializeField] private int _starCount;
   [SerializeField] private int _coinCount;

   public int LevelIndex => _levelIndex;
   public int LevelPlatformCount => _levelPlatformCount;
   public int StarCount => _starCount;
   public int CoinCount => _coinCount;
}
