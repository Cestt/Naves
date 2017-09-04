using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	//Public Variables
	public Button resetButton;
	public Button menuButton;
	public Text endText;
	public Movement player;
	public Text upgradeSpeedText;
	public Text upgradeShootingText;
	public Text upgradeBulletSpeedText;
	public Text creditsText;

	//Private Variables
	private int score = 0;
	private int credits = 0;
	private Text scoreText;
	private Disparo disparoScript;
	private Pool pool;
	private EnemiesSpawn eSpawn;
	private Dictionary<string,int> prices = new Dictionary<string, int> ();

	/// <summary>
	/// Sets the starting variables and manages the saved data.
	/// </summary>
	void Start(){
		pool = GetComponent<Pool> ();
		eSpawn = GetComponent<EnemiesSpawn> ();
		if(PlayerPrefs.HasKey("upgradeSpeed")){
			player.upgradeSpeedChange = PlayerPrefs.GetInt ("upgradeSpeed");
			player.upgradeShootingSpeedChange = PlayerPrefs.GetInt ("upgradeShooting");
			int uBulletspeed = PlayerPrefs.GetInt ("upgradeBulletSpeed");
			foreach (GameObject tempGo in pool.disparos) {
				tempGo.GetComponent<Disparo> ().upgradeBulletSpeed = uBulletspeed;
			}
			Debug.Log ("Manager Start " + player.upgradeSpeedChange + " " + player.upgradeShootingSpeedChange);
		}
		disparoScript = pool.disparos [0].GetComponent<Disparo> ();
	}

	/// <summary>
	/// Develop keys(TO BE ERASED).
	/// </summary>
	void Update(){
		if (Input.GetKey (KeyCode.P) & Input.GetKey (KeyCode.E) & Input.GetKey (KeyCode.K) & Input.GetKey (KeyCode.I)) {
			PlayerPrefs.DeleteAll ();
			scoreText.text = "Keys Deleted";
		}

		if (Input.GetKey (KeyCode.P) & Input.GetKey (KeyCode.E) & Input.GetKey (KeyCode.K) & Input.GetKey (KeyCode.O)) {
			UpdateCredits (100, true);
			scoreText.text = "Gimme da cashhh";
		}
	}

	/// <summary>
	/// Triggers every round start.
	/// </summary>
	public void OnStart(){
		scoreText = GameObject.Find ("InGame Score").GetComponent<Text> ();
	}

	/// <summary>
	/// Sets the shop prices and its texts.
	/// </summary>
	public void SetPrice(){
		UpdateCredits (0, false);
		creditsText.text = "Credits: " + credits;
		prices ["Speed"] = Mathf.FloorToInt (Mathf.Pow (1.5f, player.upgradeSpeedChange) * 100);
		prices ["ShootingSpeed"] = Mathf.FloorToInt (Mathf.Pow (1.5f, player.upgradeShootingSpeedChange) * 150);
		prices["BulletSpeed"] = Mathf.FloorToInt (Mathf.Pow (1.5f, disparoScript.upgradeBulletSpeed) * 75);
		upgradeSpeedText.text = " Speed +7% \n" + prices ["Speed"] + " Credits";
		upgradeShootingText.text = "Shooting Speed +5% \n" + prices ["ShootingSpeed"] + " Credits";
		upgradeBulletSpeedText.text = "Bullet Speed +1% \n" + prices ["BulletSpeed"] + " Credits";
		
	}


	/// <summary>
	/// Manages the purchases, 
	/// updates the prices,
	/// updates the upgrades values.
	/// </summary>
	/// <param name="item">Item.</param>
	public void Purchase(string item){
		switch (item) {
		case "Speed":
			if (credits > prices ["Speed"]) {
				UpdateCredits (-prices ["Speed"], true);
				player.upgradeSpeedChange++;
				SetPrice ();
				UpdateUpgrades ();
			}
			break;

		case "ShootSpeed":
			if (credits > prices ["ShootingSpeed"]) {
				UpdateCredits (-prices ["ShootingSpeed"], true);
				player.upgradeShootingSpeedChange++;
				SetPrice ();
				UpdateUpgrades ();
			}
			break;

		case "BulletSpeed":
			if (credits > prices ["BulletSpeed"]) {
				UpdateCredits (-prices ["BulletSpeed"], true);
				foreach (GameObject tempGo in pool.disparos) {
					tempGo.GetComponent<Disparo> ().upgradeBulletSpeed++;
				}
				SetPrice ();
				UpdateUpgrades ();
			}
			break;

		}
	}
		
	/// <summary>
	/// Resets the game to start a new round.
	/// </summary>
	public void ResetGame(){
		score = 0;
		scoreText.text = "Score: " + score;
		endText.gameObject.SetActive (false);
	}

	/// <summary>
	/// Manages all the variables and instances to finish the current round.
	/// </summary>
	public void End(){
		scoreText.gameObject.SetActive (false);
		eSpawn.OnStop ();
		pool.DeActivate ();
		player.shootingSpeedChange = 0;
		player.speedChange = 0;

		resetButton.gameObject.SetActive (true);
		menuButton.gameObject.SetActive (true);
		endText.gameObject.SetActive (true);
		UpdateCredits (score / 10, true);

		if (PlayerPrefs.HasKey ("score")) {
			int best = PlayerPrefs.GetInt ("score");
			if (best > score) {
				endText.text = "Final Score: " + score + "\n Best Score: " + best + "\n Credits: " + credits;
				UpdateScore ();
			} else {
				endText.text = "New best Score: " + score + "\n Credits: " + credits;
				UpdateScore ();
			}
				
		} else {
			endText.text = "New best Score: " + score + "\n Credits: " + credits;
			UpdateScore ();
		}


	}

	/// <summary>
	/// Saves the current highest score.
	/// </summary>
	public void UpdateScore(){
		if (PlayerPrefs.HasKey ("score")) {
			int best = PlayerPrefs.GetInt ("score");
			if (best < score)
				PlayerPrefs.SetInt ("score", score);
		} else {
			PlayerPrefs.SetInt ("score", score);
		}
	}

	/// <summary>
	/// Updates the upgrades saved in memory.
	/// </summary>
	public void UpdateUpgrades(){
		PlayerPrefs.SetInt ("upgradeSpeed", player.upgradeSpeedChange);
		PlayerPrefs.SetInt ("upgradeShooting", player.upgradeShootingSpeedChange);
		PlayerPrefs.SetInt ("upgradeBulletSpeed", disparoScript.upgradeBulletSpeed);
	}

	/// <summary>
	/// Updates the credits.
	/// </summary>
	/// <param name="creditsChange">Credits change.</param>
	/// <param name="save">If set to <c>true</c> save to memory.</param>
	public void UpdateCredits(int creditsChange, bool save){
		if (PlayerPrefs.HasKey ("credits")) {
			credits = PlayerPrefs.GetInt ("credits");
			credits += creditsChange;
		} else {
			credits += creditsChange;
		}
		if(save)
			PlayerPrefs.SetInt ("credits", credits);
	}

	/// <summary>
	/// Updates the round score text, and round score.
	/// </summary>
	/// <param name="add">Add.</param>
	public void UpdateScore(int add){
		score += add;
		scoreText.text = " Score: " + score;

	}

}
