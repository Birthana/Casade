using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnHealthChange;
    public int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(currentHealth);
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
    }
}
