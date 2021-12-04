using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public PlayerSpawn checkPoint;
    public GameObject camera;
    public GameObject StartScreen;
    public GameObject RetryScreen;
    public GameObject PauseScreen;
    public GameObject playerUI;
    public GameObject player;

    public GameObject easyModeSpawn;

    public bool gameStart = false;
    public bool paused = false;
    public bool reachedCheckPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        StartScreen.SetActive(true);
        RetryScreen.SetActive(false);
        playerUI.SetActive(false);
        easyModeSpawn.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void setPlayer()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void startGame()
    {
        StartScreen.SetActive(false);
        playerUI.SetActive(true);
        gameStart = true;
        Time.timeScale = 1.0f;
        checkPoint.placeAtFirstSpawn();
        camera.GetComponent<CameraControl>().setCameraTarget();
        easyModeSpawn.SetActive(true);
        setPlayer();
    }

    public void HardMode()
    {
        StartScreen.SetActive(false);

        
        playerUI.SetActive(true);
        gameStart = true;
        Time.timeScale = 1.0f;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Town");
    }

    public void returnToCheckPoint()
    {
        if(reachedCheckPoint == true)
        {
            checkPoint.placeAtBossSpawn();
            gameStart = true;
        }
        else
        {
            restartGame();
        }
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

    public void GameOver()
    {
        RetryScreen.SetActive(true);
        gameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
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
