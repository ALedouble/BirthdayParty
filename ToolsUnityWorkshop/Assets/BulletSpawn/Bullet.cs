using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[System.NonSerialized]
	public bool isAvailable;
	public Transform self;
	public SpriteRenderer spriteRenderer;

	public float speed;
	public float lifespan;
	private float timeleft;
	public BulletParams currentParams;




	private void Awake(){
		Die();
	}

	// Use this for initialization
	void Start () {
		
	}

	public void Birth(){
		timeleft = currentParams.lifespan;
		spriteRenderer.enabled = true;
		isAvailable = false;
	}
	
	public void ApplyParameters(BulletParams bp){
		currentParams = bp;
		spriteRenderer.color = bp.color;
		self.localScale = Vector3.one * bp.size;
	}

	// Update is called once per frame
	void Update () {
		if (isAvailable) return;

		self.Translate(self.up * currentParams.speed * Time.deltaTime);
		timeleft -= Time.deltaTime;
		if (timeleft < 0) Die();
	}

	public void Die(){
		spriteRenderer.enabled = false;
		isAvailable = true;
	}
}
