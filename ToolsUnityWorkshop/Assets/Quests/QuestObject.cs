using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType { Score, ExtraHealth }

public enum QuestType { KillMonsters, Survival, DodgeBullets }

[CreateAssetMenu(fileName = "NewQuest.asset", menuName = "Quest", order = 100)]
public class QuestObject : ScriptableObject {

    public static QuestObject instance;

    public int index;
    public string title;
    [Multiline]
    public string narration;

    public QuestType questType;
    public int questAmount;

    public RewardType rewardType;
    public int rewardAmount;


}
