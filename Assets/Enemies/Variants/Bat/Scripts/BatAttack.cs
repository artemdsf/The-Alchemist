using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatAttack : EnemyAttack
{
	[SerializeField] private Vector2 _attackOffset;
	[SerializeField] private float _attackRange;

	private void Update()
	{
		if (CheckRange())
		{
			StartAttack();
		}
		else
		{
			EndAttack();
		}
	}

	protected void TryToHit()
	{
		if (CheckRange())
		{
			HitPlayer();
		}
	}

	private void StartAttack()
	{
		animator.SetTrigger(Const.Attack1Name);
	}

	private void EndAttack()
	{
		animator.ResetTrigger(Const.Attack1Name);
	}

	private bool CheckRange()
	{
		return (Player.transform.position - (transform.position + (Vector3)_attackOffset)).magnitude < _attackRange;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position + (Vector3)_attackOffset, _attackRange);
	}
}