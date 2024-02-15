using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
	[Header("Camera Settings")]
	[SerializeField] private float _rotationSmooth = 0.5f;
	[SerializeField] private float _minLookAngle = 40;
	[SerializeField] private float _maxLookAngle = 340f;
	[SerializeField] private float _sensitivity = 1f;
	[SerializeField] private Transform _followTarget;
	[SerializeField] private Vector3 _offset;


	private MovementHandler _movementHandler;

	[Inject]
	private void Construct(MovementHandler movementHandler)
	{
		_movementHandler = movementHandler;
	}

	void LateUpdate()
	{
		HandleRotation();
		HandleMovement();
	}

	private void HandleMovement()
	{
		transform.position = _followTarget.position + _offset;
	}

	private void HandleRotation()
	{
		if (_movementHandler.RotationDirection == Vector3.zero)
			return;

		//_rotationPivot.Rotate(0f, _movementHandler.RotationDirection.y * _sensitivity * Time.deltaTime, 0f, Space.World);
		//_rotationPivot.Rotate(-_movementHandler.RotationDirection.x * _sensitivity * Time.deltaTime, 0f, 0f, Space.Self);

		transform.rotation *= Quaternion.AngleAxis(_movementHandler.RotationDirection.x * _sensitivity * Time.deltaTime, Vector3.up);
		transform.rotation *= Quaternion.AngleAxis(-_movementHandler.RotationDirection.y * _sensitivity * Time.deltaTime, Vector3.right);

		var angles = transform.localEulerAngles;
		angles.z = 0;

		var angle = transform.localEulerAngles.x;

		if (angle > 180 && angle < _maxLookAngle)
		{
			angles.x = _maxLookAngle;
		}
		else if (angle < 180 && angle > _minLookAngle)
		{
			angles.x = _minLookAngle;
		}

		transform.localEulerAngles = angles;

		transform.transform.localEulerAngles = new Vector3(angles.x, angles.y, 0);
	}
}
