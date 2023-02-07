using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullAttack : EnemyAttack
{
	[Header("Attack")]
	[SerializeField] private float _attackDelay = 0.5f;

	private float _currentAttackDelay;

	private void Update()
	{
		_currentAttackDelay += Time.deltaTime;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (_currentAttackDelay > _attackDelay && collision.gameObject.tag == Const.PlayerName)
		{
			HitPlayer();
			_currentAttackDelay = 0;
		}
	}
}
