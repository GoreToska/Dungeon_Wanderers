using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private float _lightDamage;
	[SerializeField] private float _heavyDamage;
	[SerializeField] private float _attackCooldown = 1f;

	private List<DamageCollider> _damageColliders = new List<DamageCollider>();
	private PlayerAnimation _playerAnimation;
	private InputHandler _inputHandler;
	private PlayerMovement _playerMovement;

	private float _timer = 0;
	private int _maxAttackCounter = 3;
	private int _currentAttackCounter = 0;
	private bool _canAttack = true;

	[Inject]
	private void Construct(InputHandler movementHandler)
	{
		_inputHandler = movementHandler;
	}

	private void Awake()
	{
		_playerAnimation = GetComponent<PlayerAnimation>();
		_inputHandler.AttackEvent += Attack;
		_playerMovement = GetComponent<PlayerMovement>();

		_damageColliders = GetComponentsInChildren<DamageCollider>().ToList();
		DisableDamageColliders();
	}

	private void Update()
	{
		if (_currentAttackCounter == 0)
			return;

		_timer += Time.deltaTime;

		if (_timer > _attackCooldown)
		{
			_timer = 0;
			_currentAttackCounter = 0;
		}
	}

	private void Attack()
	{
		if (!_canAttack)
			return;

		_playerAnimation.PlayLightAttackAnimation(_currentAttackCounter);
		//_playerMovement.SetWalking();
		StartAttack();

		_timer = 0;
		_currentAttackCounter += 1;

		if (_currentAttackCounter == _maxAttackCounter)
		{
			_currentAttackCounter = 0;
		}
	}

	public void StartAttack()
	{
		_canAttack = false;
	}

	public void EndAttack()
	{
		_canAttack = true;
	}

	public void EnableDamageColliders(string type)
	{
		switch (type)
		{
			case "Light":
				EnableDamageColliders(_lightDamage);
				break;
			case "Heavy":
				EnableDamageColliders(_heavyDamage);
				break;
			default:
				EnableDamageColliders(_lightDamage);
				break;
		}
	}

	public void DisableDamageColliders()
	{
		foreach (var dc in _damageColliders)
		{
			dc.ClearTargets();
			dc.enabled = false;
		}
	}

	private void EnableDamageColliders(float damage)
	{
		foreach (var dc in _damageColliders)
		{
			dc.SetDamage(damage);
			dc.enabled = true;
		}
	}
}
