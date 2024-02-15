using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : IDisposable
{
	private IInputService _inputService;

	public Vector3 MovementDirection { get; private set; }
	public Vector2 MovementDirection2D { get; private set; }
	public Vector3 RotationDirection { get; private set; }

	public event UnityAction AttackEvent;
	public event UnityAction AlternativeAttackEvent;

	public InputHandler(IInputService inputService)
	{
		_inputService = inputService;

		_inputService.MovementAxis += OnMove;
		_inputService.Attack += OnAttack;
		_inputService.AlternativeAttack += OnAlternativeAttack;
		_inputService.RotationAxis += OnRotate;
	}

	public void Dispose()
	{
		_inputService.MovementAxis -= OnMove;
		_inputService.Attack -= OnAttack;
		_inputService.AlternativeAttack -= OnAlternativeAttack;
		_inputService.RotationAxis -= OnRotate;
	}

	private void OnMove(Vector2 movementVector)
	{
		var normalizedVector = movementVector.normalized;

		MovementDirection = new Vector3(normalizedVector.x, 0, normalizedVector.y);
		MovementDirection2D = movementVector;
	}

	private void OnRotate(Vector2 rotationVector)
	{
		//var normalizedVector = rotationVector.normalized;

		RotationDirection = new Vector3(rotationVector.x, rotationVector.y, 0);
	}

	private void OnAttack()
	{
		AttackEvent?.Invoke();
	}

	private void OnAlternativeAttack()
	{
		AlternativeAttackEvent?.Invoke();
	}
}
