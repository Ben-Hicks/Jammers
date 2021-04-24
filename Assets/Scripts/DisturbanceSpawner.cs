using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceSpawner : MonoBehaviour {

    public static DisturbanceSpawner inst; // A 'singleton' reference to the disturbance spawner

    //Configure with prefabs for spawning
    public GameObject[] lstDisturbancePrefabs1; //Difficulty 1 prefabs
    public GameObject[] lstDisturbancePrefabs2; //Difficulty 2 prefabs
    public GameObject[] lstDisturbancePrefabs3; //Difficulty 3 prefabs
    public GameObject[] lstDisturbancePrefabs4; //Difficulty 4 prefabs

    //Internal Use
    public List<Disturbance> lstDisturbances;
    public float fTimeUntilNewSpawn;

    public Vector3 GetRandomStartingPoint() {
        Vector3 v3Random = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        v3Random = v3Random.normalized * Configurables.inst.fDisturbanceSpawnDist;

        return v3Random;
    }

    public GameObject GetRandomPrefabOfDifficulty(int nDifficulty) {

        GameObject goDisturbancePrefab = null;

        switch (nDifficulty) {
            case 1:
                goDisturbancePrefab = lstDisturbancePrefabs1[Random.Range(0, lstDisturbancePrefabs1.Length)];
                break;
            case 2:
                goDisturbancePrefab = lstDisturbancePrefabs1[Random.Range(0, lstDisturbancePrefabs1.Length)];
                break;
            case 3:
                goDisturbancePrefab = lstDisturbancePrefabs1[Random.Range(0, lstDisturbancePrefabs1.Length)];
                break;
            case 4:
                goDisturbancePrefab = lstDisturbancePrefabs1[Random.Range(0, lstDisturbancePrefabs1.Length)];
                break;
        }

        return goDisturbancePrefab;
    }

    public void SpawnDisturbance (int nDifficulty){
        //Spawn an appropriate random disturbance of given difficulty

        GameObject goDisturbanceToSpawn = GetRandomPrefabOfDifficulty(nDifficulty);
        Vector3 v3PositionToSpawnAt = GetRandomStartingPoint();

        GameObject goNewlySpawned = GameObject.Instantiate(goDisturbanceToSpawn, v3PositionToSpawnAt, Quaternion.identity, this.transform);

        Disturbance disturbance = goNewlySpawned.GetComponent<Disturbance>();

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
