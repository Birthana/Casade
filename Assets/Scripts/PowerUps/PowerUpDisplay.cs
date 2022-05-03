using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpDisplay : MonoBehaviour
{
    public PowerUp powerUp;
    public TextMeshPro text;

    public void SetText()
    {
        text.text = powerUp.description;
    }

    public void SetPowerUp(PowerUp pow)
    {
        powerUp = pow;
        SetText();
    }
}
