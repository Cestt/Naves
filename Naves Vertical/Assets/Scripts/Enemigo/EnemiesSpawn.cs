using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour {

	public float Enemies1SpwanRate;
	Vector3 stageDimensions;
	List<GameObject> enemigos1;
	// Use this for initialization
	void Start () {
		stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
		enemigos1 = Camera.main.GetComponent<Pool> ().enemigos1;
		StartCoroutine (Spawn());
	}
	
	IEnumerator Spawn(){
		while (true) {
			yield return new WaitForSeconds (Enemies1SpwanRate);
			foreach(GameObject tempGo in enemigos1){
				if(tempGo.activeSelf == false){
					tempGo.transform.position = new Vector2(Random.Range(-stageDimensions.x +0.1f,stageDimensions.x-0.1f),5.30f);
					tempGo.SetActive(true);
					tempGo.GetComponent<EnemyBehaviour> ().triggered = false;
					break;
				}
			}
		}

	}
}
