using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float maxHealth;
    [SerializeField] private float health;
    public event Action OnHealthUpdate;

    public float Health
    {
        get => Mathf.Clamp(health, 0f, maxHealth);
        set
        {
            health = value;
            OnHealthUpdate?.Invoke();
        }
    }

    public void CheatDoDamage() => Health -= 10f;

    public void CheatMaxHealth() => Health = maxHealth;
}
