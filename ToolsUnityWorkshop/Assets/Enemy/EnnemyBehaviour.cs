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
	public BulletEmitterEnemy emitter;
    public EnemyPath pathFollow;


    void Start()
    {
        for (int i = 0; i < pathFollow.pathPoints.Length; i++)
        {
           pathFollow.pathPoints[i] = new Vector3(Random.Range(-16.5f, 16.5f), Random.Range(-5f, 10.3f), -8);
        }

        transform.position = pathFollow.pathPoints[0];
        spriteRenderer.enabled = true;
 
    }


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
        if (isAvailable) return;

        emitter.isEnabled = true;
    }

	public void Die(){
		spriteRenderer.enabled = false;
		isAvailable = true;
	}
}

