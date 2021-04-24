using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceSpawner : MonoBehaviour {

    public static DisturbanceSpawner inst; // A 'singleton' reference to the disturbance spawner

    public GameObject pfDisturbance;

    //Internal Use
    public List<Disturbance> lstDisturbances;
    public float fTimeUntilNewSpawn;

    public Vector3 GetRandomStartingPoint() {
        Vector3 v3Random = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        if (v3Random == Vector3.zero) v3Random = new Vector3(0, 1, 0);
        v3Random = v3Random.normalized * Configurables.inst.fDisturbanceSpawnDist;

        return v3Random;
    }
    
    public void InitNewDisturbance(Disturbance disturbance) {

        int nDifficulty = GameManager.inst.nDifficulty;

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
        return Random.Range(1, 6);
    }

    public int GetRandomMeter() {
        return Random.Range(1, 4);
    }

    public float GetRandomTimeToReachCenter(int nDifficulty) {
        return Configurables.inst.fTimeToReachCenterAverage + Random.Range(-Configurables.inst.fTimeToReachCenterVariance, Configurables.inst.fTimeToReachCenterVariance);
    }

    public void SpawnDisturbance (int nDifficulty){
        //Spawn an appropriate random disturbance of given difficulty

        Vector3 v3PositionToSpawnAt = GetRandomStartingPoint();

        GameObject goNewlySpawned = GameObject.Instantiate(pfDisturbance, this.transform);

        goNewlySpawned.transform.localPosition = v3PositionToSpawnAt;

        Disturbance disturbance = goNewlySpawned.GetComponent<Disturbance>();

        InitNewDisturbance(disturbance);

        AddDisturbanceToCollection(disturbance);
    }

    public void SetNewDisturbanceSpawnTime() {
        fTimeUntilNewSpawn = Configurables.inst.fDisturbanceSpawnRate + Random.Range(-Configurables.inst.fDisturbanceSpawnVariance, Configurables.inst.fDisturbanceSpawnVariance);
    }

    public void AddDisturbanceToCollection(Disturbance disturbance) {
        //Ensure this disturbance is tracked in the list of all disturbances

        lstDisturbances.Add(disturbance);
    }

    public void RemoveDisturbanceFromCollection(Disturbance disturbance) {
        //Remove this disturbance from the tracked collection of all disturbances

        lstDisturbances.Remove(disturbance);
    }


    public void Awake() {
        inst = this;
    }

    // Start is called before the first frame update
    void Start() {
        lstDisturbances = new List<Disturbance>();
    }

    // Update is called once per frame
    void Update() {

        fTimeUntilNewSpawn -= Time.deltaTime;

        if (fTimeUntilNewSpawn <= 0f) {
            SpawnDisturbance(GameManager.inst.nDifficulty);

            SetNewDisturbanceSpawnTime();
        }

    }
}
