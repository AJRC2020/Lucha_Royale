using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public int totalEnemiesDefeated;

    private void Update(){
        updateEnemies(UIScript.enemiesDefeated);
    }

    public void updateEnemies(int enemies){
        totalEnemiesDefeated = 30 - enemies;
        pointsText.text = totalEnemiesDefeated.ToString() + " ENEMIES DEFEATED";
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



}
