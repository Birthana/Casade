using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damage;
    public LayerMask effectedLayers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer != effectedLayers)
            return;
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        transform.gameObject.SetActive(false);
    }
}
