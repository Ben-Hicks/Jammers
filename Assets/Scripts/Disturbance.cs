using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disturbance : MonoBehaviour {

    //Configuration set by constructor
    public bool bPosNeg; //true if the impact would increase the value
    public int nIntensity; //positive number representing the impact of the disturbance
    public int nMeter; //Which meter this disturbance will affect (1, 2, or 3)
    public float fTimeToReachCenter; //How long to reach the center

    //Hookups in the prefab for graphics
    public SpriteRenderer sprrenBackground;
    public SpriteRenderer sprrenShadow;
    public SpriteRenderer sprrenNumber;

    //Internal use
    public float fTimeAlive; //How long the disturbance has been spawned
    public float fProgressToCenter; //a [0,1] percentage of the progress
    public Vector3 v3InitialPosition; //The initial position where this was spawned
    public float fRotationSpeed;

    public void Init(bool _bPosNeg, int _nIntensity, int _nMeter, float _fTimeToReachCenter) {
        bPosNeg = _bPosNeg;
        nIntensity = _nIntensity;
        nMeter = _nMeter;
        fTimeToReachCenter = _fTimeToReachCenter;

        fRotationSpeed = Random.Range(Configurables.inst.fMinDisturbanceSpin, Configurables.inst.fMaxDisturbanceSpin);
        if (Random.Range(0, 100) < 50) fRotationSpeed *= -1;

        SetIntensityGraphic();
        SetBackgroundGraphic();
    }

    public void SetIntensityGraphic() {

        string sSprPath = "Sprites/Intensities/" + nIntensity;

        if (bPosNeg) {
            sSprPath += "Up";
        } else {
            sSprPath += "Down";
        }

        if (nMeter == 1) {
            sSprPath += "Green";
        } else if (nMeter == 2) {
            sSprPath += "Blue";
        } else {
            sSprPath += "Purple";
        }

        Sprite sprIntensity = Resources.Load(sSprPath, typeof(Sprite)) as Sprite;

        Debug.Assert(sprIntensity != null, "Could not find specificed sprite: " + sSprPath);

        sprrenNumber.sprite = sprIntensity;
    }

    public void SetBackgroundGraphic() {

        string sSprPath = "Sprites/DistBackgrounds/";

        if (nMeter == 1) {
            sSprPath += "Green";
        } else if (nMeter == 2) {
            sSprPath += "Blue";
        } else {
            sSprPath += "Purple";
        }

        sSprPath += "Life";

        if (bPosNeg) {
            sSprPath += "Up";
        } else {
            sSprPath += "Down";
        }

        Sprite sprBackground = Resources.Load(sSprPath, typeof(Sprite)) as Sprite;

        Debug.Assert(sprBackground != null, "Could not find specificed sprite: " + sSprPath);

        sprrenBackground.sprite = sprBackground;
        sprrenShadow.sprite = sprBackground;
    }

    public void SetPosition() {

        fProgressToCenter = fTimeAlive / fTimeToReachCenter;

        Vector3 v3NewPosition = Vector3.Lerp(v3InitialPosition, Vector3.zero, fProgressToCenter);

        this.transform.localPosition = v3NewPosition;

        if(fProgressToCenter >= 1) {
            OnCollision();
        }

    }

    public void RotateBackground() {
        Vector3 euler = sprrenBackground.transform.localEulerAngles;
        euler.z += fRotationSpeed * Time.deltaTime;
        sprrenBackground.transform.localEulerAngles = euler;

        euler = sprrenShadow.transform.localEulerAngles;
        euler.z -= Configurables.inst.fDisturbanceSpinRatio * fRotationSpeed * Time.deltaTime;
        sprrenShadow.transform.localEulerAngles = euler;
    }

    public void OnCollision() {
        //What to do when this disturbance collides with the sleeper

        Monitor.inst.arMeters[nMeter-1].OnDisturbanceCollision(this);

        GameObject.Destroy(this.gameObject);
    }

    public void SetRandomBackgroundRotation() {
        //Taken from
        //https://answers.unity.com/questions/790877/random-rotation-in-z-axis.html
        Vector3 euler = sprrenBackground.transform.localEulerAngles;
        euler.z = Random.Range(0f, 360f);
        sprrenBackground.transform.localEulerAngles = euler;

        euler = sprrenShadow.transform.localEulerAngles;
        euler.z = Random.Range(0f, 360f);
        sprrenShadow.transform.localEulerAngles = euler;
    }

    // Start is called before the first frame update
    void Start() {
        v3InitialPosition = this.transform.localPosition;

        SetRandomBackgroundRotation();
    }

    // Update is called once per frame
    void Update() {

        fTimeAlive += Time.deltaTime;

        SetPosition();

        RotateBackground();

    }
}
