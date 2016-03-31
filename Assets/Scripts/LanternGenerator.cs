using UnityEngine;
using System.Collections;

public class LanternGenerator : MonoBehaviour {
	float screenAspect = (float)Screen.width / (float)Screen.height;
	float vertExtent;
	float horzExtent;
	// Use this for initialization
	void Start () {
		vertExtent = Camera.main.orthographicSize * 2f;    
		horzExtent = vertExtent * screenAspect;
		StartCoroutine(createLanterns());
	}
	
	IEnumerator createLanterns() {
		yield return new WaitForSeconds(1f);
		GameObject newLantern = (GameObject) Instantiate(Resources.Load("Prefabs/Lantern"), new Vector3(Random.Range(-horzExtent/2f, horzExtent/2), 1f + vertExtent/2f, 0f), Quaternion.identity);
		newLantern.transform.SetParent(GetComponent<GameManager>().canvas.transform);
		StartCoroutine(createLanterns());
	}
}
