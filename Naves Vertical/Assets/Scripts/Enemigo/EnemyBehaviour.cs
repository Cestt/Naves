using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Bases {

	GameManager gmManager;
	List<GameObject> speedBuffs;
	List<GameObject> shootSpeedBuffs;
	[HideInInspector]
	public bool triggered;
	[Range (-1,100)]
	public float speedBuffSpawnRate;
	[Range (-1,100)]
	public float shootSpeedBuffSpawnRate;
	public float spawnRateReduction;
	public int score;
	EnemiesSpawn eSpawn;

	void Start () {
		SetBases (transform, gameObject, tag);
		eSpawn = Camera.main.GetComponent<EnemiesSpawn> ();
		gmManager = Camera.main.GetComponent<GameManager> ();
		speedBuffs = Camera.main.GetComponent<Pool> ().speedBuffs;
		shootSpeedBuffs = Camera.main.GetComponent<Pool> ().shootSpeedBuffs;
		Physics2D.IgnoreLayerCollision (9, 9);
		Physics2D.IgnoreLayerCollision (9, 10);

	}

	void Update(){
		base.Movement ();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Disparo" && !triggered) {
			triggered = true;
			gmManager.UpdateScore (score);
			other.gameObject.SetActive (false);
				if (Random.Range (0f, 100f) <= speedBuffSpawnRate) {

					foreach(GameObject tempGo in speedBuffs){
						if(tempGo.activeSelf == false){
							tempGo.transform.position = gameObject.transform.position;
							tempGo.SetActive(true);
							tempGo.GetComponent<BuffScript>().StartExpand(new Vector2(Random.Range(-1,1), Random.Range(-1,1)));
							break;
						}
					}
				}

				if (Random.Range (0f, 100f) <= shootSpeedBuffSpawnRate) {

					foreach(GameObject tempGo in shootSpeedBuffs){
						if(tempGo.activeSelf == false){
							tempGo.transform.position = gameObject.transform.position;
							tempGo.SetActive(true);
							tempGo.GetComponent<BuffScript>().StartExpand(new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)));
							break;
						}
					}
				}

			eSpawn.ChangeSpawnRate (1, spawnRateReduction);
			gameObject.SetActive (false);
		}
		if (other.tag == "Death" && !triggered) {
			triggered = true;
			gameObject.SetActive (false);
			gmManager.End ();
		}
	}
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			gameObject.SetActive (false);
			gmManager.End ();
		}
	}
}
