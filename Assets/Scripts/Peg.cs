using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Peg : MonoBehaviour {
	public enum colors{teal, pink, green}
	public Vector2 position;
	public colors color;
	public bool containsPeg;
	private Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
		StartCoroutine(playAnimation());
		colorPeg();
		if(!containsPeg) {
			animator.SetTrigger("remove");
		}
	}

	//stop flashing and set the frame to gone
	public void disable() {
		containsPeg = false;
		animator.ResetTrigger("flash");
		animator.SetTrigger("remove");
	}

	//stop the frame gone and set it to flashing
	public void enable() {
		containsPeg = true;
		animator.ResetTrigger("remove");
		animator.SetTrigger("flash");
	}


	IEnumerator playAnimation() {
		yield return new WaitForSeconds(Random.Range(1f, 4f));
		if(containsPeg) {
			animator.SetTrigger("flash");
		}
		StartCoroutine(playAnimation());
	}

	//set color to a specific color based on enum
	private void colorPeg() {
		if(color == colors.teal) {
			GetComponent<Image>().color = new Color(0f, 1f, 1f);
		} else if (color == colors.pink) {
			GetComponent<Image>().color = new Color(1f, .325f, .69f);
		} else if (color == colors.green) {
			GetComponent<Image>().color = new Color(0f, 1f, 0.498f);
		}
	}
}
