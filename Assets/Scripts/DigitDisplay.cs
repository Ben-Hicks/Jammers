using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitDisplay : MonoBehaviour {



    public SpriteRenderer sprrenDigit;


    public void DisplayDigit(int nDigit) {

        sprrenDigit.sprite = Score.inst.arsprDigits[nDigit];

    }

}
