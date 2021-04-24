using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int nDifficulty;

    public static GameManager inst; //A 'singleton' reference to the GameManager

    //If we want any startup sequence stuff we can put it here

    public void OnGameOver() {
        //Spawn some game over graphic stuff and pause normal execution

    }


    public void Awake() {
        inst = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
