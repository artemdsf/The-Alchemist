using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AttackVisual))]
public class PlayerArea : PlayerAttack
{
	private List<Collider2D> _colliders = new List<Collider2D>();

	private float _attackDelay = 1;
	private float _healDelay = 1;

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
		Attack();
		TryHealPlayer(collision);
	}

	private void Attack()
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

	protected override void TryHealPlayer(Collider2D collider)
	{
		if (_curentHealDelay > _healDelay && collider.tag == "Player")
		{
			base.TryHealPlayer(collider);
			_curentHealDelay = 0;
		}
	}

	private void UpdateDelay()
	{
		_curentAttackDelay += Time.deltaTime;
		_curentHealDelay += Time.deltaTime;
	}
}