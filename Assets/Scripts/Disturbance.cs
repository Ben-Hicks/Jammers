using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturbance : MonoBehaviour {

    //Configuration set by prefab
    public bool bPosNeg; //true if the impact would increase the value
    public int nIntensity; //positive number representing the impact of the disturbance
    public int nMeter; //Which meter this disturbance will affect (1, 2, or 3)
    public float fTimeToReachCenter; //How long to reach the center

    //Internal use
    public float fTimeAlive; //How long the disturbance has been spawned
    public float fProgressToCenter; //a [0,1] percentage of the progress
    public Vector3 v3InitialPosition; //The initial position where this was spawned
    

    public void SetPosition() {

        fProgressToCenter = fTimeAlive / fTimeToReachCenter;

        Vector3 v3NewPosition = Vector3.Lerp(v3InitialPosition, Vector3.zero, fProgressToCenter);

        if(fProgressToCenter >= 1) {
            OnCollision();
        }

    }


    public void OnCollision() {
        //What to do when this disturbance collides with the sleeper

        DisturbanceSpawner.inst.RemoveDisturbanceFromCollection(this);

        Monitor.inst.arMeters[nMeter].OnDisturbanceCollision(this);

        GameObject.Destroy(this);
    }

    // Start is called before the first frame update
    void Start() {
        v3InitialPosition = this.transform.localPosition;

    }

    // Update is called once per frame
    void Update() {

        fTimeAlive += Time.deltaTime;

        SetPosition();

    }
}
