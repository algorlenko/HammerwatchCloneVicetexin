using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image healthBar;

    public void AdjustHealthBar(float currentHp, float maxHp)
    {
        float percentHP = currentHp / maxHp;
        healthBar.fillAmount = percentHP;
        if (percentHP <= .33f)
        {
            healthBar.color = Color.red;
        }
        else if (percentHP <= .66f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.green;
        }
    }
}
