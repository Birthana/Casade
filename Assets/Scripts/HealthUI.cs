using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Collider2D icon;
    public Transform healthBar;
    public GameObject healthBarPrefab;
    public float SPACING;
    private List<Transform> healthBars = new List<Transform>(); 

    private void Start()
    {
        GetComponent<Health>().OnHealthChange += DisplayHealth;
    }

    private void DisplayHealth(int currentHealth)
    {
        DeleteHealthBar();
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHealthBar = Instantiate(healthBarPrefab, healthBar);
            healthBars.Add(newHealthBar.transform);
            Collider2D healthBarCollider = newHealthBar.GetComponent<Collider2D>();
            float y = healthBarCollider.bounds.size.y;
            Vector3 position = new Vector3(0, (y * i) + (icon.bounds.size.y  * 3 / 5), 0);
            newHealthBar.transform.localPosition = position ;
        }
    }

    private void DeleteHealthBar()
    {
        foreach (Transform healthBar in healthBars)
        {
            Destroy(healthBar.gameObject);
        }
        healthBars = new List<Transform>();
    }
}
