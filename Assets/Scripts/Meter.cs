using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {

    //Game-Running values
    public float fCurVal;
    public float fCurVelocity;
    public float fTimeSinceLastInput;
    public float fPercentageVal; //An easier to interpret scale for where the slider should appear

    //Configuration - Set and Forget
    public KeyCode keyAccel;
    public KeyCode keyDeccel;
    public GameObject goSliderHandle;
    public GameObject goPositionTop;
    public GameObject goPositionBot;
    

    public void PositionSliderHandle() {
        //Position the handle in the appropriate range for fCurVal in {-fMaxVal, +fMaxVal}

        Vector3 v3NewPosition = Vector3.Lerp(goPositionBot.transform.localPosition, goPositionTop.transform.localPosition, fPercentageVal);
        goSliderHandle.transform.localPosition = v3NewPosition;
    }

    public void ChangeVelocity(float fDeltaVelocity) {
        //Modify curVelocity by deltaVelocity
        fCurVelocity += fDeltaVelocity;
    }
    public void DecayVelocity(float fDecayMultiplier) {
        //Zero-out Velocity with a factor of fDecayMultiplier
        fCurVelocity *= fDecayMultiplier;
    }

    public void ChangeVal(float fDeltaVal) {
        //Modify curVal by deltaVal
        fCurVal += fDeltaVal;

        fPercentageVal = (fCurVal + Configurables.inst.fMaxVal) / (2 * Configurables.inst.fMaxVal);

        bool bGameOver = false;

        if(fPercentageVal <= 0) {
            fPercentageVal = 0;
            Debug.Log("Reached 0!");
            bGameOver = true;
        }else if(fPercentageVal >= 1) {
            fPercentageVal = 1;
            Debug.Log("Reached 1!");
            bGameOver = true;
        }

        PositionSliderHandle();

        if (bGameOver == true) GameManager.inst.OnGameOver();
    }

    public void UpdateValFromVelocity() {
        //At the end of each frame, update the position of the value depending on its velocity

        ChangeVal(fCurVelocity);
    }

    public void UpdateVelocity() {
        //At the end of each frame, update the velocity depending on inputs (or by gradually zeroing velocity if there haven't been inputs recently)

        bool bAnyInput = false;

        if (Input.GetKey(keyAccel)) {
            bAnyInput = true;
            ChangeVelocity(Configurables.inst.fVelocityIncrement * Time.deltaTime);
        }
        if (Input.GetKey(keyDeccel)) {
            bAnyInput = true;
            ChangeVelocity(-Configurables.inst.fVelocityIncrement * Time.deltaTime);
        }

        if(bAnyInput == true) {
            fTimeSinceLastInput = 0f;
        } else {
            fTimeSinceLastInput += Time.deltaTime;
            
            if(fTimeSinceLastInput >= Configurables.inst.fZeroingDelay) {
                Debug.Log("Decaying Velocity");
                DecayVelocity(Configurables.inst.fZeroingRate);
            }
        }
    }

    public void OnDisturbanceCollision(Disturbance disturbance) {
        //React to colliding with a disturbance with given intensity (may be positive or negative)

        float fImpact = Configurables.inst.arfValIncrements[disturbance.nIntensity];

        if(disturbance.bPosNeg == false) {
            fImpact *= -1;
        }

        ChangeVal(fImpact);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        UpdateVelocity();

        UpdateValFromVelocity();

    }
}
