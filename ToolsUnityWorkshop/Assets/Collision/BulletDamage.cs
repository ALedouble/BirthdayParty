using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BulletDamage : MonoBehaviour {
    BulletParams bp;
    public Bullet idBullet;

    public enum stateType
    {
        Player,
        Enemy,
    }

    public stateType characterType;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
 
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.name == "Player" && idBullet.id == 1)
            {
                Debug.Log("outch");
            }

        if (collision.gameObject.name == "Ennemy(Clone)" && idBullet.id == 0)
        {
            Debug.Log("héhé");
        }
    }

      
}

