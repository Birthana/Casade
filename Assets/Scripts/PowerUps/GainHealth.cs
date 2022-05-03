using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUp/GainHealth")]
public class GainHealth : PowerUp
{
    public override void TriggerEffect()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        Health playerHealth = player.gameObject.GetComponent<Health>();
        playerHealth.maxHealth++;
        playerHealth.Reset();
    }
}
