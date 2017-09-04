using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

	//Public Variables
	public GameObject player;
	public GameObject disparo;
	public int numeroDisparos;
	public GameObject enemigo1;
	public int numeroEnemigos1;
	public GameObject speedBuff;
	public int numeroSpeedBuff;
	public GameObject shootSpeedBuff;
	public int numeroShootSpeedBuff;

	[HideInInspector]
	public List<GameObject> disparos = new List<GameObject> ();
	[HideInInspector]
	public List<GameObject> enemigos1 = new List<GameObject> ();
	[HideInInspector]
	public List<GameObject> speedBuffs = new List<GameObject> ();
	[HideInInspector]
	public List<GameObject> shootSpeedBuffs = new List<GameObject> ();

	//Private Variables
	List<List <GameObject>> poolLists = new List<List <GameObject>>();

	private GameObject disparosParent;
	private GameObject enemigosParent;
	private GameObject buffsParent;
	private Movement playerMovement;

	/// <summary>
	/// Sets the starting variables and create the instances.
	/// Adds all the GO lists to a parent list.
	/// </summary>
	void Start () {

		poolLists.Add (disparos);
		poolLists.Add (enemigos1);
		poolLists.Add (speedBuffs);
		poolLists.Add (shootSpeedBuffs);
		playerMovement = player.GetComponent<Movement> ();
		disparosParent = GameObject.Find ("Disparos");
		for (int i = 0; i < numeroDisparos; i++) {
			GameObject clone = Instantiate (disparo,disparosParent.transform) as GameObject;
			clone.SetActive (false);
			disparos.Add (clone);
		
		}

		enemigosParent = GameObject.Find ("Enemigos");
		for (int i = 0; i < numeroEnemigos1; i++) {
			GameObject clone = Instantiate (enemigo1,enemigosParent.transform) as GameObject;
			clone.SetActive (false);
			enemigos1.Add (clone);

		}
			
		buffsParent = GameObject.Find ("Buffs");
		for (int i = 0; i < numeroSpeedBuff; i++) {
			GameObject clone = Instantiate (speedBuff,buffsParent.transform) as GameObject;
			clone.GetComponent<BuffScript> ().playerMovement = playerMovement;
			clone.SetActive (false);
			speedBuffs.Add (clone);

		}
		for (int i = 0; i < numeroShootSpeedBuff; i++) {
			GameObject clone = Instantiate (shootSpeedBuff,buffsParent.transform) as GameObject;
			clone.GetComponent<BuffScript> ().playerMovement = playerMovement;
			clone.SetActive (false);
			shootSpeedBuffs.Add (clone);

		}
			

	}


	/// <summary>
	/// De activates all the necesary GO to end the round.
	/// </summary>
	public void DeActivate(){
		player.GetComponent<Movement> ().StopDisparo ();
		player.transform.position = new Vector3 (0, -5, 0);
		foreach (List<GameObject> lists in poolLists) {
			foreach (GameObject tempGo in lists) {
				tempGo.SetActive (false);
			}
		}
		player.SetActive (false);
	}

}
