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
		thisGameobject = this.gameObject;
		thisTransform = thisGameobject.transform;
	}
	
	void Update(){
		thisTransform.Translate(speed * Vector3.up * Time.deltaTime, Space.World);
	}

	void OnBecameInvisible() {
		thisGameobject.SetActive (false);
	}
}
