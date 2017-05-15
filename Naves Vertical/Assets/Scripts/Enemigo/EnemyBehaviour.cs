using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	ScoreManager scManager;
	List<GameObject> speedBuffs;
	List<GameObject> shootSpeedBuffs;
	[HideInInspector]
	public bool triggered;
	[Range (-1,100)]
	public float speedBuffSpawnRate;
	[Range (-1,100)]
	public float shootSpeedBuffSpawnRate;
	public int score;

	void Start () {
		scManager = Camera.main.GetComponent<ScoreManager> ();
		speedBuffs = Camera.main.GetComponent<Pool> ().speedBuffs;
		shootSpeedBuffs = Camera.main.GetComponent<Pool> ().shootSpeedBuffs;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Disparo" && !triggered) {
			triggered = true;
			scManager.UpdateScore (score);
			other.gameObject.SetActive (false);
				if (Random.Range (0f, 100f) <= speedBuffSpawnRate) {

					foreach(GameObject tempGo in speedBuffs){
						if(tempGo.activeSelf == false){
							tempGo.transform.position = gameObject.transform.position;
							tempGo.SetActive(true);
							break;
						}
					}
				}

				if (Random.Range (0f, 100f) <= shootSpeedBuffSpawnRate) {

					foreach(GameObject tempGo in shootSpeedBuffs){
						if(tempGo.activeSelf == false){
							tempGo.transform.position = gameObject.transform.position;
							tempGo.SetActive(true);
							break;
						}
					}
				}
				
			gameObject.SetActive (false);
		}
		if (other.tag == "Death Trigger" && !triggered) {
			triggered = true;
			scManager.UpdateScore (-10);
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
