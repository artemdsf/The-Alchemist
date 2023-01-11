using UnityEngine;

public class GolemAttackB : GolemAttackState
{	
	[Header("Attack 1")]
	[SerializeField] private Vector3 _attack1Pos;
	[SerializeField] private uint _damage = 5;
	[SerializeField] private uint _attackProjectilesCount = 8;
	[SerializeField] private string _attackName = "Attack";
	[SerializeField] private float _offset = 5;

	protected void Attack1B()
	{
		for (int i = 0; i < _attackProjectilesCount; i++)
		{
			InstProjectile(transform.position + _offset * (i + 1) * Vector3.right, Quaternion.identity, _damage);
		}
	}

	private void Update()
	{
		if (!GameManager.IsGamePaused && controller.CurentState == GolemState.B)
		{
			attackDelay += Time.deltaTime;

			if (attackDelay > maxAttackDelay)
			{
				Attack();
				attackDelay = 0;
			}
		}
	}

	private void Attack()
	{
		animator.SetTrigger(_attackName);
	}
}