using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitDisplay : MonoBehaviour {



    public SpriteRenderer sprrenDigit;
    public DigitCollection digitCollection;

    public void DisplayDigit(int nDigit) {

        sprrenDigit.sprite = digitCollection.GetDigitSprite(nDigit);

    }

}
