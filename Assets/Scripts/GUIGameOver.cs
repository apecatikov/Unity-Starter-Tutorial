using UnityEngine;
using System.Collections;

public class GUIGameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (ray, out hit);
			if (hit.transform.tag == "Game Over") {
				
				Application.LoadLevel ("Menu");
			}
		}
	}
}
