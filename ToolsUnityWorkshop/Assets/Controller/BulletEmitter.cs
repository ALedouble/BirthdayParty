using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour {

	public Transform self;
	public int numberOfSimultaneousBullets;
	public float amplitudeAngle;
	private float currentCooldown;
	public float firingRate;
	public BulletParams bulletType;


    [SerializeField]
    public Transform player;
	[System.NonSerialized]
	public bool isEnabled;

    public void Update(){
		currentCooldown -= Time.deltaTime;

		if (isEnabled && currentCooldown < 0){
			currentCooldown = firingRate;
			EmitSingleBullet();
		}

    


        //if (Time.time % firingRate < Time.deltaTime)
        //	EmitSingleBullet();
    }

	public void EmitSingleBullet(){
		float angleDifferenceBetweenBullets = 0;
		if (numberOfSimultaneousBullets > 1){
			angleDifferenceBetweenBullets = amplitudeAngle / (numberOfSimultaneousBullets - 1);
		}

		float angleOfFirstBullet = -0.5f * amplitudeAngle;
		

		for(int i = 0; i < numberOfSimultaneousBullets; i++){
			Bullet bullet = BulletPool.instance.CreateObject();
            bullet.self.position = Vector3.up;

			// Orienter le projectile
			bullet.self.position = self.position;
			bullet.self.eulerAngles = new Vector3(0, 0, angleOfFirstBullet + i * angleDifferenceBetweenBullets);

			bullet.ApplyParameters(bulletType);

			bullet.Birth();
		}
		
	}


	// Use this for initialization
	

}


