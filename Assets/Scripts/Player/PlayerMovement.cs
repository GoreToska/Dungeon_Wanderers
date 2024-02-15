using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	[Header("Movement Settings")]
	[SerializeField] private float _speed;
	[SerializeField] private float _gravity;
	[SerializeField] protected float _rotationSpeed;

	private Quaternion _lastRotation;
	private Camera _camera;
	private Vector3 _moveDirection;
	private MovementHandler _movementHandler;
	private CharacterController _characterController;

	[Inject]
	private void Construct(MovementHandler movementHandler)
	{
		_movementHandler = movementHandler;
	}

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();

		Debug.Log("TODO");
		Cursor.visible = false;
		_camera = Camera.main;
		_lastRotation = transform.localRotation;
	}

	private void Update()
	{
		CalculateMovementAxis();
		HandleGravity();
		HandleMovement();
	}

	private void HandleMovement()
	{
		_characterController.Move(_moveDirection * Time.deltaTime * _speed);

		if (_movementHandler.MovementDirection != Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_moveDirection), _rotationSpeed * Time.deltaTime);
		}
	}

	private void HandleGravity()
	{
		_characterController.Move(Vector3.down * Time.deltaTime * _gravity);
	}

	private void CalculateMovementAxis()
	{
		_moveDirection =
			new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z) * _movementHandler.MovementDirection2D.y;
		_moveDirection = _moveDirection +
			new Vector3(_camera.transform.right.x, 0, _camera.transform.right.z) * _movementHandler.MovementDirection2D.x;

		_moveDirection.Normalize();
	}
}
