using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ICombat))]
public class BarbarianAlternativeAttack : MonoBehaviour, IAlternativeAttack
{
	private ICombat _combatSystem;

	private void Awake()
	{
		_combatSystem = GetComponent<ICombat>();
	}

	public void AlternativeAttack()
	{
		if (!_combatSystem.CanAlternativeAttack)
			return;

		_combatSystem.PlayerAnimation.PlayAlternativeAttackAnimation(_combatSystem.AttackCounter);
		_combatSystem.StartAlternativeAttack();

		_combatSystem.AttackCounter += 1;

		if (_combatSystem.AttackCounter == _combatSystem.MaxAttackCounter)
		{
			_combatSystem.AttackCounter = 0;
		}
	}
}

public interface IAlternativeAttack
{
	public void AlternativeAttack();
}