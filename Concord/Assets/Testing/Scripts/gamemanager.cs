using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject RetryScreen;
    public GameObject PauseScreen;
    public PlayerMotion player;

    private float playerHealth = 1.0f;
    public bool gameStart = false;
    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        StartScreen.SetActive(true);
        RetryScreen.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void startGame()
    {
        StartScreen.SetActive(false);
        gameStart = true;
        Time.timeScale = 1.0f;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Town");
    }

    public void endGame()
    {
        #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
        #else
          Application.Quit();
        #endif
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        paused = false;
        PauseScreen.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        paused = true;
        PauseScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerStats>().currHealth;
        if(playerHealth <= 0.0f)
        {
            RetryScreen.SetActive(true);
            gameStart = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gameStart == true)
        {
            if(paused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
