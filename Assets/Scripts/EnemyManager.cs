using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int totalEnemies = 10; 
    private int enemiesRemaining;

    public TMP_Text enemyCounterText; 
    void Start()
    {
        enemiesRemaining = totalEnemies;
        UpdateEnemyCounterText();
    }

    public void EnemyDied()
    {
        enemiesRemaining--;

        
        if (enemiesRemaining <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        UpdateEnemyCounterText();
    }

    void UpdateEnemyCounterText()
    {
        
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Enemies Remaining: " + enemiesRemaining;
        }
    }
}
