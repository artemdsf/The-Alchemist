using UnityEngine;

public class GolemAttack : EnemyAttack
{
	[SerializeField] [Min(0)] private float _maxAttackDelay = 0.5f;
	private float _attackDelay;

	private void Update()
	{
		_attackDelay += Time.deltaTime;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (_attackDelay > _maxAttackDelay && collision.gameObject.tag == Const.PlayerName)
		{
			HitPlayer();
			_attackDelay = 0;
		}
	}
}