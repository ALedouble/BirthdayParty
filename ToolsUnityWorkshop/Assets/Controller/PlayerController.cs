using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header ("Stats")]
	public float speed; 
	public float focusedSpeed;

	[Header("Inputs")]
	public KeyCode focusKey;
	public KeyCode shootKey;
	public KeyCode leftKey, rightKey, downKey, upKey;

	[Header ("References")]
	public Transform self;
	public Transform bottomLeftCorner, topRightCorner;

	public BulletEmitter emitter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
		UpdateShoot();
		UpdateAnimation();
	}

	void UpdateMovement(){
		Vector2 movement = Vector2.zero;
		if (Input.GetKey(leftKey)) movement.x--;
		if (Input.GetKey(rightKey)) movement.x++;
		if (Input.GetKey(downKey)) movement.y--;
		if (Input.GetKey(upKey)) movement.y++;
		if (movement.sqrMagnitude != 0) movement = movement.normalized;

		float usedSpeed = Input.GetKey(focusKey) ? focusedSpeed : speed;
		self.Translate(movement * usedSpeed * Time.deltaTime);

		if (self.position.x <  bottomLeftCorner.position.x)
			self.position = new Vector3 (bottomLeftCorner.position.x, self.position.y, self.position.z);
		if (self.position.x >  topRightCorner.position.x)
			self.position = new Vector3 (topRightCorner.position.x, self.position.y, self.position.z);
		if (self.position.y <  bottomLeftCorner.position.y)
			self.position = new Vector3 (self.position.x, bottomLeftCorner.position.y, self.position.z);
		if (self.position.y >  topRightCorner.position.y)
			self.position = new Vector3 (self.position.x, topRightCorner.position.y, self.position.z);
	}

	void UpdateShoot(){
		emitter.isEnabled = Input.GetKey(shootKey);
	}

	void UpdateAnimation(){

	}
}
