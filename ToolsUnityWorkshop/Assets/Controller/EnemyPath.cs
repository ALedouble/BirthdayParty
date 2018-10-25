﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
    [SerializeField]
	public Vector3[] pathPoints;
	public Color gizmoColor = Color.yellow;
    public float speed = 0.000001f;
    public float reachDist = 0.5f;

	public Transform self;
    public Transform ennemies;
    int currentPoint = 0;
    private Transform pathTransfom;

    void Update()
    {

        float dist = Vector3.Distance(pathPoints[currentPoint], ennemies.position);

        ennemies.position = Vector3.Lerp(ennemies.position, pathPoints[currentPoint], speed * Time.deltaTime);

        if (dist <= reachDist)
        {
            currentPoint++;
        }

        if (currentPoint >= pathPoints.Length)
        {
            currentPoint = 0;
        }
    }

    private void OnDrawGizmos(){
        
		if (!self) return;
		if (self.childCount < 2) return;

		Gizmos.color = gizmoColor;

		for (int i = 0; i<self.childCount - 1; i++){
			Gizmos.DrawLine(self.GetChild(i).position, self.GetChild(i + 1).position);
		}

		Gizmos.DrawLine(self.GetChild(0).position, self.GetChild(self.childCount-1).position);
	}
}
