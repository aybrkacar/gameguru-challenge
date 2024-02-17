using UnityEngine;

[CreateAssetMenu(fileName = "Movement", menuName = "MovementData", order = 0)]
public class MovementData : ScriptableObject
{
    [SerializeField] private float _movementSpeed;

    [Space(10)]
    [Header("Clamp")]
    [SerializeField] private float[] _movementNormalXlimit;

    // Property
    public float MovementSpeed => _movementSpeed;
    public float[] MovementNormalXLimit => _movementNormalXlimit;
}
