using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPool : MonoBehaviour {

	public static EnnemyPool instance;
	
	public EnnemyBehaviour[] pool;


	private void Awake(){
		if (instance == null){
			instance = this;
		} else {
			Destroy(this);
		}
	}
	

	public EnnemyBehaviour CreateObject() {
		bool isThereAnError = false;
		if (pool == null) isThereAnError = true;
		else if (pool.Length == 0) isThereAnError = true;
		if (isThereAnError){
			Debug.LogWarning("Error : invalid bullet pool");
			return null;
		}


		EnnemyBehaviour result = null;

		for(int i = 0; i < pool.Length; i++){
			if (pool[i].isAvailable)
			{
				result = pool[i];
				break;
			}
		}



		result.isAvailable = false;
		return result;
	}

	public void DestroyObject(EnnemyBehaviour ennemyToDestroy){
		ennemyToDestroy.Die();
	}
}
