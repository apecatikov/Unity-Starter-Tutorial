using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	private GameObject _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(_player.transform.position.y <= -150.0f)
		{
			Application.LoadLevel ("GameOver");
		}
	}
}
