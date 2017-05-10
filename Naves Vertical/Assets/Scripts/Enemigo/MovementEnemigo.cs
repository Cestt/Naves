using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemigo : MonoBehaviour {

	public float speed;

	Transform thisTransform;
	GameObject thisGameobject;
	// Use this for initialization
	void Start () {
		thisGameobject = this.gameObject;
		thisTransform = thisGameobject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.Translate(speed * Vector3.down * Time.deltaTime, Space.World);
	}
}
