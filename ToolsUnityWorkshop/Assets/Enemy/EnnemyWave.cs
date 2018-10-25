using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnnemySubwave
{
	public GameObject ennemy;
	public int enemyAmount;
}

public class EnnemyWave : MonoBehaviour
{
    public static EnnemyWave instance;
    public EnnemySubwave[] possibleWaves;
    public EnemyPath pathOfEnemy;
    int enemy = 0;
    
    public int wave = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        enemy = 0;
        for (int i = 0; i < possibleWaves[wave].enemyAmount; i++)
            {
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(possibleWaves[wave].ennemy, pathOfEnemy.pathPoints[0], spawnRotation);
                enemy++;
                yield return new WaitForSeconds(3f);
            }
    }

    void Update()
    {
        if (enemy == possibleWaves[wave].enemyAmount && (!GameObject.Find("Ennemy(Clone)")))
        {
            wave++;
            StartCoroutine(SpawnWaves());
        }
    }
}

