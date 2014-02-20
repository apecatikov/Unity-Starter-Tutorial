using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	private Vector3		_playerVelocity;	
	private Animator	_playerAnimator;

	private float		_verticalDirection;
	private float		_horizontalDirection;

	private float		_rotationSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		_playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//An example of how to ge the player's local direction without the player's input.
		_playerVelocity = transform.InverseTransformDirection(rigidbody.velocity);
		_verticalDirection = _playerVelocity.normalized.z;

		//Uses mouse input in order to get the horizontal direction to play the turning animations.
		_horizontalDirection = Input.GetAxis("Mouse X");
		_horizontalDirection *= _rotationSpeed;

		//Set animator variables that will be used in transitions and blend nodes.
		_playerAnimator.SetFloat ("Speed", _verticalDirection);
		_playerAnimator.SetFloat ("Direction", _horizontalDirection, 0.5f, Time.deltaTime);

	}
}
