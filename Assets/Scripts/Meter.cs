using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour {

    //Game-Running values
    public float fCurVal;
    public float fCurVelocity;
    public float fTimeSinceLastInput;
    public float fPercentageVal; //An easier to interpret scale for where the slider should appear

    public float fTimeInExtreme;

    //Configuration - Set and Forget
    public KeyCode keyAccel;
    public KeyCode keyDeccel;

    public const float fRectFillWidth = 0.6f;
    public const float fMaxRectFillSacle = 2.5f;
    public RectTransform rectFill;
    

    public void SetFullness() {
        //Position the handle in the appropriate range for fCurVal in {-fMaxVal, +fMaxVal}
        
        rectFill.sizeDelta = new Vector2(fRectFillWidth, Mathf.Lerp(0, fMaxRectFillSacle, fPercentageVal));
    }

    public void ChangeVelocity(float fDeltaVelocity) {
        //Modify curVelocity by deltaVelocity
        fCurVelocity += fDeltaVelocity;
    }
    public void DecayVelocity(float fDecayMultiplier) {
        //Zero-out Velocity with a factor of fDecayMultiplier
        fCurVelocity *= fDecayMultiplier;
    }

    public void CheckHittingExtremes() {
        
        bool bHitExtreme = false;
        if (fCurVal <= -Configurables.inst.fMaxVal) {
            fCurVal = -Configurables.inst.fMaxVal;
            bHitExtreme = true;
        } else if (fCurVal >= Configurables.inst.fMaxVal) {
            fCurVal = Configurables.inst.fMaxVal;
            bHitExtreme = true;
        }

        if (bHitExtreme) {
            if (fTimeInExtreme == 0f || fTimeInExtreme >= Configurables.inst.fComboJerkDelay) {
                Debug.Log("Hit Extreme");
                Score.inst.HitExtreme();

                fTimeInExtreme = 0f; //reset to 0 in case we've just been chilling in the extremes for a while to penalize again
            }

            fTimeInExtreme += Time.deltaTime;

        } else {
            fTimeInExtreme = 0f;
        }
    }

    public void ChangeVal(float fDeltaVal) {
        //Modify curVal by deltaVal
        fCurVal += fDeltaVal;

        //Check if reaching one of the extremes
        CheckHittingExtremes();

        fPercentageVal = (fCurVal + Configurables.inst.fMaxVal) / (2 * Configurables.inst.fMaxVal);

        SetFullness();
        
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

        ChangeVelocity(fImpact);
    }

    public bool IsSweetspot() {
        return Mathf.Abs(fPercentageVal - 0.5f) <= Configurables.inst.fSweetspotWidth;
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
