using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour {

	//Public variables
	public float speed;
	public float realSpeed;
	[HideInInspector]
	public float speedChange = 0;
	public float shootingSpeed;
	public float realShootingSpeed;
	[HideInInspector]
	public float shootingSpeedChange = 0;

	//Optimization Variables
	private Transform thisTransform;
	private GameObject thisGameobject;
	private Camera mCam;
	private Vector2 mousePosition;
	private Vector2 touchPosition;
	private List<GameObject> disparos;

	void Awake(){
		
	}

	void Start () {
		thisTransform = this.transform;
		thisGameobject = this.gameObject;
		mCam = Camera.main;
		disparos = mCam.GetComponent<Pool> ().disparos;
	}
	

	void Update () {
		
		#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
		if(Input.GetMouseButton(0)){
			mousePosition = mCam.ScreenToWorldPoint (Input.mousePosition);
			mousePosition = new Vector2 (mousePosition.x, mousePosition.y + 1);
			thisGameobject.transform.position = Vector2.MoveTowards (thisTransform.position, mousePosition, speed + speedChange);
			Utils.LookAt2D(thisTransform, new Vector3(mousePosition.x,mousePosition.y +0.8f,0),-22f,22f);
			if(!IsInvoking("Disparar")){
				InvokeRepeating("Disparar",0,shootingSpeed - shootingSpeedChange);
			}
			
		}
		if(Input.GetMouseButtonUp(0)){
			CancelInvoke("Disparar");
		}
		#endif

		#if UNITY_ANDROID || UNITY_IPHONE
		if (Input.touchCount > 0 & Input.GetTouch(0).phase == TouchPhase.Began || Input.touchCount > 0 & Input.GetTouch(0).phase == TouchPhase.Moved ) {
			touchPosition = mCam.ScreenToWorldPoint(Input.GetTouch (0).position);
			touchPosition = new Vector2 (touchPosition.x, touchPosition.y + 1f);
			thisGameobject.transform.position = Vector2.MoveTowards (thisTransform.position, mousePosition, speed);
			if(!IsInvoking("Disparar")){
				InvokeRepeating("Disparar",0,shootingSpeed);
			}
		}

		if (Input.touchCount > 0 & Input.GetTouch (0).phase == TouchPhase.Ended || Input.touchCount > 0 & Input.GetTouch (0).phase == TouchPhase.Canceled) {
			CancelInvoke("Disparar");
		}

		#endif



	}

	void Disparar(){
		foreach(GameObject tempGo in disparos){
			if(tempGo.activeSelf == false){
				tempGo.transform.position = thisTransform.position;
				tempGo.transform.rotation = thisTransform.rotation;
				tempGo.SetActive(true);
				break;
			}
		}
	}
}
