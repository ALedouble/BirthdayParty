using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BulletDamage : MonoBehaviour {
    BulletParams bp;

    [Header("Script")]
    public Bullet idBullet;



    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.name == "Player" && idBullet.id == 1)
       {
           Destroy(this.gameObject);
            StatsManager.lifeValue -= 10;
       }

        if (collision.gameObject.name == "Ennemy(Clone)" && idBullet.id == 0)
        {
            StatsManager.scoreValue += 10;
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

      
}

