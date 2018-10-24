using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SomeTestComponent))]
public class SomeBetterInspector : Editor {

	SerializedProperty myFloat, myString, myTransform, colorArray;

	// Use this for initialization
	void OnEnable(){
		myFloat = serializedObject.FindProperty("myFloat");
		myString = serializedObject.FindProperty("myString");
		myTransform = serializedObject.FindProperty("someObjectReference");
		colorArray = serializedObject.FindProperty("arrayOfColors");
	}

	public override void OnInspectorGUI(){
		serializedObject.Update();

		if (serializedObject.isEditingMultipleObjects){
			EditorGUILayout.HelpBox("Warning", MessageType.Warning);
		}

		Color defaultColor = GUI.color;
		if(myFloat.hasMultipleDifferentValues){
			GUI.color = Color.yellow;
		}

		EditorGUILayout.PropertyField(myFloat);
		GUI.color = defaultColor;
		EditorGUILayout.PropertyField(myString);
		EditorGUILayout.PropertyField(myTransform);
		
		//Array Display
		EditorGUI.BeginChangeCheck();
		int newArraySize = EditorGUILayout.IntField("Color Array Size", colorArray.arraySize);
		if (newArraySize < 0) newArraySize = 0;
		if (EditorGUI.EndChangeCheck()){
			colorArray.arraySize = newArraySize;
		}

		if (colorArray.arraySize > 0 ){
			for (int i = 0; i < colorArray.arraySize; i++){
				EditorGUILayout.PropertyField(colorArray.GetArrayElementAtIndex(i));
			}
		}

		serializedObject.ApplyModifiedProperties();
	}
}
