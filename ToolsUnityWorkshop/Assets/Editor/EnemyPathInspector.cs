using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyPath))]
public class EnemyPathInspector : Editor {

	Color testColor;
	Transform inspectedObject;

    EnemyPath enemyPath;
	Color gizmosColor = Color.yellow;
	bool foldout;
    SerializedProperty pathPoints;
    int speed;

	private void OnEnable(){
		foldout = false;
		enemyPath = target as EnemyPath;
        pathPoints = serializedObject.FindProperty("pathPoints");
    }

	public override void OnInspectorGUI(){
        // base.OnInspectorGUI();
        serializedObject.Update();

		foldout = EditorGUILayout.Foldout(foldout, "Display Transform", true);
		if (foldout){
			EditorGUI.indentLevel++;
			enemyPath.self = EditorGUILayout.ObjectField("Self", enemyPath.self, typeof(Transform), true) as Transform;
            enemyPath.ennemies = EditorGUILayout.ObjectField("Enemy", enemyPath.ennemies, typeof(Transform), true) as Transform;
            EditorGUI.indentLevel--;
		}


		/* if (enemyPath.self == null){
			enemyPath.self = enemyPath.transform;
		}*/

		EditorGUILayout.LabelField("Définition du chemin de la vague 1", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        int newArraySize = EditorGUILayout.IntField("Points du path", pathPoints.arraySize);
        if (newArraySize < 0) newArraySize = 0;
        if (EditorGUI.EndChangeCheck())
        {
            pathPoints.arraySize = newArraySize;
        }

        if (pathPoints.arraySize > 0)
        {
            for (int i = 0; i < pathPoints.arraySize; i++)
            {
                EditorGUILayout.PropertyField(pathPoints.GetArrayElementAtIndex(i));
            }
        }

        if (pathPoints.arraySize == 0)
        {

        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(enemyPath);
        
    }

	private void OnSceneGUI(){
		if (!enemyPath.self) return;

		float handleSize = 0.1f * HandleUtility.GetHandleSize(enemyPath.self.position);
		Handles.color = Color.red;
		GUIStyle textStyle = new GUIStyle(EditorStyles.label);
		textStyle.alignment = TextAnchor.MiddleCenter;
		textStyle.normal.textColor = Color.white;
        textStyle.fontSize = 10;

        /**/
        for (int i = 0; i < enemyPath.pathPoints.Length; i++){
            if (enemyPath.pathPoints[i].x > 18 && enemyPath.pathPoints[i].x > -18) enemyPath.pathPoints[i].x = 18;
            if (enemyPath.pathPoints[i].x < -18) enemyPath.pathPoints[i].x = -18;

            if (enemyPath.pathPoints[i].y > 10 ) enemyPath.pathPoints[i].y = 10;
            if (enemyPath.pathPoints[i].y < -1) enemyPath.pathPoints[i].y = -1;

            enemyPath.pathPoints[i].z = -8;
			enemyPath.pathPoints[i] = Handles.FreeMoveHandle(enemyPath.pathPoints[i], Quaternion.identity, handleSize, Vector3.zero, Handles.SphereHandleCap);
			//enemyPath.pathPoints[i] = Handles.PositionHandle(enemyPath.pathPoints[i], enemyPath.self.rotation);
			Handles.Label(enemyPath.pathPoints[i], "Point #" + i.ToString(), textStyle);
		}

		Handles.DrawPolyLine(enemyPath.pathPoints);
		Handles.DrawLine(enemyPath.pathPoints[0], enemyPath.pathPoints[enemyPath.pathPoints.Length - 1]);

		textStyle.fontStyle = FontStyle.Bold;



		Handles.BeginGUI();
			GUILayout.BeginArea(new Rect(100, 20, 400, 200));
				GUILayout.Label("Les modifications du path ne sont pas prises en compte en jeu!! ", textStyle);
				
            
            GUILayout.EndArea();

		Handles.EndGUI();

        /**/
        EditorUtility.SetDirty(enemyPath);
    }
}
