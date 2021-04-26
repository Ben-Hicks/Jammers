using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int nDifficulty;
    public bool bPaused;

    public IntroPanel intropanel;
    public GameOverPanel gameoverpanel;
    
    public AudioSource audioSource;
    public float fAudioLength;
    public float fAudioFadeoutSpeed;

    public bool bGameStarted;
    public bool bGameEnd;

    float fTimePlayingMusic;

    public float fUpdateGraphicsInterval;
    float fTimeSinceLastUpdate;

    public static GameManager inst; //A 'singleton' reference to the GameManager

    //If we want any startup sequence stuff we can put it here

    public void OnGameOver() {
        //pause and enable the game over panel
        gameoverpanel.gameObject.SetActive(true);
        gameoverpanel.OnGameOver();
        Pause();
        bGameEnd = true;
    }

    public void OnStartGame() {
        //disable the intro panel and unpause
        intropanel.gameObject.SetActive(false);
        UnPause();
        Countdown.inst.StartTimer();
        StartMusic();
        bGameStarted = true;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause() {
        bPaused = true;

        if (bGameStarted) audioSource.Pause();

        Time.timeScale = 0f;
    }

    public void UnPause() {
        if (bGameEnd) return;
        bPaused = false;

        if (bGameStarted) audioSource.UnPause();

        Time.timeScale = 1f;
    }

    public void StartMusic() {
        audioSource.Play();
    }

    public void ManageMusicVolume() {

        fTimePlayingMusic += Time.deltaTime;

        if (fTimePlayingMusic >= fAudioLength) {
            audioSource.volume -= fAudioFadeoutSpeed * Time.deltaTime;
            if (audioSource.volume == 0f) audioSource.Stop();
        }
    }

    public void CheckForRestart() {
        if (Input.GetKeyDown(KeyCode.R) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
            RestartGame();
        }
    }

    public void CheckForPause() {
        if (bGameStarted == false) return;

        if (Input.GetKeyDown(KeyCode.Space) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
            if (bPaused) {
                UnPause();
            } else {
                Pause();
            }
        }
    }

    public void UpdateGraphics() {
        int nMultiplier = Mathf.FloorToInt(Score.inst.fMultiplier);

        SyncWaves.inst.OnMultiplierChange(nMultiplier);
        DisturbanceSpawner.inst.SetExpression(nMultiplier);
        Score.inst.UpdateMultiplierGraphics(nMultiplier);

    }

    public void CheckForUpdatingGraphics() {
        fTimeSinceLastUpdate += Time.deltaTime;

        if(fTimeSinceLastUpdate > fUpdateGraphicsInterval) {
            UpdateGraphics();
            fTimeSinceLastUpdate = 0f;
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

        CheckForPause();

        if (bGameStarted) ManageMusicVolume();

        CheckForUpdatingGraphics();
    }
}
