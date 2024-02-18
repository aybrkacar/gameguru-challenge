using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="LevelData", menuName ="Scriptables/LevelData")]
public class LevelData : ScriptableObject
{
   [SerializeField] private int _levelIndex;
   [SerializeField] private int _levelPlatformCount;

   public int LevelIndex => _levelIndex;
   public int LevelPlatformCount => _levelPlatformCount;
}
