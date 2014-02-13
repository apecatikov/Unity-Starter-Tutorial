using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private float _horizontal, _vertical;
	private GameObject _playerCam;

	public GameObject _bulletPrefab;
	public Transform _bulletSpawnPos;

	public float _maxVelocityChange = 10.0f;
	public float _speed = 10.0f;
	public float _throwForce = 30.0f;

	// Use this for initialization
	void Start () {
		_playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		//Get the WASD keyboard input. 
		//Front 0 to 1, back 0 to -1.
		//Right 0 to 1, left 0 to -1.
		_horizontal = Input.GetAxis("Horizontal");
		_vertical = Input.GetAxis("Vertical");

		//Velocity according to the xOy plane.
		Vector3 targetVelocity = new Vector3(_horizontal, 0, _vertical);

		//rotate the velocity vector using the player's rotation (Quaternion*Vector).
		targetVelocity = transform.rotation*targetVelocity;

		//Speed up the velocity.
		targetVelocity *= _speed;

		//We want a velocity change, therefore we subtract the new velocity from the player's current velocity.
		Vector3 v = rigidbody.velocity;
		Vector3 velocityChange = (targetVelocity-v);

		//Limit the amount that the velocity can change.
		velocityChange.x = Mathf.Clamp(velocityChange.x, -_maxVelocityChange, _maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -_maxVelocityChange, _maxVelocityChange);

		//We do not want the player to move up or down. (Jump's role)
		velocityChange.y = 0;

		//Apply the velocity change to the player.
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

		Shoot ();
	}

	void Shoot()
	{
		if(Input.GetMouseButtonDown(0))
		{
			GameObject bulletClone = (GameObject)GameObject.Instantiate(_bulletPrefab, _bulletSpawnPos.position, transform.rotation);

			bulletClone.rigidbody.AddForce(_playerCam.transform.forward*_throwForce, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter()
	{

	}
}
