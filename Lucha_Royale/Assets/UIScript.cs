using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public int enemiesLeft = 30;
    public Text leftText;
    public Text damageText;
    public WrestlerScript luchador;
    public Image progressBar;

    private void Update()
    {
        updateDamageText();
        updateProgressBar();
    }

    [ContextMenu("Eliminate Enemy")]
    public void eliminated()
    {
        enemiesLeft--;
        leftText.text = "Left: " + enemiesLeft.ToString();
    }

    [ContextMenu("Damage Up")]
    public void updateDamageText()
    {
        damageText.text = "Damage: " + (luchador.damage - 1.0).ToString() + "%";
    }

    [ContextMenu("Filling Bar")]
    public void updateProgressBar()
    {
        progressBar.fillAmount = luchador.cheer_up;
    }
}

