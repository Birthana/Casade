using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Health health;
    public Transform healthBar;
    public GameObject healthBarPrefab;
    public float SPACING;
    private List<Transform> healthBars = new List<Transform>(); 

    private void Start()
    {
        health.OnHealthChange += DisplayHealth;
    }

    private void DisplayHealth(int currentHealth)
    {
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHealthBar = Instantiate(healthBarPrefab, healthBar);
            healthBars.Add(newHealthBar.transform);
            float transformAmount = ((float)i) - ((float)currentHealth - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), 0) * SPACING;
            newHealthBar.transform.localPosition = position;
        }
    }
}
