using UnityEngine;

[CreateAssetMenu]
public class AmmoSettings : ScriptableSingleton<AmmoSettings>
{
    public float lifeTime;

    [Header("Damage")]
    public float rifleAmmoDamage;
    public float smgAmmoDamage;
}
