using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {
	
	public static BulletPool instance;
	
	public GameObject bulletPrefab;
	public Bullet[] pool;


	private void Awake(){
		if (instance == null){
			instance = this;
		} else {
			Destroy(this);
		}
	}
	

	public Bullet CreateObject() {

		bool isThereAnError = false;
		if (pool == null) isThereAnError = true;
		else if (pool.Length == 0) isThereAnError = true;
		if (isThereAnError){
			Debug.LogWarning("Error : invalid bullet pool");
			return null;
		}


		Bullet result = null;

		for(int i = 0; i < pool.Length; i++){
			if (pool[i].isAvailable)
			{
				result = pool[i];
                break;
                

            }
        }



		if (result) result.isAvailable = false;
		return result;
	}

	public void DestroyObject(Bullet bulletToDestroy){
		bulletToDestroy.Die();
	}
}
