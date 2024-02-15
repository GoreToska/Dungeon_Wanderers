using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class PlayerAnimation : MonoBehaviour
{
	private Animator _animator;
	private CharacterController _characterController;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		Debug.Log(_characterController.velocity.magnitude);
		_animator.SetFloat("Speed", _characterController.velocity.magnitude);
	}


}
