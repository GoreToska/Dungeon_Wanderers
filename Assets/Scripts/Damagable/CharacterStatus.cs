using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour, IHealable, IDamagable
{
	[SerializeField] private float _maxHP;
	[SerializeField] private float _hp;
	[SerializeField] private float _maxDefence;
	[SerializeField] private float _defence;

	public float HP => _hp;

	private void Start()
	{
		_hp = _maxHP;
		_defence = _maxDefence;
	}

	public void TakeDamage(float damage, bool ignoreDefence = false)
	{
		_hp -= damage;

		if (_hp < 0)
		{
			_hp = 0;
			Debug.Log("Dead!");
		}
	}

	public void TakeHeal(float heal)
	{
		_hp += heal;

		if (_hp > _maxHP)
		{
			_hp = _maxHP;
		}
	}
}
