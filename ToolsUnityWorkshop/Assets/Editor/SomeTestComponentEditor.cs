using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
//[CustomEditor(typeof(SomeTestComponent))]
public class SomeTestComponentEditor : Editor {

	SomeTestComponent inspectedStuff;

	SomeTestComponent[] multipleInspectedItem;

	void OnEnable(){
		inspectedStuff = target as SomeTestComponent;

		multipleInspectedItem = new SomeTestComponent[targets.Length];
		for (int i = 0; i < multipleInspectedItem.Length; i++){
			multipleInspectedItem[i] = targets[i] as SomeTestComponent;

			if (multipleInspectedItem[i].arrayOfColors == null){
				multipleInspectedItem[i].arrayOfColors = new Color[0];
				EditorUtility.SetDirty(multipleInspectedItem[i]);
			}
		}

		if (inspectedStuff.arrayOfColors == null){
			inspectedStuff.arrayOfColors = new Color[0];
			EditorUtility.SetDirty(inspectedStuff);
		}
	}

	public override void OnInspectorGUI(){
		EditorGUI.BeginChangeCheck();
		float newSliderValue = EditorGUILayout.Slider("My Float", inspectedStuff.myFloat, 0f, 1f);
		bool somethingChanged = EditorGUI.EndChangeCheck();
		if (somethingChanged){
		//	Undo.RecordObject(inspectedStuff, "Edition du slider");
		//	inspectedStuff.myFloat = newSliderValue;
			Undo.RecordObjects(multipleInspectedItem, "Edition multiple - slider");
			for (int i = 0; i < multipleInspectedItem.Length; i++){
				multipleInspectedItem[i].myFloat = newSliderValue;
			}
		}



		inspectedStuff.myString = EditorGUILayout.TextField("My String", inspectedStuff.myString);
		inspectedStuff.someObjectReference = EditorGUILayout.ObjectField("A Transform", inspectedStuff.someObjectReference, typeof(Transform), true) as Transform;
		
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("Button 1", EditorStyles.miniButtonLeft)) Debug.Log("You Pressed button 1");
		if (GUILayout.Button("Button 2", EditorStyles.miniButtonMid)) Debug.Log("You Pressed button 2");
		if (GUILayout.Button("Button 3", EditorStyles.miniButtonRight)) Debug.Log("You Pressed button 3");

		EditorGUILayout.EndHorizontal();
		
		int colorArraySize = inspectedStuff.arrayOfColors.Length;
		EditorGUI.BeginChangeCheck();
		colorArraySize = EditorGUILayout.IntField("Color Array Size", colorArraySize);
		if (colorArraySize < 0) colorArraySize = 0;
		bool arrayHasChanged = EditorGUI.EndChangeCheck();

		if (arrayHasChanged){
			Color[] oldArray = new Color[inspectedStuff.arrayOfColors.Length];
			inspectedStuff.arrayOfColors.CopyTo(oldArray, 0);

			inspectedStuff.arrayOfColors = new Color[colorArraySize];
			int minimumLenght = Mathf.Min(oldArray.Length, colorArraySize);
			if (minimumLenght > 0){
				for (int i = 0; i < minimumLenght; i++){
					inspectedStuff.arrayOfColors[i] = oldArray[i];
				}	
			}
		}

		if (colorArraySize > 0){
			for (int i = 0; i < colorArraySize; i++){
				inspectedStuff.arrayOfColors[i] = EditorGUILayout.ColorField("Color" + i.ToString(), inspectedStuff.arrayOfColors[i]);
			} 
		}

		if (GUILayout.Button("Add AudioSource"))
			Undo.AddComponent<AudioSource>(inspectedStuff.gameObject);

		if (GUILayout.Button("Create Child Object")){
			GameObject go = new GameObject("Child");
			go.transform.SetParent(inspectedStuff.transform);
			go.transform.localPosition = Vector3.zero;
			go.transform.localEulerAngles = Vector3.zero;
			go.transform.localScale = Vector3.one;
			Undo.RegisterCreatedObjectUndo(go, "Created Child");
		}

		for (int i = 0; i < multipleInspectedItem.Length; i++){
			EditorUtility.SetDirty(multipleInspectedItem[i]);
		}
	}
}
