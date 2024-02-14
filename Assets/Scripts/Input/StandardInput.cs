using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StandardInput : IInputService
{
	public event UnityAction<Vector2> MovementAxis;
	public event UnityAction Attack;
	public event UnityAction AlternativeAttack;
	public event UnityAction<Vector2> RotationAxis;

	private PlayerControls _controls;

	public StandardInput()
	{
		_controls = new PlayerControls();

		Debug.Log("StandardInput Constructor");

		_controls.GameInput.Movement.performed += i => MovementAxis?.Invoke(i.ReadValue<Vector2>());
		_controls.GameInput.Attack.performed += i => Attack?.Invoke();
		_controls.GameInput.AlternativeAttack.performed += i => AlternativeAttack?.Invoke();
		_controls.GameInput.Rotation.performed += i => RotationAxis?.Invoke(i.ReadValue<Vector2>());

		_controls.Enable();
	}

	public void EnableGameControls()
	{
		_controls.GameInput.Enable();
	}

	public void DisableGameControls()
	{
		_controls.GameInput.Disable();
	}
}
