using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
	//peg description
	public List<Vector2> pegLocations = new List<Vector2>();
	public List<Vector2> initialDisabledPegs = new List<Vector2>();
	public List<List<bool>> pegHistory = new List<List<bool>>();

	//gameobjects
	public GameObject canvas;
	public List<GameObject> pegs = new List<GameObject>();
	public int activePegsCount = 0;
	public GameObject startPanel;
	public GameObject endGamePanel;
	public GameObject slider;

	// Use this for initialization
	void Awake () {
		canvas = GameObject.Find("Canvas");

		if(PlayerPrefs.GetInt("pegNumber") == 0) { //if a pegNumber has not been assigned set to 6
			PlayerPrefs.SetInt("pegNumber", 6);
		}

		if(PlayerPrefsX.GetVector2Array("disabledPegs").Length == 0) { //if there are no disabled Pegs assigned, set to 0f, 0f
			PlayerPrefsX.SetVector2Array("disabledPegs", new Vector2[] {new Vector2(0f, 0f)});
		}

		foreach(Vector2 disabledPegLocation in PlayerPrefsX.GetVector2Array("disabledPegs")) { //add disabled locations to List
			initialDisabledPegs.Add(disabledPegLocation);
		}

		slider.GetComponent<Slider>().value = (float) PlayerPrefs.GetInt("pegNumber"); //set slider value to playerPref
	}

	//loads the game with the Peg Factory
	public void loadGame() {
		startPanel.SetActive(false); //disables UI
		GetComponent<PegFactory>().generatePegs(PlayerPrefs.GetInt("pegNumber"), initialDisabledPegs);
		//creates first history instance of the state of the pegs
		List<bool> historyInstance = new List<bool>();
		foreach(GameObject peg in pegs) {
			if(peg.GetComponent<Peg>().containsPeg) {
				historyInstance.Add(true);
			} else {
				historyInstance.Add(false);
			}
		}
		pegHistory.Add(historyInstance);
	}

	//handles the game win
	public void winGame() {
		endGamePanel.SetActive(true);
		foreach(GameObject peg in pegs) {
			Destroy(peg);
		}
	}

	//reloads the scene
	public void restartGame() {
		SceneManager.LoadScene("Main");
	}

	//check if game has been won
	public void checkIfWon() {
		if(activePegsCount == 1) {
			winGame();
		}
	}

	//used by slider to adjust pegNumber
	public void setPegNumber(float num) {
		int newNum = Mathf.RoundToInt(num);
		PlayerPrefs.SetInt("pegNumber", newNum);
		slider.GetComponentInChildren<Text>().text = "Size: " + newNum.ToString();
	}
}
