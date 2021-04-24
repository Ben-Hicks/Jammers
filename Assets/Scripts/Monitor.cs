using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour {

    public Meter[] arMeters;

    public static Monitor inst; //a static 'singleton' reference so this can be quickly accessed externally




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
