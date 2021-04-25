using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int nDifficulty;
    public bool bPaused;

    public IntroPanel intropanel;
    public GameOverPanel gameoverpanel;

    public static GameManager inst; //A 'singleton' reference to the GameManager

    //If we want any startup sequence stuff we can put it here

    public void OnGameOver() {
        //pause and enable the game over panel
        gameoverpanel.gameObject.SetActive(true);
        Pause();
    }

    public void OnStartGame() {
        //disable the intro panel and unpause
        intropanel.gameObject.SetActive(false);
        UnPause();
        Countdown.inst.StartTimer();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause() {
        bPaused = true;

        Time.timeScale = 0f;
    }

    public void UnPause() {
        bPaused = false;

        Time.timeScale = 1f;
    }

    public void CheckForRestart() {
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }
    }

    public void Awake() {
        inst = this;
        Pause();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        CheckForRestart();
    }
}
