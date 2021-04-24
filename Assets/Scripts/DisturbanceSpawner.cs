using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceSpawner : MonoBehaviour {

    public static DisturbanceSpawner inst; // A 'singleton' reference to the disturbance spawner

    public List<Disturbance> lstDisturbances;



    public void SpawnDisturbance (int nDifficulty){
        //Spawn an appropriate random disturbance of given difficulty

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

    }
}
