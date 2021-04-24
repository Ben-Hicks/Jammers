using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturbance : MonoBehaviour {

    //Set by prefab
    public int nIntensity; //positive or negative number representing the impact of the disturbance
    public int nMeter; //Which meter this disturbance will affect (1, 2, or 3)



    public void OnCollision() {
        //What to do when this disturbance collides with the sleeper

    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
