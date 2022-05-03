using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnRandomTile", menuName = "PowerUp/SpawnRandomTile")]
public class SpawnRandomTile : PowerUp
{
    public override void TriggerEffect()
    {
        TileManager tileManager = FindObjectOfType<TileManager>();
        tileManager.SpawnRandomTile();
    }
}
