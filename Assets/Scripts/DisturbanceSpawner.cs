using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceSpawner : MonoBehaviour {

    public static DisturbanceSpawner inst; // A 'singleton' reference to the disturbance spawner

    public GameObject pfDisturbance;

    public GameObject goPosTop;
    public GameObject goPosLeft;
    public GameObject goPosRight;
    public GameObject goPosBot;

    public SpriteRenderer sprrenExpression;
    public Sprite[] arsprExpressions;

    //Internal Use
    public float fTimeUntilNewSpawn;
    public float fTimeToRecoverFromJerk;

    public Vector3 GetRandomStartingPoint() {

        int nSpawnOnSide = Random.Range(0, 4); //0 top, 1 right, 2 bot, 3 left

        float fRandomX = Random.Range(goPosLeft.transform.localPosition.x, goPosRight.transform.localPosition.x);
        float fRandomY = Random.Range(goPosBot.transform.localPosition.y, goPosTop.transform.localPosition.y);

        float fX = 0f;
        float fY = 0f;

        switch (nSpawnOnSide) {
            case 0: //top
                fX = fRandomX;
                fY = goPosTop.transform.localPosition.y;
                break;

            case 1: //right
                fX = goPosRight.transform.localPosition.x;
                fY = fRandomY;
                break;

            case 2: //bot
                fX = fRandomX;
                fY = goPosBot.transform.localPosition.y;
                break;

            case 3: //left
                fX = goPosLeft.transform.localPosition.x;
                fY = fRandomY;
                break;
        }

        Vector3 v3Random = new Vector3(fX, fY, 0f);

        return v3Random;
    }
    
    public void InitNewDisturbance(Disturbance disturbance, int nDifficulty) {

        bool bPosNeg = GetRandomPosNeg(nDifficulty);
        int nIntensity = GetRandomIntensity(nDifficulty);
        int nMeter = GetRandomMeter();
        float fTimeToReachCenter = GetRandomTimeToReachCenter(nDifficulty);

        disturbance.Init(bPosNeg, nIntensity, nMeter, fTimeToReachCenter);
    }

    public bool GetRandomPosNeg(int nDifficulty) {
        return Random.Range(0, 100) <= 50 ? true : false;
    }

    public int GetRandomIntensity(int nDifficulty) {
        return Random.Range(1, 4) + nDifficulty;
    }

    public int GetRandomMeter() {
        return Random.Range(1, 4);
    }

    public float GetRandomTimeToReachCenter(int nDifficulty) {
        float fSpeedupFromDifficulty = nDifficulty * Configurables.inst.fDisturbanceSpeedFromDifficulty;
        return Configurables.inst.fTimeToReachCenterAverage + Random.Range(-Configurables.inst.fTimeToReachCenterVariance, Configurables.inst.fTimeToReachCenterVariance) - fSpeedupFromDifficulty;
    }

    public void SpawnDisturbance (int nDifficulty){
        //Spawn an appropriate random disturbance of given difficulty

        Vector3 v3PositionToSpawnAt = GetRandomStartingPoint();

        GameObject goNewlySpawned = GameObject.Instantiate(pfDisturbance, this.transform);

        goNewlySpawned.transform.localPosition = v3PositionToSpawnAt;

        Disturbance disturbance = goNewlySpawned.GetComponent<Disturbance>();

        InitNewDisturbance(disturbance, nDifficulty);
    }

    public void SetNewDisturbanceSpawnTime() {
        float fTimeSpeedupFromDifficulty = GameManager.inst.nDifficulty * Configurables.inst.fDisturbanceSpawnRateFromDifficulty;
        fTimeUntilNewSpawn = Configurables.inst.fDisturbanceSpawnRate + Random.Range(-Configurables.inst.fDisturbanceSpawnVariance, Configurables.inst.fDisturbanceSpawnVariance) - fTimeSpeedupFromDifficulty;
    }

    public void SetJerkExpression() {

        SetExpression(0);

        fTimeToRecoverFromJerk = Configurables.inst.fTimeJerkExpression;
    }

    public void SetExpression(int iExpression) {

        //Don't overwrite the jerking expression with other normal ones
        if (iExpression != 0 && fTimeToRecoverFromJerk > 0f) return;

        if (sprrenExpression.sprite != arsprExpressions[iExpression]) {
            sprrenExpression.sprite = arsprExpressions[iExpression];
        }

    }

    public void SetExpressionToAppropriate() {
        SetExpression(Mathf.FloorToInt(Score.inst.fMultiplier));
    }

    public void UpdateExpression() {

        //If we need to recover, reduce the timer appropriately
        if (fTimeToRecoverFromJerk > 0f) {
            fTimeToRecoverFromJerk -= Time.deltaTime;
        } else if(fTimeToRecoverFromJerk < 0f) { 
            fTimeToRecoverFromJerk = 0f;
            //Set the expression as appropriate after recovering from a jerk
            SetExpressionToAppropriate();
        }

    }

    public void Awake() {
        inst = this;
    }

    // Start is called before the first frame update
    void Start() {

        fTimeUntilNewSpawn = Configurables.inst.fDisturbanceSpawnInitialDelay;

        SetExpressionToAppropriate();
    }

    // Update is called once per frame
    void Update() {

        fTimeUntilNewSpawn -= Time.deltaTime;

        if (fTimeUntilNewSpawn <= 0f) {
            SpawnDisturbance(GameManager.inst.nDifficulty);

            SetNewDisturbanceSpawnTime();
        }

        UpdateExpression();

    }
}
