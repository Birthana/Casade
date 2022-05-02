using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool isTemporary;

    public void TryDestroy()
    {
        if (isTemporary)
            Destroy(gameObject);
    }
}
