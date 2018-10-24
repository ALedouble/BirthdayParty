using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PathType { Eratic, Circular, ZigZag, Random }

public class EnnemyParams : ScriptableObject {

	public Color color;
	public float health;
	public float speed;
	public float firingRate;
	public PathType pathType;
	public BulletParams bulletType;
}
