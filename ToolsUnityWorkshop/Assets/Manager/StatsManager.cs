using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

    public static StatsManager instance;
    public static int scoreValue;
    public static int lifeValue = 100;
    public QuestManager qm;
    Text score;

    void Start()
    {
        score = GetComponent<Text>();
    }

	void Update () {
        score.text = " Score : " + scoreValue + "\n" + " Life : " + lifeValue;

        if (lifeValue <= 0)
        {
            score.text = " Score : " + scoreValue + "\n" + "You're dead";
        }

        if (scoreValue >= 10)
        {
            qm.questNumber = 1;
        }
    }
}
