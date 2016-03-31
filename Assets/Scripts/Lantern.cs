using UnityEngine;
using System.Collections;

public class Lantern : MonoBehaviour {
	// Use this for initialization
	void Start () {
		StartCoroutine(decay());
		StartCoroutine(move());
	}
	
	IEnumerator decay() {
		yield return new WaitForSeconds(20f);
		Destroy(gameObject);
	}

	IEnumerator move() {
		GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-3f, 3f), GetComponent<Rigidbody2D>().velocity.y, 0f);
		yield return new WaitForSeconds(3f);
		StartCoroutine(move());
	}
}
