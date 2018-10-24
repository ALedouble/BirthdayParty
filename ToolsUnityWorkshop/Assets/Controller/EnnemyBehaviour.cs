using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehaviour : MonoBehaviour {

	public EnnemyParams currentParams;
	
	[System.NonSerialized]
	public bool isAvailable;

	[Header ("References")]
	public Transform self;
	public SpriteRenderer spriteRenderer; 
	public BulletEmitter emitter;
    public EnemyPath pathFollow;




    private void Awake(){
		Die();
	}


	public void Birth(){
		spriteRenderer.enabled = true;
        //self.position = Vector3.zero;
		isAvailable = false;
	}
	
	public void ApplyParameters(EnnemyParams ep){
		currentParams = ep;
		spriteRenderer.color = ep.color;
	}

    // Update is called once per frame
    void Update()
    {
        Birth();
        if (isAvailable) return;
    }

	public void Die(){
		spriteRenderer.enabled = false;
		isAvailable = true;
	}
}

