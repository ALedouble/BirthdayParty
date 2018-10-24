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
			EditorGUI.indentLevel--;
		}


		/* if (enemyPath.self == null){
			enemyPath.self = enemyPath.transform;
		}*/

		EditorGUILayout.LabelField("Ceci est mon custom editor", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        int newArraySize = EditorGUILayout.IntField("Color Array Size", pathPoints.arraySize);
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

        /**/
        for (int i = 0; i < enemyPath.pathPoints.Length; i++){
			enemyPath.pathPoints[i] = Handles.FreeMoveHandle(enemyPath.pathPoints[i], Quaternion.identity, handleSize, Vector3.zero, Handles.SphereHandleCap);
			//enemyPath.pathPoints[i] = Handles.PositionHandle(enemyPath.pathPoints[i], enemyPath.self.rotation);
			Handles.Label(enemyPath.pathPoints[i], "Point #" + i.ToString(), textStyle);
		}

		Handles.DrawPolyLine(enemyPath.pathPoints);
		Handles.DrawLine(enemyPath.pathPoints[0], enemyPath.pathPoints[enemyPath.pathPoints.Length - 1]);

		textStyle.fontStyle = FontStyle.Bold;



		Handles.BeginGUI();
			GUILayout.BeginArea(new Rect(100, 20, 400, 200));
				GUILayout.Label("Hello World", textStyle);
				if(GUILayout.Button("Reset path", GUILayout.MaxWidth(200))){
					for(int i = 0; i < enemyPath.pathPoints.Length; i++ ){
						//enemyPath.pathPoints[i] = Vector3.zero;
						enemyPath.pathPoints[i] = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), -8);
					} 
				}
            
            GUILayout.EndArea();

		Handles.EndGUI();

        /**/
        EditorUtility.SetDirty(enemyPath);
    }
}
