using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
    [SerializeField]
	public Vector3[] pathPoints;
	public Color gizmoColor = Color.yellow;

	public Transform self;
    int currentPoint = 0;
    private Transform pathTransfom;

    void Update()
    {
        /*pathTransfom = pathPoints;
        float dist = Vector3.Distance(pathPoints[currentPoint].position, transform.position);*/

       //transform.position = Vector3.MoveTowards (transform.position, pathPoints)
        Debug.Log(pathPoints.Length);
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
