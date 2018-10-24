using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuestCreator : EditorWindow {

    TextAsset myCsvFile;

    [MenuItem("Window/Quest Creator")]
    public static void Init()
    {
        QuestCreator qc = EditorWindow.GetWindow<QuestCreator>();
        qc.Show();
    }

    private void OnGUI()
    {
        myCsvFile = EditorGUILayout.ObjectField("Quest Asset", myCsvFile, typeof(TextAsset), false) as TextAsset;

        if (myCsvFile != null)
            if (GUILayout.Button("Read CSV and generate quests ! "))
                GenerateQuests();
    }

    void GenerateQuests()
    {
        string fullText = myCsvFile.text;

        string[] lineSeparators = new string[] { "\n", "\r", "\n\r", "\r\n" };
        char[] cellSeparators = new char[] { ';' };

        string[] lines = fullText.Split(lineSeparators, System.StringSplitOptions.RemoveEmptyEntries);

        List<string[]> completeExcelFile = new List<string[]>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(cellSeparators, System.StringSplitOptions.RemoveEmptyEntries);
            completeExcelFile.Add(cells);

            // Ne pas traiter le header
            if (i > 0)
                CreateQuestScriptableObjects(cells);
        }
    }

    void CreateQuestScriptableObjects(string[] dataLine)
    {
        QuestObject qo = ScriptableObject.CreateInstance<QuestObject>();

        qo.index = int.Parse(dataLine[0]);
        qo.title = dataLine[1];
        qo.narration = dataLine[2];
        qo.questType = (QuestType)System.Enum.Parse(typeof(QuestType), dataLine[3].Replace(" ", ""), false);
        qo.questAmount = int.Parse(dataLine[4]);
        qo.rewardType = (RewardType)System.Enum.Parse(typeof(RewardType), dataLine[5].Replace( " ", ""));
        qo.rewardAmount = int.Parse(dataLine[6]);


        //S'assurer que l'emplacement existe


        // Créer le fichier au bon emplacement
        AssetDatabase.CreateAsset(qo, "Assets/Quests/GeneratedQuests" + qo.index.ToString() + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
    }
}
