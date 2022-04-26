using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;

    public bool showLines;
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform lengthPoint;
    public float lineSpacing;
    public float tileSpacing;
    public int lines = 34;
    public int tiles = 16;

    private void Start()
    {
        for(int i = 1; i < 11; i++)
        {
            SpawnTile(i, 6);
        }
    }

    public void SpawnTile(int x, int y)
    {
        GameObject tile = Instantiate(tilePrefab, transform);
        tile.transform.position = leftPoint.position + new Vector3(x * tileSpacing, -y * lineSpacing);
    }

    private void OnDrawGizmosSelected()
    {
        if (showLines)
        {
            for (int i = 0; i < lines; i++)
            {
                Vector2 left = leftPoint.position - new Vector3(0, i * lineSpacing);
                Vector2 right = rightPoint.position - new Vector3(0, i * lineSpacing);
                Gizmos.DrawLine(left, right);
            }
            for (int i = 0; i < tiles; i++)
            {
                Vector2 right = rightPoint.position - new Vector3(i * tileSpacing, 0);
                Vector2 length = lengthPoint.position - new Vector3(i * tileSpacing, 0);
                Gizmos.DrawLine(right, length);
            }
        }
    }
}
