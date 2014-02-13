using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// 0 : Left click
		// 1 : Right click
		// 2 : Middle button
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (ray, out hit);
			if (hit.transform.tag == "Start") {

					Application.LoadLevel ("Game");
			}
		}
	}
}
