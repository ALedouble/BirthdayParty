using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BulletPool))]
public class BulletPoolInspector : Editor {
	SerializedProperty pool, bulletPrefab; 
	BulletPool inspectedComponent;
	int objectsToAddOrRemove;

	void OnEnable(){
		pool = serializedObject.FindProperty("pool");
		bulletPrefab = serializedObject.FindProperty("bulletPrefab");

		inspectedComponent = target as BulletPool;
	}

	public override void OnInspectorGUI(){
		int numberOfObjects = pool.arraySize;
		EditorGUILayout.PropertyField(bulletPrefab);
		//bulletPrefab.objectReferenceValue = EditorGUILayout.ObjectField("Bullet Prefab", bulletPrefab.objectReferenceValue, typeof(GameObject), false);
		EditorGUILayout.LabelField(" The pool contains " + numberOfObjects.ToString() + " objects.");

		objectsToAddOrRemove = EditorGUILayout.IntField(" Add or Remove objects : ", objectsToAddOrRemove);
		if (objectsToAddOrRemove < 0) objectsToAddOrRemove = 0;

		EditorGUILayout.BeginHorizontal();
		
		if (GUILayout.Button("Remove 100")) RemoveObjects(objectsToAddOrRemove);
		if (GUILayout.Button("Add 100")) AddObjects(objectsToAddOrRemove);

		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();
	}

	void RemoveObjects(int amount){
		Debug.Log("Removing objets ..");

		amount = Mathf.Min(amount, inspectedComponent.pool.Length);

		//...
		for (int i = inspectedComponent.pool.Length-1; i > inspectedComponent.pool.Length-(amount+1); i--){
			DestroyImmediate(inspectedComponent.pool[i].gameObject);
		}
		
		Bullet[] bullets = inspectedComponent.GetComponentsInChildren<Bullet>();
		inspectedComponent.pool = bullets;
		EditorUtility.SetDirty(inspectedComponent);

		Debug.Log("Objects removed");
	}

	void AddObjects(int amount){
		Debug.Log("Adding Objects .. ");

		for (int i = 0; i < amount; i++){
			GameObject go = PrefabUtility.InstantiatePrefab(bulletPrefab.objectReferenceValue) as GameObject;
			go.name = "Bullet " +(inspectedComponent.pool.Length + i).ToString();
			go.transform.SetParent(inspectedComponent.transform);
			go.transform.localPosition = go.transform.localEulerAngles = Vector3.zero;
			go.transform.localScale = Vector3.one;
		}

		Bullet[] bullets = inspectedComponent.GetComponentsInChildren<Bullet>();
		inspectedComponent.pool = bullets;
		EditorUtility.SetDirty(inspectedComponent);

		Debug.Log("Objects Added");
	}
}
