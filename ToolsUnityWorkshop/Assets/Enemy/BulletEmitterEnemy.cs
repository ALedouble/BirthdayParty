using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitterEnemy : MonoBehaviour
{

    [Header("Reference")]
    public Transform self;

    [Header("Values")]
    public int numberOfSimultaneousBullets;
    public float amplitudeAngle;
    private float currentCooldown;
    public float firingRate;
    int currentParams = 0;
    bool isEnemyBullet = true;

    [Header("Array")]
    public BulletParams[] bulletEnemy;

   
    [System.NonSerialized]
    public bool isEnabled = true;

    private void Start()
    {
        
    }

    void Update()
    {
        bulletEnemy[currentParams].speed = -8;
        currentCooldown -= Time.deltaTime;

        if (isEnabled && currentCooldown < 0 )
        {
            currentCooldown = bulletEnemy[EnnemyWave.instance.wave].firingRate; 
            EmitSingleBullet();  
        }

    }


    public void EmitSingleBullet()
    {
        float angleDifferenceBetweenBullets = 0;
        if (bulletEnemy[EnnemyWave.instance.wave].numberOfSimultaneousBullets > 1)
        {
            angleDifferenceBetweenBullets = bulletEnemy[EnnemyWave.instance.wave].amplitudeAngle / (bulletEnemy[EnnemyWave.instance.wave].numberOfSimultaneousBullets - 1);
        }

        float angleOfFirstBullet = -0.5f * bulletEnemy[EnnemyWave.instance.wave].amplitudeAngle;


        for (int i = 0; i < bulletEnemy[EnnemyWave.instance.wave].numberOfSimultaneousBullets; i++)
        {
            Bullet bullet = BulletPool.instance.CreateObject();

            if (bullet != null)
            {
                bullet.self.position = Vector3.up;
                bullet.id = 1;

                // Orienter le projectile
                bullet.self.position = self.position;
                bullet.self.eulerAngles = new Vector3(0, 0, angleOfFirstBullet + i * angleDifferenceBetweenBullets);
            }

            bulletEnemy[EnnemyWave.instance.wave].isEnemyBullet = true;

            bullet.ApplyParameters(bulletEnemy[EnnemyWave.instance.wave]);

            bullet.Birth();
           
        }
    }
}

