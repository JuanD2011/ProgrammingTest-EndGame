using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerStats enemyStats;

    private void Start()
    {
        ResetPlayers();
        PlayerCharacter.OnDeath += OnPlayerDead;
    }

    private void OnDestroy()
    {
        PlayerCharacter.OnDeath -= OnPlayerDead;
    }

    private void ResetPlayers()
    {
        playerStats.CheatMaxHealth();
        enemyStats.CheatMaxHealth();
    }

    private void OnPlayerDead(string _name)
    {
        Notification.Show(string.Format("{0} is dead!", _name));
        ResetPlayers();
    }
}
