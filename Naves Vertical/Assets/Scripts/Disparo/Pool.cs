using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

	public GameObject disparo;
	public int numeroDisparos;
	public GameObject enemigo1;
	public int numeroEnemigos1;

	[HideInInspector]
	public List<GameObject> disparos = new List<GameObject> ();
	[HideInInspector]
	public List<GameObject> enemigos1 = new List<GameObject> ();

	private GameObject disparosParent;
	private GameObject enemigosParent;

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

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
