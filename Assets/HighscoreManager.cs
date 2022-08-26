using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    ScoreManager scoreManager;

    public TextMeshProUGUI highscoreText;
    int currentHighscore = 0;
    
    void Start(){
        scoreManager = GetComponent<ScoreManager>();
        PlayerManager.Instance.onDeath.AddListener(CheckHighscore);
        currentHighscore = PlayerPrefs.GetInt("Highscore");
    }

    private void Update() {
        highscoreText.text = "Highscore: " + currentHighscore.ToString();
    }

    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(CheckHighscore);
    }

    void CheckHighscore(){
        if(scoreManager.score > currentHighscore){
            // highscoreText.text = "Highscore: " + scoreManager.score.ToString();
            currentHighscore = scoreManager.score;
            PlayerPrefs.SetInt("Highscore",currentHighscore);
            PlayerPrefs.Save();
        }
    }
}
