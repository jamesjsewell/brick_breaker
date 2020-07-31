using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score;
    public Text scoreText;
    public Text livesText;
    public Text highScoreText;
    public Text gameOverPanelScoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    public Transform toggleSoundButton;
    public Sprite soundOffSprite;
    public Sprite soundOnSprite;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        
        scoreText.text = score.ToString();
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void UpdateLives(int changeInLives){
        lives += changeInLives;

        if(lives <= 0){
            lives = 0;
            GameOver();
        }

        livesText.text = lives.ToString();
       


    }

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = score.ToString();
    }

    public void UpdateNumberOfBricks() {
        numberOfBricks--;
        if(numberOfBricks <= 0) {
            if(currentLevelIndex >= levels.Length-1) {
                gameOverText.text = "COMPLETE";
                GameOver();
            }
            else {
                gameOver = true;
                Invoke("LoadLevel", 1f);
            }
        }
    }

    void LoadLevel() {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
    }

    void GameOver() {
        gameOver = true;
        gameOverPanel.SetActive(true);
        gameOverPanelScoreText.text = score.ToString();

        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if(score > highScore){
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "NEW HIGH SCORE!";
        } else {
            highScoreText.text = highScore - score + " AWAY FROM BEATING " + highScore;
        }
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit() {
        Application.Quit();
    }

    void ToggleSound() {
 
        AudioListener.pause = !AudioListener.pause;

        if(AudioListener.pause) {
            toggleSoundButton.gameObject.GetComponent<Image>().sprite = soundOffSprite;
        }
        else{
            toggleSoundButton.gameObject.GetComponent<Image>().sprite = soundOnSprite;
        }
    }
}
