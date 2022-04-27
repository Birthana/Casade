using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpReset : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            player.JumpReset();
        }
    }
}
