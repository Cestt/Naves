using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuffScript : MonoBehaviour {

	public float speed;
	public float playerSpeedIncrease;

	Transform thisTransform;
	GameObject thisGameobject;

	[HideInInspector]
	public Movement playerMovement;

	void Start(){
		thisGameobject = this.gameObject;
		thisTransform = thisGameobject.transform;
	}

	void Update () {
		thisTransform.Translate(speed * Vector3.down * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Death Trigger") {
			
			gameObject.SetActive (false);

		}

	}
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			playerMovement.speedChange += playerSpeedIncrease; 
			playerMovement.realSpeed = playerMovement.speed + playerMovement.speedChange;
			gameObject.SetActive (false);
		}
	}
}
