using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public PowerUp powerUpUIPrefab;
    public float SPACING = 10f;
    private Health enemyHealth;
    private List<PowerUp> choices = new List<PowerUp>();

    private void Start()
    {
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDeath += Respawn;
    }

    private void Respawn()
    {
        enemyHealth.maxHealth++;
        enemyHealth.Reset();
        MoveToRandomPosition();
        PlayerDraft();
    }

    private void MoveToRandomPosition()
    {

    }

    private void PlayerDraft()
    {
        for (int i = 0; i < 3; i++)
        {
            PowerUp newChoice = Instantiate(powerUpUIPrefab);
            choices.Add(newChoice);
        }
        for (int i = 0; i < 3; i++)
        {
            float transformAmount = (float)i - ((float)choices.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, 0) * SPACING * 10f;
            choices[i].transform.position = position;
        }
        //Pause Everything.
        //Wait for Player Choice.
    }
}
