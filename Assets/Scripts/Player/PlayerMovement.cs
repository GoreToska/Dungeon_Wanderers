using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	[Header("Movement Settings")]
	[SerializeField] private float _runSpeed;
	[SerializeField] private float _walkSpeed;
	[SerializeField] private float _gravity;
	[SerializeField] protected float _rotationSpeed;

	private bool _isWalking = false;
	private bool _isStopped = false;
	private Quaternion _lastRotation;
	private Camera _camera;
	private Vector3 _moveDirection;
	private InputHandler _movementHandler;
	private CharacterController _characterController;

	[Inject]
	private void Construct(InputHandler movementHandler)
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

		if (_isWalking)
		{
			HandleMovement(_walkSpeed);
		}
		else
		{
			HandleMovement(_runSpeed);
		}
	}

	public void StopMovement()
	{
		_isStopped = true;
	}

	public void ResumeMovement()
	{
		_isStopped = false;
	}

	public void SetWalking()
	{
		_isWalking = true;
	}

	public void SetRunning()
	{
		_isWalking = false;
	}

	private void HandleMovement(float speed)
	{
		if (_isStopped)
			return;

		_characterController.Move(_moveDirection * Time.deltaTime * speed);

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