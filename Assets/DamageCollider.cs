using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageCollider : MonoBehaviour
{
	private Collider _collider;
	private List<IDamagable> _damagedObjects = new List<IDamagable>();
	private float _damage;

	private void Awake()
	{
		_collider = GetComponent<Collider>();
	}

	public void SetDamage(float damage)
	{
		_damage = damage;
	}

	public void ClearTargets()
	{
		_damagedObjects.Clear();
	}

	public void EnableCollider()
	{
		_collider.enabled = true;
	}

	public void DisableCollider()
	{
		_collider.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Damagable" && other.transform.root.gameObject != this.transform.root.gameObject)
		{
			var damagable = other.GetComponent<IDamagable>();

			if (_damagedObjects.Contains(damagable))
				return;

			damagable.TakeDamage(_damage);
			_damagedObjects.Add(damagable);
		}
	}
}
