using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AttackVisual))]
public class Area : Attack
{
	[SerializeField] protected float _attackDelay = 0;
	[SerializeField] protected float _healDelay = 0;

	[SerializeField] private List<Collider2D> _colliders = new List<Collider2D>();

	private float _curentAttackDelay = 0;
	private float _curentHealDelay = 0;

	protected override void Awake()
	{
		base.Awake();

		_curentAttackDelay = _attackDelay;
		_curentHealDelay = _healDelay;
	}

	protected override void Update()
	{
		base.Update();
		UpdateDelay();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_colliders.Add(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		_colliders.Remove(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		AttackAllEnemies();
		TryHeal(collision);
	}

	private void AttackAllEnemies()
	{
		if (_curentAttackDelay > _attackDelay && _colliders.Count > 0)
		{
			for (int i = _colliders.Count - 1; i >= 0; i--)
			{
				TryDamage(_colliders[i]);
			}
			_curentAttackDelay = 0;
		}
	}

	protected override void TryHeal(Collider2D collider)
	{
		if (_curentHealDelay > _healDelay)
		{
			base.TryHeal(collider);
			_curentHealDelay = 0;
		}
	}

	private void UpdateDelay()
	{
		_curentAttackDelay += Time.deltaTime;
		_curentHealDelay += Time.deltaTime;
	}
}