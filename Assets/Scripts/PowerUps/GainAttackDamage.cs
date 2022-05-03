using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainAttackDamage", menuName = "PowerUp/GainAttackDamage")]
public class GainAttackDamage : PowerUp
{
    public override void TriggerEffect()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.attackDamage++;
    }
}
