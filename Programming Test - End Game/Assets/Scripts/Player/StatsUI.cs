using System;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats = null;

    [Header("Animation")]
    [SerializeField] private float animationTime = 0f;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.notUsed;

    [Header("UI")]
    [SerializeField] private Image fillBar = null;

    private void Awake()
    {
        stats.OnHealthUpdate += UpdateHealth;
        fillBar.fillAmount = stats.Health;
    }

    private void UpdateHealth()
    {
        LeanTween.value(fillBar.fillAmount, stats.Health / 100f, animationTime).setEase(tweenType).setOnUpdate((value) => UpdateHealthValue(value));
    }

    private void UpdateHealthValue(float _value) => fillBar.fillAmount = _value;
}
