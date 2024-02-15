using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageCollider : MonoBehaviour
{
	private List<IDamagable> _damagedObjects = new List<IDamagable>();
	private float _damage;

	public void SetDamage(float damage)
	{
		_damage = damage;
	}

	public void ClearTargets()
	{
		_damagedObjects.Clear();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Damagable")
		{
			var damagable = other.GetComponent<IDamagable>();

			if (_damagedObjects.Contains(damagable))
				return;

			damagable.TakeDamage(_damage);
			_damagedObjects.Add(damagable);
		}
	}
}
