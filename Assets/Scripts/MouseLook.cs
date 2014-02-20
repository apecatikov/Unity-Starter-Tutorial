using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
	
	private GameObject _player;
	private float _xRot, _yRot;

	//Arbitrary values to speed up rotation using the mouse's motion.
	public float _xSpeed = 200.0f;
	public float _ySpeed = 200.0f;

	//the relative distance from the player to the camera.
	public Vector3 _camOffset;

	//Mininum and maximum camera angles
	public float _minY = -20.0f;
	public float _maxY = 80.0f;

	//Minimum and maximum camera distance
	public float _minCamDistance = -0.15f;
	public float _maxCamDistance = -10.0f;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player");
		_camOffset = new Vector3 (0.0f, 1.0f, -5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//Similar to keyboard input, mouse input varies from -1 to 1.
		//We take the mouse input and add it up to use as euler angles.
		//You can change -= for += if you want an inverted rotation.
		_xRot += Input.GetAxis ("Mouse X") * _xSpeed * Time.deltaTime;
		_yRot -= Input.GetAxis ("Mouse Y") * _ySpeed * Time.deltaTime;

		//We don't want the camera to spin around our character vertically.
		ClampAngle (ref _yRot);
	}

	void LateUpdate()
	{
		//We want the mouse motion along the X axis to change the Yaw (rotation about the Y axis),
		//and the motion along the Y axis to change the Roll (rotation about the X axis).
		Quaternion rotation = Quaternion.Euler (_yRot, _xRot, 0.0f);

		//Zoom in and out of the third person view.
		_camOffset.z += Input.GetAxis ("Mouse ScrollWheel");
		ClampDistance (ref _camOffset.z);

		//Store the distance away from the player.
		Vector3 distance = new Vector3 (0.0f, 0.0f, _camOffset.z);

		//rotation*distance represents the distance which separates the player and the camera
		//Add the distance vector to the player's position vector to obtain the camera's new postion.
		Vector3 position = (_player.transform.position + new Vector3 (0.0f, _camOffset.y, 0.0f)) + rotation * distance;

		//Make sure the camera doesn't go through unwanted surfaces.
		CompensateForCollisions (ref position, _player.transform.position + new Vector3 (0.0f, _camOffset.y, 0.0f));

		//Set the camera's new rotation and position.
		transform.rotation = rotation;
		transform.position = position;

		//Update the player's rotation according to the camera's rotation.
		//However, we only want to update the player's Yaw rotation (around the Y axis).
		_player.transform.localEulerAngles = new Vector3 (_player.transform.localEulerAngles.x, transform.localEulerAngles.y, _player.transform.localEulerAngles.z);
	}

	//This is to make sure that the angle stays within the defined boundaries.
	void ClampAngle(ref float angle)
	{
		if (angle < _minY)
		{
			angle = _minY;
		}
		else if (angle > _maxY)
		{
			angle = _maxY;
		}
	}

	//This is to make sure that the distance from the player to the camera is within the defined boundaries.
	void ClampDistance(ref float distance)
	{
		if (distance > _minCamDistance)
		{
			distance = _minCamDistance;
		}
		else if (distance < _maxCamDistance)
		{
			distance = _maxCamDistance;
		}
	}

	//Set the camera's position to the point it hit on the surface.
	void CompensateForCollisions(ref Vector3 camPos, Vector3 playerPos)
	{
		RaycastHit hit = new RaycastHit();
		if(Physics.Linecast(playerPos, camPos, out hit))
		{
			camPos = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
		}

	}

}
