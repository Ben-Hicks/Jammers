using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public static Score inst;

    public SpriteRenderer sprrenMultiplier;

    // internal use
    public float fMultiplier;
    public float fScore;


    public void HitExtreme() {
        fMultiplier -= Configurables.inst.fComboJerkPenalty;

        if (fMultiplier < 1f) fMultiplier = 1f;

        UpdateMultiplierGraphics();
    }

    public void IncreaseScore() {

        fScore += Configurables.inst.fPassiveScoreIncrease * Mathf.Floor(fMultiplier) * Time.deltaTime;

        UpdateScoreGraphics();
    }

    public void UpdateMultiplier() {
        float fMultiplierChange = -Configurables.inst.fPassiveComboDrain;

        for(int i=0; i< Monitor.inst.arMeters.Length; i++) {
            if (Monitor.inst.arMeters[i].IsSweetspot()) {
                fMultiplierChange += Configurables.inst.fSweetspotComboIncrease;
            }
        }

        fMultiplier += fMultiplierChange * Time.deltaTime;

        if(fMultiplier < 1f) {
            fMultiplier = 1f;
        }else if (fMultiplier > 4f) {
            fMultiplier = 4f;
        }

        UpdateMultiplierGraphics();
    }

    public void UpdateMultiplierGraphics() {
        string sSprPath = "Sprites/Multipliers/SleepPatternx";

        sSprPath += Mathf.FloorToInt(fMultiplier);

        Sprite sprMultiplier = Resources.Load(sSprPath, typeof(Sprite)) as Sprite;

        Debug.Assert(sprMultiplier != null, "Could not find specificed sprite: " + sSprPath);

        sprrenMultiplier.sprite = sprMultiplier;
    }

    public void UpdateScoreGraphics() {

    }

    private void Awake() {
        inst = this;
    }

    // Start is called before the first frame update
    void Start() {
        fMultiplier = 1;
    }

    // Update is called once per frame
    void Update() {

        UpdateMultiplier();
        IncreaseScore();

    }
}
