using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class PegInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
	public static bool dragging = false;
	private bool inUse;

	public void OnPointerDown (PointerEventData eventData) 
	{
		if(GetComponent<Peg>().containsPeg) {
			//set the variables of selected in PegMovement
			PegMovement.pm.selectedPosition = GetComponent<Peg>().position;
			PegMovement.pm.selectedObject = gameObject;
			GetComponent<Peg>().disable(); //disable the current peg
			//create a draggable peg
			PegMovement.pm.movingPeg = (GameObject)Instantiate(Resources.Load("Prefabs/PegCursor"), Input.mousePosition, Quaternion.identity);
			PegMovement.pm.movingPeg.transform.SetParent(GameObject.Find("Canvas").transform);
			PegMovement.pm.movingPeg.GetComponent<Image>().color = GetComponent<Image>().color;
			inUse = true;
			dragging = true;
		}
	}

	public void OnPointerUp (PointerEventData eventData) 
	{
		//if there is a spot when the mouse is released that does not contain a peg
		if(eventData.pointerEnter && !eventData.pointerEnter.GetComponent<Peg>().containsPeg && dragging) {
				PegMovement.pm.targetPosition = eventData.pointerEnter.GetComponent<Peg>().position;
				PegMovement.pm.targetObject = eventData.pointerEnter;
				PegMovement.pm.move(); //perma move the peg
				dragging = false;
		} else {
			if(inUse) { //if it's the same spot
				GetComponent<Peg>().enable();
				inUse = false;
			}
		}
		Destroy(PegMovement.pm.movingPeg);
	}

	public void OnDrag (PointerEventData eventData) 
	{
		PegMovement.pm.movingPeg.transform.position = Input.mousePosition;
	}

}
