using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ICombat))]
public class PrimaryMeleeAttack : MonoBehaviour, IPrimaryAttack
{
	private ICombat _combatSystem;
	
	private void Awake()
	{
		_combatSystem = GetComponent<ICombat>();
	}

	public void Attack()
	{
		if (!_combatSystem.CanAttack)
			return;

		_combatSystem.PlayerAnimation.PlayAttackAnimation(_combatSystem.AttackCounter);
		_combatSystem.StartAttack();

		_combatSystem.AttackCounter += 1;

		if (_combatSystem.AttackCounter == _combatSystem.MaxAttackCounter)
		{
			_combatSystem.AttackCounter = 0;
		}
	}
}

public interface IPrimaryAttack
{
	public void Attack();
}