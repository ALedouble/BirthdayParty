using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BulletParamCreator  {


	[MenuItem("Assets/Create/BulletParams", false, 21)]
	public static void CreateBulletParamAsset(){
		BulletParams bp = ScriptableObject.CreateInstance<BulletParams>();


		string path = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/MyFirstBullet.asset");
		AssetDatabase.CreateAsset(bp, path);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	[MenuItem("Assets/Create/EnnemyParams")]
	public static void CreateEnemyParams(){
		EnnemyParams ep = ScriptableObject.CreateInstance<EnnemyParams>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "") path = "Assets";
		string fileName = "New EnnemyParams.asset";
		path += "/" + fileName;

		path = AssetDatabase.GenerateUniqueAssetPath(path);

		AssetDatabase.CreateAsset(ep, path);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	[MenuItem("MyCustomStuff/Browse scene and do stuff")]
	public static void BrowseSceneAndDoStuff(){
		GameObject[] all = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < all.Length; i++){
            if (!all[i].GetComponent<EnnemyBehaviour>())
            {
                continue;
            }
            EnnemyBehaviour eb = all[i].GetComponent<EnnemyBehaviour>();
            EnemyPath ep = GameObject.FindObjectOfType(typeof(EnemyPath)) as EnemyPath;
            eb.pathFollow = ep;

            EditorUtility.SetDirty(eb);
        }

		Debug.Log("done successfully!");
	}
}
