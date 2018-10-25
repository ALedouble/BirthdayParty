using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParams : ScriptableObject {

    [Header("Couleur des balles")]
    public Color color;

    [Header ("Propriétés des balles")]
    [Range(5.0f, 15.0f)]
	public float speed;
    [Range(0.7f, 5f)]
	public float size;
    [Range(1f, 10f)]
    public float lifespan;
    [Range(0.1f, 1f)]
    public float firingRate;
    [Range(1, 20)]
    public int numberOfSimultaneousBullets;
    [Range(5f, 90f)]
    public float amplitudeAngle;

    [Header("Utilisateur de la balle")]
    public bool isEnemyBullet;

}
