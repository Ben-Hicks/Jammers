using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanel : MonoBehaviour {


    public void LookForKeyInput() {

        if (Input.anyKeyDown) {
            GameManager.inst.OnStartGame();
            
        }

    }


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        LookForKeyInput();
    }
}
