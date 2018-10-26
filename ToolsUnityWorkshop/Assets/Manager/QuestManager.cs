using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    public static QuestManager instance;
    Text quest;
    public QuestObject[] questArray;
    public int questNumber;

    // Use this for initialization
    void Start () {
        questNumber = 0;
        quest = GetComponent<Text>();
    }

    void Update()
    {
        quest.text = questArray[questNumber].title.ToString() + " : " + questArray[questNumber].narration.ToString();

        if (StatsManager.scoreValue >= 30)
        {
            quest.text = " Congrats !! You finished the quests !!" ;
        }
    }
}
