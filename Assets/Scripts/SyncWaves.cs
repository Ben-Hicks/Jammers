using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncWaves : MonoBehaviour {

    public GameObject goWaves;
    public GameObject goFrontWave;
    public GameObject goBackWave;

    public float fVel;
    public float fVelFront;
    public float fVelBack;

    public const float fWaveLength = 9f;
    public float fTargetGap;

    public static SyncWaves inst;

    public void CheckForCycling() {
        if(goWaves.transform.localPosition.x <= -fWaveLength) {
            CycleWaves();
        }
    }

    public void CycleWaves() {
        ShiftBothWaves(fWaveLength);
    }

    public void OnMultiplierChange(int nNewMultiplier) {
        fTargetGap = Configurables.inst.arfSyncGaps[nNewMultiplier - 1];
    }

    public void CheckIfIndividualWavesNeedShifting() {
        if(goFrontWave.transform.localPosition.x == fTargetGap) {
        } else if(goFrontWave.transform.localPosition.x < fTargetGap){
            ShiftIndividualWaves(Configurables.inst.fResyncVelocity * Time.deltaTime);
        } else {
            ShiftIndividualWaves(-Configurables.inst.fResyncVelocity * Time.deltaTime);
        }
    }

    public void ShiftIndividualWaves(float fAmount) {

        float fNewFrontX = goFrontWave.transform.localPosition.x + fAmount;
        float fNewBackX = goBackWave.transform.localPosition.x - fAmount;

        //Check if we're expanding the gap and have expanded far enough
        if (fAmount >= 0 && fNewFrontX >= fTargetGap) {
            fNewFrontX = fTargetGap;
            fNewBackX = -fTargetGap;
        }else if (fAmount <= 0 && fNewFrontX <= fTargetGap) {
            //Check if we're shrinking and have shrunk enough
            fNewFrontX = fTargetGap;
            fNewBackX = -fTargetGap;
        }

        goFrontWave.transform.localPosition = new Vector3(fNewFrontX, goFrontWave.transform.localPosition.y, goFrontWave.transform.localPosition.z);
        goBackWave.transform.localPosition = new Vector3(fNewBackX, goBackWave.transform.localPosition.y, goBackWave.transform.localPosition.z);

    }

    public void ShiftBothWaves(float fAmount) {
        float fNewX = goWaves.transform.localPosition.x + fAmount;

        goWaves.transform.localPosition = new Vector3(fNewX, goWaves.transform.localPosition.y, goWaves.transform.localPosition.z);
    }

    public void Awake() {
        inst = this;
    }

    // Start is called before the first frame update
    void Start() {
        fVel = Configurables.inst.fSyncVelocity;

        OnMultiplierChange(1);
        goFrontWave.transform.localPosition = new Vector3(goFrontWave.transform.localPosition.x + fTargetGap, goFrontWave.transform.localPosition.y, goFrontWave.transform.localPosition.z);
        goBackWave.transform.localPosition = new Vector3(goBackWave.transform.localPosition.x - fTargetGap, goBackWave.transform.localPosition.y, goBackWave.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update() {

        ShiftBothWaves(fVel * Time.deltaTime);

        CheckIfIndividualWavesNeedShifting(); 

        CheckForCycling();
    }
}
