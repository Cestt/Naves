using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour {

	//Public variables
	public float speed;
	[HideInInspector]
	public float speedChange = 0;
	public float shootingSpeed;
	[HideInInspector]
	public float shootingSpeedChange = 0;
	[HideInInspector]
	public int upgradeSpeedChange = 0;
	[HideInInspector]
	public int upgradeShootingSpeedChange = 0;
	public float upgradeShootingSpeedActual;

	//Private Variables
	private bool disparando = false;
	private float upgradeSpeedActual;


	//Optimization Variables
	private Transform thisTransform;
	private GameObject thisGameobject;
	private Camera mCam;
	private Vector2 mousePosition;
	private Vector2 touchPosition;
	private List<GameObject> disparos;

	/// <summary>
	/// Sets the starting variables.
	/// </summary>
	void Start () {
		thisTransform = this.transform;
		thisGameobject = this.gameObject;
		mCam = Camera.main;
		disparos = mCam.GetComponent<Pool> ().disparos;
	}

	/// <summary>
	/// Sets the needed variables for the ship at the start of each round;
	/// </summary>
	public void OnStart(){
		upgradeSpeedActual = speed * ((7f * upgradeSpeedChange) / 100);
		upgradeShootingSpeedActual = shootingSpeed * ((5f * upgradeShootingSpeedChange) / 100);
	}

	/// <summary>
	/// Updates every delta, 
	/// triggers the movement, and moves the ship towars the touch position.
	/// triggers the shoting;
	/// </summary>
	void Update () {
		
		#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
		if(Input.GetMouseButton(0)){
			mousePosition = mCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
			mousePosition = new Vector2 (mousePosition.x, mousePosition.y + 1);
			thisGameobject.transform.position = Vector2.MoveTowards (thisTransform.position, mousePosition, speed + speedChange + upgradeSpeedActual);
			Utils.LookAt2D(thisTransform, new Vector3(mousePosition.x,mousePosition.y +0.8f,0),-22f,22f);
			if(!disparando){
				StartCoroutine(Disparar());
				disparando = true;
			}
			
		}

		#endif

		#if UNITY_ANDROID || UNITY_IPHONE
		if (Input.touchCount > 0) {
			touchPosition = mCam.ScreenToWorldPoint(new Vector3(Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 10));
			touchPosition = new Vector2 (touchPosition.x, touchPosition.y + 1f);
			thisGameobject.transform.position = Vector2.MoveTowards (thisTransform.position, touchPosition, speed);
			Utils.LookAt2D(thisTransform, new Vector3(touchPosition.x,touchPosition.y +0.8f,0),-22f,22f);
			if(!disparando){
				StartCoroutine(Disparar());
				disparando = true;
			}
		}



		#endif



	}

	/// <summary>
	/// Handles thenshooting behaviour every X seconds.
	/// </summary>
	IEnumerator Disparar(){
		while (true) {
			#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
			if (Input.GetMouseButton (0) || Input.GetMouseButtonDown(0)) {

				foreach(GameObject tempGo in disparos){
					if(tempGo.activeSelf == false){
						tempGo.transform.position = thisTransform.position;
						tempGo.transform.rotation = thisTransform.rotation;
						tempGo.SetActive(true);
						break;

					}
				}
			}
			#endif
			#if UNITY_ANDROID || UNITY_IPHONE
				if (Input.touchCount > 0 ) {
				foreach(GameObject tempGo in disparos){
					if(tempGo.activeSelf == false){
					tempGo.transform.position = thisTransform.position;
					tempGo.transform.rotation = thisTransform.rotation;
					tempGo.SetActive(true);
					break;

					}
				}
			}

			#endif

			yield return new WaitForSeconds (shootingSpeed - shootingSpeedChange - upgradeShootingSpeedActual);
		}
	

	}

	/// <summary>
	/// Stops the shooting behaviour.
	/// </summary>
	public void StopDisparo(){
		StopAllCoroutines ();
		disparando = false;
	}
}
