using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public int enemiesLeft = 30;
    public static int enemiesDefeated;
    public Text leftText;
    public Text damageText;
    public Text pageText;
    public WrestlerScript luchador;
    public Image progressBar;
    public Text hitText;
    public Image chair;
    bool chairExists = false;

    void Start()
    {
        chair.enabled = false;
        hitText.enabled = false;
    }

    private void Update()
    {
        enemiesDefeated = enemiesLeft;
        updateDamageText();
        updateProgressBar();
        updatePaperCount();
        if (chairExists)
        {
            updateChairCount();
        }
        if(enemiesLeft == 0){
            enemiesLeft = 30;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    [ContextMenu("Eliminate Enemy")]
    public void eliminated()
    {
        enemiesLeft--;
        leftText.text = "Enemies: " + enemiesLeft.ToString();
    }

    [ContextMenu("Damage Up")]
    public void updateDamageText()
    {
        damageText.text = "Health: " + (luchador.damage - 1.0).ToString("N2") + "%";
    }

    [ContextMenu("Filling Bar")]
    public void updateProgressBar()
    {
        progressBar.fillAmount = luchador.cheer_up;
    }

    [ContextMenu("Papers Please")]
    public void updatePaperCount()
    {
        pageText.text = "X " + luchador.paper_count.ToString();
    }

    [ContextMenu("Chairshot")]
    public void updateChairCount()
    {
        hitText.text = "X " + luchador.chair_hits.ToString();
    }

    public void enableChair()
    {
        chair.enabled = true;
        chairExists = true;
        hitText.enabled = true;
    }

    public void disableChair()
    {
        chair.enabled = false;
        chairExists = false;
        hitText.enabled = false; ;
    }
}

