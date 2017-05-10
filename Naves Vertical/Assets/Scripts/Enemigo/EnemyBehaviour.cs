using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	ScoreManager scManager;
	[HideInInspector]
	public bool triggered;
	public int score;
	void Start () {
		scManager = Camera.main.GetComponent<ScoreManager> ();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Disparo" && !triggered) {
			triggered = true;
			scManager.UpdateScore (score);
			other.gameObject.SetActive (false);
			gameObject.SetActive (false);
		}
	}
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			scManager.UpdateScore (- 10);
			gameObject.SetActive (false);
		}
	}
}
