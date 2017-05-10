using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {

	int score = 0;
	Text scoreText;

	void Start(){
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
	}

	public void UpdateScore(int add){
		score += add;
		scoreText.text = " Score: " + score;

	}
	public void ResetScore(){
		score = 0;
	}
}
