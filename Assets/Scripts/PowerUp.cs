using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUp : MonoBehaviour
{
    public TextMeshPro text;
    [TextArea(3,5)]
    public string description;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    private void SetText()
    {
        text.text = description;
    }


}
