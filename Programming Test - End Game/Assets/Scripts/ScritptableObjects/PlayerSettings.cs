using UnityEngine;

[CreateAssetMenu]
public class PlayerSettings : ScriptableObject
{
    [Header("Player Movement")]
    [Header("Speed")]
    public float maxNormalSpeed = 0f;
    public float maxBoostedSpeed = 0f;
    public float timetoMaxSpeedMovement = 0f;

    [Header("Rotation")]
    public float timeToMaxSpeedRotation = 0f;
}
