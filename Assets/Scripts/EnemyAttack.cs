using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float attackCooldown;
    public float attackRange;
    public float attackSpeed;
    public float attackDuration;
    public LayerMask playerLayer;
    private bool isAttacking = false;

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, 0, playerLayer);
        if (hit)
        {
            GameObject newFireBall = Instantiate(fireballPrefab, transform);
            Vector3 target = hit.collider.transform.position;
            Vector3 direction = (target - transform.position).normalized;
            newFireBall.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            newFireBall.transform.position += direction;
            newFireBall.GetComponent<Rigidbody2D>().AddForce(direction * attackSpeed * 50);
            Destroy(newFireBall, attackDuration);
            yield return new WaitForSeconds(attackCooldown);
        }
        isAttacking = false;
    }


    private void OnDrawGizmos()
    {
        if (attackRange == 0)
            return;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
