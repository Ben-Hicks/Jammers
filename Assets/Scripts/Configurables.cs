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
