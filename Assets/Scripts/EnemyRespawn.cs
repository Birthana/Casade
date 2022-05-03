using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyRespawn : MonoBehaviour
{
    public TextMeshProUGUI killCountText;
    public PowerUpDisplay powerUpUIPrefab;
    public float SPACING = 10f;
    public BoxCollider2D spawnArea;
    public LayerMask uiLayer;
    public List<PowerUp> powerUps = new List<PowerUp>();
    private Health enemyHealth;
    private List<PowerUpDisplay> choices = new List<PowerUpDisplay>();
    private int killCount = 0;

    private void Start()
    {
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDeath += Respawn;
    }

    private void Respawn()
    {
        killCount++;
        killCountText.text = $"Enemies Killed:\n {killCount}";
        enemyHealth.maxHealth++;
        enemyHealth.Reset();
        MoveToRandomPosition();
        PlayerDraft();
    }

    private void MoveToRandomPosition()
    {
        Vector2 size = spawnArea.bounds.size;
        int rngX = Random.Range(0, (int)size.x);
        int rngY = Random.Range(0, (int)size.y);
        transform.position = new Vector3(rngX - (size.x / 2), rngY - (size.y / 2), 0);
    }

    private void PlayerDraft()
    {
        Time.timeScale = 0;
        List<int> rngNumbers = GetRandomNumbers(3);
        for (int i = 0; i < 3; i++)
        {
            PowerUpDisplay newChoice = Instantiate(powerUpUIPrefab);
            newChoice.SetPowerUp(powerUps[rngNumbers[i]]);
            choices.Add(newChoice);
        }
        for (int i = 0; i < 3; i++)
        {
            float transformAmount = (float)i - ((float)choices.Count - 1) / 2;
            float angle = transformAmount * 3.0f;
            Vector3 position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, 0) * SPACING * 10f;
            choices[i].transform.position = position;
        }
        StartCoroutine("Choosing");
    }

    IEnumerator Choosing()
    {
        bool still_looking = true;
        while (still_looking)
        {
            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000, uiLayer);
                if (hit)
                {
                    still_looking = false;
                    hit.collider.gameObject.GetComponent<PowerUpDisplay>().powerUp.TriggerEffect();
                    foreach (PowerUpDisplay choice in choices)
                    {
                        Destroy(choice.gameObject);
                    }
                    choices = new List<PowerUpDisplay>();
                    Time.timeScale = 1;
                }
            }
            yield return null;
        }
    }

    private List<int> GetRandomNumbers(int numberOfNumbers)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < numberOfNumbers; i++)
        {
            int newNumber = Random.Range(0, powerUps.Count);
            while (result.Contains(newNumber))
            {
                newNumber = Random.Range(0, powerUps.Count);
            }
            result.Add(newNumber);
        }
        return result;
    }
}
