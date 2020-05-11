using UnityEngine;

[CreateAssetMenu]
public class WeaponSettings : ScriptableObject
{
    [Tooltip("Bullets per minute")]
    [SerializeField] private float firingRate = 0f;

    public float weaponPower;

    public GameObject bulletPrefab;

    public AudioClip fireSFX;

    public float FiringRate
    {
        get
        {
            return 60f / firingRate;
        }
    }
}

public enum AmmoType
{
    Rifle,
    SMG
}
