using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {

    //Game-Running values
    public float fCurVal;
    public float fCurVelocity;
    public float fTimeSinceLastInput;

    //Configuration - Set and Forget
    public KeyCode keyAccel;
    public KeyCode keyDeccel;
    public GameObject goSliderHandle;

    public void PositionSliderHandle() {
        //Position the handle in the appropriate range for fCurVal in {-fMaxVal, +fMaxVal}


    }

    public void ChangeVelocity(float fDeltaVelocity) {
        //Modify curVelocity by deltaVelocity

    }

    public void ChangeVal(float fDeltaVal) {
        //Modify curVal by deltaVal
        fCurVal += fDeltaVal;

        PositionSliderHandle();
    }

    public void UpdateValFromVelocity() {
        //At the end of each frame, update the position of the value depending on its velocity

    }

    public void UpdateVelocity() {
        //At the end of each frame, update the velocity depending on inputs (or by gradually zeroing velocity if there haven't been inputs recently)

    }

    public void OnDisturbanceCollision(int nIntensity) {
        //React to colliding with a disturbance with given intensity (may be positive or negative)

    }

    public void CheckForGameOver() {
        //Check if curVal has reached one of the extremes of the allowable range - game over if so

    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
