using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMovement), typeof(CharacterStatus))]
public class PlayerCombat : MonoBehaviour, ICombat
{
	[SerializeField] private float _lightDamage;
	[SerializeField] private float _heavyDamage;
	[SerializeField] private float _attackCooldown = 1f;
	[SerializeField] private float _alternativeAttackCooldown = 3f;

	private List<DamageCollider> _damageColliders = new List<DamageCollider>();
	private InputHandler _inputHandler;
	private CharacterStatus _characterStatus;
	private PlayerAnimation _playerAnimation;

	private float _attackTimer = 0;
	private float _alternativeAttackTimer = 0;
	private int _maxAttackCounter = 3;
	private int _currentAttackCounter = 0;
	private bool _canAttack = true;

	private IPrimaryAttack _primaryAttack;
	private IAlternativeAttack _alternativeAttack;
	public PlayerAnimation PlayerAnimation => _playerAnimation;

	public float AlternativeAttackCooldown => _alternativeAttackCooldown;

	public int MaxAttackCounter => _maxAttackCounter;

	public int AttackCounter { get { return _currentAttackCounter; } set { _currentAttackCounter = value; } }

	public bool CanAttack => _canAttack && _characterStatus.HasStamina();

	public bool CanAlternativeAttack => _canAttack && _characterStatus.HasStamina() && (_alternativeAttackTimer >= _alternativeAttackCooldown);

	public float AttackTimer => _attackTimer;

	[Inject]
	private void Construct(InputHandler movementHandler)
	{
		_inputHandler = movementHandler;
	}

	private void Awake()
	{
		_characterStatus = GetComponent<CharacterStatus>();
		_primaryAttack = GetComponent<IPrimaryAttack>();
		_alternativeAttack = GetComponent<IAlternativeAttack>();
		_damageColliders = GetComponentsInChildren<DamageCollider>().ToList();
		_playerAnimation = GetComponent<PlayerAnimation>();

		DisableDamageColliders();
		_inputHandler.AttackEvent += _primaryAttack.Attack;
		_inputHandler.AlternativeAttackEvent += _alternativeAttack.AlternativeAttack;
	}

	private void Update()
	{
		_alternativeAttackTimer += Time.deltaTime;

		if (_currentAttackCounter == 0)
			return;

		_attackTimer += Time.deltaTime;

		if (_attackTimer > _attackCooldown)
		{
			_attackTimer = 0;
			_currentAttackCounter = 0;
		}
	}

	public void StartAttack()
	{
		_canAttack = false;
		_attackTimer = 0;
	}

	public void StartAlternativeAttack()
	{
		StartAttack();
		_alternativeAttackTimer = 0;
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
				_characterStatus.TakeStaminaDamage(_lightDamage);
				break;
			case "Heavy":
				EnableDamageColliders(_heavyDamage);
				_characterStatus.TakeStaminaDamage(_heavyDamage);
				break;
			case "Alternative":
				EnableAlternativeColliders(_lightDamage);
				_characterStatus.TakeStaminaDamage(_heavyDamage);
				break;
			default:
				EnableDamageColliders(_lightDamage);
				_characterStatus.TakeStaminaDamage(_lightDamage);
				break;
		}
	}

	public void DisableDamageColliders()
	{
		foreach (var dc in _damageColliders)
		{
			if (!dc.ColliderType.Equals(ColliderType.Damage))
				continue;

			dc.ClearTargets();
			dc.DisableCollider();
		}
	}

	private void EnableDamageColliders(float damage)
	{
		foreach (var dc in _damageColliders)
		{
			if (!dc.ColliderType.Equals(ColliderType.Damage))
				continue;

			dc.SetDamage(damage);
			dc.EnableCollider();
		}
	}

	private void EnableAlternativeColliders(float damage)
	{
		foreach (var dc in _damageColliders)
		{
			if (!dc.ColliderType.Equals(ColliderType.Alternative))
				continue;

			dc.SetDamage(damage);
			dc.EnableCollider();
		}
	}

	public void DisableAlternativeColliders()
	{
		foreach (var dc in _damageColliders)
		{
			if (!dc.ColliderType.Equals(ColliderType.Alternative))
				continue;

			dc.ClearTargets();
			dc.DisableCollider();
		}
	}
}

public interface ICombat
{
	public float AttackTimer { get; }
	public float AlternativeAttackCooldown { get; }
	public int MaxAttackCounter { get; }
	public int AttackCounter { get; set; }
	public bool CanAttack { get; }
	public bool CanAlternativeAttack { get; }
	public PlayerAnimation PlayerAnimation { get; }
	public void StartAttack();
	public void StartAlternativeAttack();
	public void EndAttack();
	public void EnableDamageColliders(string type);
	public void DisableDamageColliders();
}
