using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInputService
{
	public event UnityAction<Vector2> MovementAxis;
	public event UnityAction Attack;
	public event UnityAction AlternativeAttack;
	public event UnityAction<Vector2> RotationAxis;

	public void EnableGameControls();
	public void DisableGameControls();
}