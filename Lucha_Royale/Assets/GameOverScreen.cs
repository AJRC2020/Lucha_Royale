using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public int totalEnemiesDefeated;

    private void Update(){
        updateEnemies(UIScript.enemiesLeft);
    }

    public void updateEnemies(int enemies){
        totalEnemiesDefeated = 30 - enemies;
        pointsText.text = totalEnemiesDefeated.ToString() + " ENEMIES DEFEATED";
    }



}
