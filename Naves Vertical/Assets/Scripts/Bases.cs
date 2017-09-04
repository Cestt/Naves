using UnityEngine;
using System.Collections;

public class Bases : MonoBehaviour {

	//Public Variables
	public float speed;
	public int life;

	//Private Variables
	Transform thisTransform;
	GameObject thisGameObject;
	string thisName;

	/// <summary>
	/// Custom constructor for the heritance.
	/// </summary>
	/// <param name="_thisTransform">This transform.</param>
	/// <param name="_thisGameObject">This game object.</param>
	/// <param name="_thisName">This name.</param>
	public void SetBases(Transform _thisTransform, GameObject _thisGameObject, string _thisName){
		thisTransform = _thisTransform;
		thisGameObject = _thisGameObject;
		thisName = _thisName;
	}

	/// <summary>
	/// Moves the GO.
	/// </summary>
	public void Movement(){
		if (speed != 0) {
			thisTransform.position += speed * transform.up* Time.deltaTime;
		}
	}
	/// <summary>
	/// Manages the object when off screen.
	/// </summary>
	void OnBecameInvisible(){
		if(thisName == "Disparo")
			thisGameObject.SetActive (false);
	}
}
