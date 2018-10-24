using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnnemySubwave
{
	public EnnemyParams enemyType;
	public int enemyAmount;
}

public class EnnemyWave : MonoBehaviour {

	public EnnemySubwave[] possibleWaves;

}
