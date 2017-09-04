using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : Bases {

	//Public Variables
	public float playerShootSpeedIncrease;
	public float playerSpeedIncrease;
	[System.Serializable]
	public enum buffType
	{
		shoot,
		speed
	};
	public buffType buffTypes = new buffType();
	[HideInInspector]
	public Movement playerMovement;


	//Private Variables
	bool movement = false;

	/// <summary>
	/// Sets the starting variables
	/// </summary>
	void Start(){
		Physics2D.IgnoreLayerCollision (10, 9);
		Physics2D.IgnoreLayerCollision (10, 10);
		base.SetBases (this.transform, this.gameObject, this.tag);
	}

	/// <summary>
	/// Updates every delta, invokes the movement
	/// </summary>
	void Update () {
		if (movement) {
			base.Movement();
		}
	}

	/// <summary>
	/// Starts the expansion of the buff.
	/// </summary>
	/// <param name="dir">Dir.</param>
	public void StartExpand (Vector2 dir){
		StartCoroutine (Expand (dir));
	}

	/// <summary>
	/// Expand to a specified dir a buff before moving it downwards.
	/// </summary>
	/// <param name="dir">Dir.</param>
	IEnumerator Expand(Vector2 dir){
		int times = 0;
		while (true) {
			transform.position += new Vector3 (transform.position.x + dir.x, transform.position.y + dir.y, 0) * speed * Time.deltaTime;
			yield return new WaitForFixedUpdate ();
			times++;
			if (times >= 20) {
				movement = true;
				StopAllCoroutines();
			}
		}
	}




	/// <summary>
	/// Raises the trigger enter 2d event.
	/// </summary>
	/// <param name="other">Other(Collider).</param>
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Death") {

			gameObject.SetActive (false);

		}

	}

	/// <summary>
	/// Raises the collision enter 2d event.
	/// </summary>
	/// <param name="other">Other(Collider).</param>
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			if (buffTypes == buffType.shoot) {
				if((playerMovement.shootingSpeed - playerMovement.shootingSpeedChange - playerMovement.upgradeShootingSpeedActual) 
					> 0.06)
					playerMovement.shootingSpeedChange += playerShootSpeedIncrease;
			}
			if (buffTypes == buffType.speed) {
				playerMovement.speedChange += playerSpeedIncrease; 
			} 

			gameObject.SetActive (false);
		}
	}
}
