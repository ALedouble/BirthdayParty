using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[System.NonSerialized]
	public bool isAvailable;

    [Header("References")]
	public Transform self;
	public SpriteRenderer spriteRenderer;
    public CircleCollider2D collid;

    [Header("Values")]
    public float speed;
	public float lifespan;
	private float timeleft;
    public int id = 0;

    [Header("Scripts")]
    public BulletParams currentParams;
    public BulletEmitter emitter;
    public BulletEmitterEnemy enemy;
    
   



	private void Awake(){
		Die();
	}

	// Use this for initialization
	void Start () {
		
	}

	public void Birth(){
        if (self != null)
        {
            timeleft = currentParams.lifespan;
            spriteRenderer.enabled = true;
            collid.enabled = true;
            isAvailable = false;
        }
	}
	
	public void ApplyParameters(BulletParams bp){
        if (self != null) { 
            emitter.amplitudeAngle = bp.amplitudeAngle;
            emitter.numberOfSimultaneousBullets = bp.numberOfSimultaneousBullets;
            emitter.firingRate = bp.firingRate;
		    currentParams = bp;
		    spriteRenderer.color = bp.color;
		    self.localScale = Vector3.one * bp.size;
        }
    }

	// Update is called once per frame
	void Update() {
		if (isAvailable) return;

		self.Translate(self.up * currentParams.speed * Time.deltaTime);
        timeleft -= Time.deltaTime;
		if (timeleft < 0) Die();
	}

	public void Die(){
		spriteRenderer.enabled = false;
        collid.enabled = false;
		isAvailable = true;
	}
}
