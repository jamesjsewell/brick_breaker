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
    public Text highScoreText;
    public Text gameOverPanelScoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    AudioSource audio;
    public Transform[] livesIcons;
     

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        
        scoreText.text = score.ToString();
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void UpdateLives(int changeInLives){
        lives += changeInLives;

        if(lives <= 0){
            lives = 0;
            audio.Play();
            GameOver();
        }

        for (int i = 0; i < 10; i++) 
        {
            if(lives-1 < i) {
                livesIcons[i].gameObject.SetActive(false);
                continue;

            }
            livesIcons[i].gameObject.SetActive(true);
             
        }
       


    }

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = score.ToString();
    }

    public void UpdateNumberOfBricks() {
        numberOfBricks--;
        if(numberOfBricks <= 0) {
            if(currentLevelIndex >= levels.Length-1) {
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
            highScoreText.text = "New High Score!";
        } else {
            highScoreText.text = highScore - score + " Away From Beating High Score " + highScore;
        }
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit() {
        Application.Quit();
    }
}
