using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PegMovement : MonoBehaviour {
	public Vector2 selectedPosition;
	public Vector2 targetPosition;
	public GameObject selectedObject;
	public GameObject targetObject;
	public GameObject middleObject;
	public GameObject movingPeg;

	public static PegMovement pm;

	void Awake() {
		pm = this;
	}

	public void move() {
		//if both even or both odd and skips over one peg.
		if(isBothEvenOrOdd((int)selectedPosition.x, (int)targetPosition.x) && pegBetweenExists(selectedPosition, targetPosition)) {
			middleObject.GetComponent<Peg>().disable();
			selectedObject.GetComponent<Peg>().disable();
			targetObject.GetComponent<Peg>().enable();
			GetComponent<GameManager>().activePegsCount -= 1;
			GetComponent<GameManager>().checkIfWon();
			List<bool> historyInstance = new List<bool>();
			foreach(GameObject peg in GetComponent<GameManager>().pegs) {
				if(peg.GetComponent<Peg>().containsPeg) {
					historyInstance.Add(true);
				} else {
					historyInstance.Add(false);
				}
			}
			GetComponent<GameManager>().pegHistory.Add(historyInstance);
		} else if(targetObject.Equals(selectedObject)){
			selectedObject.GetComponent<Peg>().enable();
			targetObject.GetComponent<Peg>().enable();
		} else {
			selectedObject.GetComponent<Peg>().enable();
		}
	}

	//check if two values are both even or odd
	private bool isBothEvenOrOdd(int x, int y) {
		x = Mathf.Abs(x);
		y = Mathf.Abs(y);
		if((x % 2 == 0 && y % 2 == 0) || (x % 2 == 1 && y % 2 == 1)) {
			return true;
		} else {
			return false;
		}
	}

	//check if there is a peg in the position between the two
	private bool pegBetweenExists(Vector2 selected, Vector2 target) {
		Vector2 midpoint = (target - selected) * 0.5f + selected;
		foreach(GameObject peg in GetComponent<GameManager>().pegs) {
			//if it is the midpoint peg
			if(peg.GetComponent<Peg>().position == midpoint) {
				//if it is enabled, return true
				if(peg.GetComponent<Peg>().containsPeg == true) {
					middleObject = peg;
					return true;
				}
			}
		}
		return false;
	}

	//undos the most recent movement
	public void undoMovement() {
		//get the length of the history of moves
		int pegHistoryLength = GetComponent<GameManager>().pegHistory.Count;

		//if there is a history that doesn't include the first instant
		if(pegHistoryLength != 1) {
			GetComponent<GameManager>().pegHistory.Remove(GetComponent<GameManager>().pegHistory[pegHistoryLength - 1]); //remove the current state from history
			List<bool> selectedHistory = GetComponent<GameManager>().pegHistory[pegHistoryLength - 2]; //set selected history to the one before the current state
			int i = 0;
			//set pegs to enabled and disabled based on the history
			foreach(GameObject peg in GetComponent<GameManager>().pegs) {
				if(selectedHistory[i] == true) {
					peg.GetComponent<Peg>().enable();
				} else {
					peg.GetComponent<Peg>().disable();
				}
				i++;
			}
			//add one to the number of pegs active
			GetComponent<GameManager>().activePegsCount += 1;
		}
	}


}
