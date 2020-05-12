using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStats stats = null;

    public static event Action<string> OnDeath = null;

    public void DoDamage(float _damage)
    {
        stats.Health -= _damage;
        if (stats.Health <= 0f)
            OnDeath?.Invoke(transform.name);
    }
}
