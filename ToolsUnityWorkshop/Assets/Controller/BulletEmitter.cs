using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour {

    [Header("Reference")]
    public Transform self;

    [Header("Values")]
    public int numberOfSimultaneousBullets;
	public float amplitudeAngle;
	private float currentCooldown;
	public float firingRate;
    private int currentParams;
    public bool isEnemyBullet = false;

    [Header("Array")]
    public BulletParams[] bulletType;




    [System.NonSerialized]
	public bool isEnabled;



    void Start()
    {
        
    }

    public void Update()
    { 
        currentCooldown -= Time.deltaTime;

        if (isEnabled && currentCooldown < 0)
        {
            currentCooldown = bulletType[currentParams].firingRate;
            EmitSingleBullet();
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (currentParams >= 0 && currentParams < bulletType.Length - 1)
            {
                currentParams += 1;
               
                currentCooldown = bulletType[currentParams].firingRate;
            }
            else
            {
                currentParams = 0;
                currentCooldown = 0;
            }
 
        }
    }


    public void EmitSingleBullet(){
    float angleDifferenceBetweenBullets = 0;
	if (bulletType[currentParams].numberOfSimultaneousBullets > 1){
		angleDifferenceBetweenBullets = bulletType[currentParams].amplitudeAngle / (bulletType[currentParams].numberOfSimultaneousBullets - 1);
	}

	float angleOfFirstBullet = -0.5f * bulletType[currentParams].amplitudeAngle;

        for (int i = 0; i < bulletType[currentParams].numberOfSimultaneousBullets; i++)
        {
            Bullet bullet = BulletPool.instance.CreateObject();

            if (bullet != null)
            { 
                bullet.self.position = Vector3.up;
                bullet.id = 0;


                // Orienter le projectile
                bullet.self.position = self.position;
                bullet.self.eulerAngles = new Vector3(0, 0, angleOfFirstBullet + i * angleDifferenceBetweenBullets);
            }

            bullet.ApplyParameters(bulletType[currentParams]);

		    bullet.Birth();
            
        }



		
    }

}


