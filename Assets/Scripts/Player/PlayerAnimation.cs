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
		_animator.SetFloat("Speed", _characterController.velocity.magnitude, 0.05f, Time.deltaTime);
	}

	public virtual void PlayAttackAnimation(int attackCount)
	{
		_animator.SetInteger("AttackCount", attackCount);
		_animator.SetTrigger("Attack");
	}

	public virtual void PlayAlternativeAttackAnimation(int attackCount)
	{
		_animator.SetInteger("AttackCount", attackCount);
		_animator.SetTrigger("AlternativeAttack");
	}
}
