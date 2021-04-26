using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour {

    //Not currently doing anything with this

    public FinalScore finalscore;

    public void OnGameOver() {
        finalscore.UpdateScoreGraphics();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
