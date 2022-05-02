using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnHealthChange;
    public event Action OnDeath;
    public int maxHealth;
    private int currentHealth;

    private void Start()
    {
        Reset();
    }

    public void HealDamage(int damage)
    {
        ChangeHealth(damage);
    }

    public void TakeDamage(int damage)
    {
        ChangeHealth(-damage);
    }

    private void ChangeHealth(int change)
    {
        currentHealth = Mathf.Max(0, currentHealth + change);
        OnHealthChange?.Invoke(currentHealth);
        if (currentHealth <= 0)
            OnDeath?.Invoke();
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(currentHealth);
    }
}
