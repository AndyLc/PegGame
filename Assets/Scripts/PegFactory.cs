using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PegFactory : MonoBehaviour {
	private GameObject gameManager;
	private int selectedColor;

	void Start() {
		gameManager = GameObject.Find("GameManager");
		selectedColor = Random.Range(0, 3);
	}

	public void generatePegs(int size, List<Vector2> disabledPositions) {
		//create pegLocations based on given size
		for(int y = 0; y < size; y++) {
			for(int x = 0; x < y + 1; x++) {
				Vector2 newPegLocation = new Vector2(-y + 2 * x, -y);
				gameManager.GetComponent<GameManager>().pegLocations.Add(newPegLocation);
			}
		}

		//instantiate pegs in each location
		foreach(Vector2 position in gameManager.GetComponent<GameManager>().pegLocations) {
			GameObject newPeg = (GameObject)Instantiate(Resources.Load("Prefabs/Peg"), position, Quaternion.identity);
			newPeg.transform.SetParent(gameManager.GetComponent<GameManager>().canvas.transform);
			newPeg.GetComponent<RectTransform>().anchoredPosition = (position + new Vector2(0, (float)size/2f)) * 70f;
			newPeg.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);
			newPeg.GetComponent<Peg>().containsPeg = true;
			newPeg.GetComponent<Peg>().position = position;
			newPeg.GetComponent<Peg>().color = (Peg.colors) selectedColor;
			gameManager.GetComponent<GameManager>().pegs.Add(newPeg);
			//disable if included in initialDIsabledPegs
			foreach(Vector2 disabledLocation in disabledPositions) {
				if(disabledLocation.Equals(position)) {
					newPeg.GetComponent<Peg>().containsPeg = false;
				}
			}

			if(newPeg.GetComponent<Peg>().containsPeg == true) {
				gameManager.GetComponent<GameManager>().activePegsCount += 1;
			}
		}
	}
}
