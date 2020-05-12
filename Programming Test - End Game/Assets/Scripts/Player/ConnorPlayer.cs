using System;
using UnityEngine;

public class ConnorPlayer : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStats stats = null;

    public static event Action OnDeath = null;

    public void DoDamage(float _damage)
    {
        stats.Health -= _damage;
    }
}
