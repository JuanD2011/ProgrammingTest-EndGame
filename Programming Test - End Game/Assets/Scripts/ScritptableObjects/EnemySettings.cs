using UnityEngine;

[CreateAssetMenu]
public class EnemySettings : ScriptableObject
{
    [Header("Movement")]
    public float shootDistance;
    public float maxPursuitDistance;
    public float guardWalkRadius;
    public float guardTime;
    public float pursuitSpeed;
    public float guardSpeed;
}
