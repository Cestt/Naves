using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDisparo : MonoBehaviour {

	public float speed;

	Transform thisTransform;
	GameObject thisGameobject;
	Vector2 newPosition;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (8, 8);
		thisGameobject = this.gameObject;
		thisTransform = thisGameobject.transform;
	}
	
	void Update(){
		thisTransform.position += speed * transform.up* Time.deltaTime ;
		if (thisTransform.position.y > 6 && thisGameobject.activeSelf == true) {
			thisGameobject.SetActive (false);
		}
	}

	void OnBecameInvisible() {
		thisGameobject.SetActive (false);
	}
}
