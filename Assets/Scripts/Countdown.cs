using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    public float fTimeLeft;
    public bool bTimeStarted;

    public DigitDisplay digitMinutes;
    public DigitDisplay digitSeconds10;
    public DigitDisplay digitSeconds1;
    public DigitDisplay digitMiliSec10;
    public DigitDisplay digitMiliSec1;

    public static Countdown inst;

    public void StartTimer() {
        fTimeLeft = Configurables.inst.fTimeLimitSeconds;
        bTimeStarted = true;
    }

    public void UpdateTimer() {
        fTimeLeft -= Time.deltaTime;

        if (fTimeLeft < 0) {
            fTimeLeft = 0f;
            bTimeStarted = false;
            GameManager.inst.OnGameOver();
        }

        UpdateTimeDisplay();
    }

    public void UpdateTimeDisplay() {
        int nTimeToDisplay = Mathf.FloorToInt(fTimeLeft * 100);

        int nMinutes = nTimeToDisplay / 6000;
        digitMinutes.DisplayDigit(nMinutes);

        nTimeToDisplay -= nMinutes * 6000;

        digitMiliSec1.DisplayDigit(nTimeToDisplay % 10);
        nTimeToDisplay /= 10;
        digitMiliSec10.DisplayDigit(nTimeToDisplay % 10);
        nTimeToDisplay /= 10;
        digitSeconds1.DisplayDigit(nTimeToDisplay % 10);
        nTimeToDisplay /= 10;
        digitSeconds10.DisplayDigit(nTimeToDisplay % 10);

    }

    public void Awake() {
        inst = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (bTimeStarted) {
            UpdateTimer();
        }
    }
}
