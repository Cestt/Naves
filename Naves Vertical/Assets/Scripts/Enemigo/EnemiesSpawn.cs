using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour {

	//Public Variables.
	public float Enemies1SpwanRate;

	//Private Variables.
	float Enemies1SpawnRateInitial;
	Vector3 stageDimensions;
	List<GameObject> enemigos1;

	/// <summary>
	/// Sets the starting variables.
	/// </summary>
	void Start () {
		stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,10));
		enemigos1 = GetComponent<Pool> ().enemigos1;
		Enemies1SpawnRateInitial = Enemies1SpwanRate;
	}

	/// <summary>
	/// Triggers at the start of each round,
	/// Starts the enemy spawn.
	/// </summary>
	public void OnStart(){
		StartCoroutine (Spawn());
	}

	/// <summary>
	/// Triggers at the end of each round,
	/// Stops the enemy spawn.
	/// </summary>
	public void OnStop(){
		StopAllCoroutines ();
		Enemies1SpwanRate = Enemies1SpawnRateInitial;
	}

	/// <summary>
	/// Manages the enemy spawn every X seconds
	/// </summary>
	IEnumerator Spawn(){
		while (true) {
			yield return new WaitForSeconds (Enemies1SpwanRate);
			foreach(GameObject tempGo in enemigos1){
				if(tempGo.activeSelf == false){
					tempGo.transform.position = new Vector2(Random.Range(-stageDimensions.x +0.2f,stageDimensions.x-0.2f),5.99f);
					tempGo.transform.rotation = Quaternion.identity;
					tempGo.SetActive(true);
					tempGo.GetComponent<EnemyBehaviour> ().triggered = false;
					break;
				}
			}
			//if(Enemies1SpwanRate > 0.1f)
			//	Enemies1SpwanRate -= Enemies1SpawnRateReduction;
		}

	}

	/// <summary>
	/// Updates the enemy spawn rate
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	/// <param name="change">Change.</param>
	public void ChangeSpawnRate(int enemy, float change){
		switch (enemy) {
		case 1: 
			Enemies1SpwanRate -= change;
			break;
		}
	}
}
