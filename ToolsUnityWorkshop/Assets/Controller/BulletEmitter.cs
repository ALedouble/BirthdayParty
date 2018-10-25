using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour {

	public Transform self;
	public int numberOfSimultaneousBullets;
	public float amplitudeAngle;
	private float currentCooldown;
	public float firingRate;
	public BulletParams[] bulletType;
    private int currentParams;
    public bool isEnemyBullet = false;

    [System.NonSerialized]
	public bool isEnabled;


    public void Update(){
		currentCooldown -= Time.deltaTime;

        if (isEnabled && currentCooldown < 0)
        {
            currentCooldown = firingRate;
            EmitSingleBullet();
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (currentParams >= 0 && currentParams < bulletType.Length - 1)
            {
                currentParams += 1;
            }
            else
            {
                currentParams = 0;
            }
 
        }
       
       
        //if (Time.time % firingRate < Time.deltaTime)
        //	EmitSingleBullet();
    }


    public void EmitSingleBullet(){
    float angleDifferenceBetweenBullets = 0;
	if (bulletType[currentParams].numberOfSimultaneousBullets > 1){
		angleDifferenceBetweenBullets = bulletType[currentParams].amplitudeAngle / (bulletType[currentParams].numberOfSimultaneousBullets - 1);
	}

	float angleOfFirstBullet = -0.5f * bulletType[currentParams].amplitudeAngle;
		
	for(int i = 0; i < bulletType[currentParams].numberOfSimultaneousBullets; i++)
        {
		    Bullet bullet = BulletPool.instance.CreateObject();

            bullet.self.position = Vector3.up;
            bullet.id = 0;

            // Orienter le projectile
            bullet.self.position = self.position;
		    bullet.self.eulerAngles = new Vector3(0, 0, angleOfFirstBullet + i * angleDifferenceBetweenBullets);


            bullet.ApplyParameters(bulletType[currentParams]);

		    bullet.Birth();
	    }



		
    }

}


