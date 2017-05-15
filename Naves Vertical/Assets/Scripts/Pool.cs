using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

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

	private GameObject disparosParent;
	private GameObject enemigosParent;
	private GameObject buffsParent;
	private GameObject player;
	private Movement playerMovement;

	void Start () {


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

		player = GameObject.Find ("Nave");
		playerMovement = player.GetComponent<Movement> ();
		buffsParent = GameObject.Find ("Buffs");
		for (int i = 0; i < numeroSpeedBuff; i++) {
			GameObject clone = Instantiate (speedBuff,buffsParent.transform) as GameObject;
			clone.GetComponent<SpeedBuffScript> ().playerMovement = playerMovement;
			clone.SetActive (false);
			speedBuffs.Add (clone);

		}
		for (int i = 0; i < numeroShootSpeedBuff; i++) {
			GameObject clone = Instantiate (shootSpeedBuff,buffsParent.transform) as GameObject;
			clone.GetComponent<ShootBuffScript> ().playerMovement = playerMovement;
			clone.SetActive (false);
			shootSpeedBuffs.Add (clone);

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
