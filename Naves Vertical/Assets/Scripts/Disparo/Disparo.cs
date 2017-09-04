using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : Bases {

	//Public Variables
	public int upgradeBulletSpeed = 0;

	/// <summary>
	/// Sets the starting variables.
	/// </summary>
	void Start () {
		base.SetBases (this.transform, this.gameObject, this.tag);
		Physics2D.IgnoreLayerCollision (8, 8);

	}

	/// <summary>
	/// Updates every delta, invokes the movement
	/// </summary>
	void Update(){
		base.Movement();
	}
		
}
