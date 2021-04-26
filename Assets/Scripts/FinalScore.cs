using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour {

    public DigitDisplay[] arDigits;

    public void UpdateScoreGraphics() {

        int nScoreToRender = Mathf.FloorToInt(Score.inst.fScore);

        for (int i = 0; i < arDigits.Length; i++) {
            arDigits[i].DisplayDigit(nScoreToRender % 10);
            nScoreToRender /= 10;
        }

    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
