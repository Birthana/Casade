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
        for(int i = 4; i < 7; i++)
        {
            SpawnTile(i, 9);
            SpawnTile(i, 21);
        }
        for (int i = 10; i < 13; i++)
        {
            SpawnTile(i, 9);
            SpawnTile(i, 21);
        }
        for (int i = 0; i < 2; i++)
        {
            SpawnTile(i, 15);
        }
        for (int i = 15; i < 17; i++)
        {
            SpawnTile(i, 15);
        }

        for (int i = 1; i < 16; i++)
        {
            SpawnTile(i, 27);
            SpawnTile(i, 33);
        }
    }

    public void SpawnTile(int x, int y)
    {
        GameObject tile = Instantiate(tilePrefab, transform);
        tile.transform.position = leftPoint.position + new Vector3(x * tileSpacing, -y * lineSpacing);
        if (y % 2 != 0)
            tile.transform.position += new Vector3(tileSpacing / 2, 0);
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
