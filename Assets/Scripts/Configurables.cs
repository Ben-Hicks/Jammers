using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurables : MonoBehaviour {

    public float fMaxVal;
    public float fMaxVelocity;
    public float fVelocityIncrement; //How much pressing a key accelerates the velocity
    public float[] arfValIncrements; //How much different level disturbances influence the value

    public float fZeroingRate; //How fast the velocity returns to 0 when no keys are held (as a percentage multiplier in [0, 1]
    public float fZeroingDelay; //How much time must elapse where no keys are pressed before the velocity slows to 0
    public float fZeroingFrequency; //Time between zeroing friction applications

    public float fDisturbanceSpawnInitialDelay;
    public float fDisturbanceSpawnRate; //On average, how long it takes between disturbance spawns;
    public float fDisturbanceSpawnVariance; //The time window in which disturbances can randomly spawn away from the average rate
    public float fDisturbanceSpawnRateFromDifficulty;

    public float fTimeToReachCenterAverage;
    public float fTimeToReachCenterVariance;
    public float fDisturbanceSpeedFromDifficulty;

    public float fSweetspotWidth;
    public float fPassiveComboDrain;
    public float fSweetspotComboIncrease;
    public float fComboJerkPenalty;
    public float fComboJerkDelay;
    public float fPassiveScoreIncrease;

    public float fTimeLimitSeconds;

    public float fSyncVelocity;
    public float fResyncVelocity;

    public float[] arfSyncGaps;

    public float fTimeJerkExpression;
    public float fMaxDisturbanceSpin;
    public float fMinDisturbanceSpin;
    public float fDisturbanceSpinRatio;

    public static Configurables inst;




    public void Awake() {
        inst = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
