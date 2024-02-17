using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

[CreateAssetMenu(fileName ="DifficultyData" , menuName ="Scriptables/DifficultyData")]
public class DifficultyData : ScriptableObject
{
   [SerializeField] private float _toleranceDistance;

   public float ToleranceDistance => _toleranceDistance;
}
