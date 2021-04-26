using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanel : MonoBehaviour {

    public static readonly KeyCode[] arControls = { KeyCode.Q, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.E, KeyCode.D };

    public void LookForKeyInput() {

        for (int i = 0; i < arControls.Length; i++) {
            if (Input.GetKeyUp(arControls[i]) ) {
                GameManager.inst.OnStartGame();

            }
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
