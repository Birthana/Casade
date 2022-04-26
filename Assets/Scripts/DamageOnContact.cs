using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        transform.gameObject.SetActive(false);
    }
}
