using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.ShaderGraph;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	[Header("Movement Settings")]
	[SerializeField] private float _speed;
	[SerializeField] private float _gravity;

	[Header("Camera Settings")]
	[SerializeField] private Transform _rotationPivot;
	[SerializeField] private float _minLookAngle = 40;
	[SerializeField] private float _maxLookAngle = 340f;
	[SerializeField] private float _sensitivity = 1f;
	private MovementHandler _movementHandler;
	private CharacterController _characterController;

	[Inject]
	private void Construct(MovementHandler movementHandler)
	{
		_movementHandler = movementHandler;

		Debug.Log($"Current movement handler on this player - {_movementHandler.GetType()}");
	}

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();

		Debug.Log("TODO");
		Cursor.visible = false;
	}

	private void Update()
	{
		HandleGravity();
		HandleMovement();
		HandleRotation();
	}

	private void HandleMovement()
	{
		_characterController.Move(_movementHandler.MovementDirection * Time.deltaTime * _speed);
	}

	private void HandleGravity()
	{
		_characterController.Move(Vector3.down * Time.deltaTime * _gravity);
	}

	private void HandleRotation()
	{
		if (_movementHandler.RotationDirection == Vector3.zero)
			return;

		//_rotationPivot.Rotate(0f, _movementHandler.RotationDirection.y * _sensitivity * Time.deltaTime, 0f, Space.World);
		//_rotationPivot.Rotate(-_movementHandler.RotationDirection.x * _sensitivity * Time.deltaTime, 0f, 0f, Space.Self);

		_rotationPivot.rotation *= Quaternion.AngleAxis(_movementHandler.RotationDirection.x * _sensitivity * Time.deltaTime, Vector3.up);
		_rotationPivot.rotation *= Quaternion.AngleAxis(_movementHandler.RotationDirection.y * _sensitivity * Time.deltaTime, Vector3.right);

		var angles = _rotationPivot.localEulerAngles;
		angles.z = 0;

		var angle = _rotationPivot.localEulerAngles.x;

		if (angle > 180 && angle < _maxLookAngle)
		{
			angles.x = _maxLookAngle;
		}
		else if (angle < 180 && angle > _minLookAngle)
		{
			angles.x = _minLookAngle;
		}

		_rotationPivot.localEulerAngles = angles;
		_rotationPivot.localEulerAngles = new Vector3(angles.x, angles.y, 0);
	}
}
