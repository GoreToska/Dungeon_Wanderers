using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour, IHealable, IDamagable, IStamina
{
	[SerializeField] private float _maxHP;
	[SerializeField] private float _hp;
	[SerializeField] private float _maxDefence;
	[SerializeField] private float _defence;
	[SerializeField] private float _maxStamina;
	[SerializeField] private float _stamina;

	[SerializeField] private float _timeToRegenHP;
	[SerializeField] private float _timeToRegenStamina;
	[SerializeField] private float _hpRegen;
	[SerializeField] private float _staminaRegen;

	[SerializeField] private StatusBar _hpBar;
	[SerializeField] private StatusBar _staminaBar;

	public event UnityAction<float> OnHPChange;
	public event UnityAction<float> OnDefenceChange;
	public event UnityAction<float> OnStaminaChange;

	private float _staminaTimer = 0;
	private float _hpTimer = 0;

	public float HP => _hp;

	public float Stamina => _stamina;

	private void Start()
	{
		_hp = _maxHP;
		_defence = _maxDefence;
		_stamina = _maxStamina;
		_staminaTimer = 0;
		_staminaTimer = 0;
	}

	private void Update()
	{
		_staminaTimer += Time.deltaTime;
		_hpTimer += Time.deltaTime;

		if (_hpTimer >= _timeToRegenHP)
		{
			TakeHeal(_hpRegen * Time.deltaTime);
		}

		if (_staminaTimer >= _timeToRegenStamina)
		{
			RegenStamina(_staminaRegen * Time.deltaTime);
		}
	}

	public bool HasStamina()
	{
		if (_stamina > 0)
		{
			return true;
		}

		return false;
	}

	public void StopStaminaRegen()
	{
		_staminaTimer = 0;
	}

	public void StopHpRegen()
	{
		_hpTimer = 0;
	}

	public void TakeDamage(float damage, bool ignoreDefence = false)
	{
		StopHpRegen();
		_hp -= damage;

		if (_hp < 0)
		{
			_hp = 0;
			Debug.Log("Dead!");
		}

		if (_hpBar)
			_hpBar.SetValue(_hp, _maxHP);

		OnHPChange?.Invoke(_hp);
	}

	public void TakeHeal(float heal)
	{
		_hp += heal;

		if (_hp > _maxHP)
		{
			_hp = _maxHP;
		}

		if (_hpBar)
			_hpBar.SetValue(_hp, _maxHP);

		OnHPChange?.Invoke(_hp);
	}

	public void TakeStaminaDamage(float damage, bool ignoreDefence = false)
	{
		StopStaminaRegen();
		_stamina -= damage;

		if (_stamina < 0)
		{
			_stamina = 0;
		}

		if (_staminaBar)
			_staminaBar.SetValue(_stamina, _maxStamina);

		OnStaminaChange?.Invoke(_stamina);
	}

	public void RegenStamina(float value)
	{
		if (_stamina >= _maxStamina)
		{
			return;
		}

		_stamina += value;

		if (_staminaBar)
			_staminaBar.SetValue(_stamina, _maxStamina);

		OnStaminaChange?.Invoke(_stamina);
	}
}
